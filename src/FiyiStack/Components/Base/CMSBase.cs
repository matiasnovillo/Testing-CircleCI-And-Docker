using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.Repositories;
using FiyiStack.Helpers;
using FiyiStack.Services.System;
using Microsoft.AspNetCore.Components;

namespace FiyiStack.Components.Base
{
    public class CMSBase : ComponentBase
    {
        [Inject] private CookieService? CookieService { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private UserRepository? UserRepository { get; set; }
        [Inject] private RoleMenuRepository? RoleMenuRepository { get; set; }
        [Inject] private FailureRepository? FailureRepository { get; set; }

        protected User? User { get; set; }

        protected async Task GetUserIdFromCookies()
        {
            try
            {
                InjectionGuard.EnsureServicesAreInjected(CookieService, NavigationManager, UserRepository);

                int? UserId = await CookieService!.GetDecryptedUserIdAsync();

                if (!UserId.HasValue)
                {
                    NavigationManager!.NavigateTo("/entrar", true);
                }
                else
                {
                    User? UserTryingToLogIn = await UserRepository!.GetOneByUserIdAsync(Convert.ToInt32(UserId));

                    if (UserTryingToLogIn == null || !UserTryingToLogIn.Active)
                    {
                        NavigationManager!.NavigateTo("/entrar", true);
                    }
                    else
                    {
                        User = UserTryingToLogIn;
                    }
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected async Task IsUserAvailableToUseThisPage(string page)
        {
            try
            {
                InjectionGuard.EnsureServicesAreInjected(RoleMenuRepository, NavigationManager);

                List<Menu> ListMenuWithPermission = await RoleMenuRepository!
                                    .GetAllByRoleIdAndPathForPermissionAsync(User!.RoleId, page);

                if (ListMenuWithPermission.Count == 0)
                {
                    //Redirect to 403
                    NavigationManager!.NavigateTo("403");
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected async Task CatchException(Exception ex)
        {
            InjectionGuard.EnsureServicesAreInjected(FailureRepository);

            Failure failure = new()
            {
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = User!.UserId,
                UserLastModificationId = User!.UserId,
                EmergencyLevel = 1,
                Comment = "",
                Message = ex.Message,
                Source = ex.Source ?? "",
                StackTrace = ex.StackTrace ?? ""
            };

            await FailureRepository!.AddAsync(failure);
        }
    }
}