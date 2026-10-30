using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;

namespace FiyiStack.Services.System
{
    public class CookieService(IJSRuntime IJSRuntime, IDataProtectionProvider IDataProtectionProvider)
    {
        private readonly IJSRuntime _IJSRuntime = IJSRuntime;
        private readonly IDataProtector _IDataProtector = IDataProtectionProvider.CreateProtector("UserIdProtection");

        public async Task SetEncryptedUserIdCookieAsync(string strUserId)
        {
            //Protect/Encrypt UserId and save it in a cookie
            string EncryptedUserId = _IDataProtector.Protect(strUserId);
            string CookieValue = $"EncryptedUserId={EncryptedUserId}; path=/; max-age={7 * 24 * 60 * 60}";
            await _IJSRuntime.InvokeVoidAsync("eval", $"document.cookie = '{CookieValue}'");
        }

        public async Task<int?> GetDecryptedUserIdAsync()
        {
            string EncryptedUserId = await _IJSRuntime.InvokeAsync<string>("cookieHelper.getCookieOfEncryptedUserId", "EncryptedUserId");

            if (string.IsNullOrWhiteSpace(EncryptedUserId))
                return null;

            try
            {
                string DecryptedUserId = _IDataProtector.Unprotect(EncryptedUserId);

                int UserId = int.Parse(DecryptedUserId);

                return UserId;
            }
            catch
            {
                //The cookie was changed or is not valid
                return null;
            }
        }

        public async Task RemoveUserIdCookieAsync()
        {
            string CookieValue = "EncryptedUserId=; path=/; max-age=0";
            await _IJSRuntime.InvokeVoidAsync("eval", $"document.cookie = '{CookieValue}'");
        }
    }

}