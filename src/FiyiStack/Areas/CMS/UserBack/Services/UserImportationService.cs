using ClosedXML.Excel;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.UserBack.Interfaces;

namespace FiyiStack.Areas.CMS.UserBack.Services
{
    public class UserImportationService : IUserImportationService
    {
        public List<User> ImportExcel(string path, long userId)
        {
            List<User> ListUser = [];

            XLWorkbook WorkBook = new(path);

            var Rows = WorkBook.Worksheet(1).RangeUsed()!.RowsUsed();

            foreach (var row in Rows)
            {
                var rowNumber = row.RowNumber();

                if (rowNumber > 1)
                {
                    string Email = row.Cell(7).GetString();
                    string Password = row.Cell(8).GetString();
                    int RoleId = Convert.ToInt32(row.Cell(9).GetString());
                    

                    User User = new()
                    {
                        UserId = 0,
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        UserCreationId = userId,
                        UserLastModificationId = userId,
                        Email = Email,
                        Password = Password,
                        RoleId = RoleId,
                        
                    };

                    ListUser.Add(User);
                }
            }

            return ListUser;
        }
    }
}