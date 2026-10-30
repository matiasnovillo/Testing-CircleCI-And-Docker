using ClosedXML.Excel;
using FiyiStack.Areas.CMS.RoleBack.Entities;
using FiyiStack.Areas.CMS.RoleBack.Interfaces;

namespace FiyiStack.Areas.CMS.RoleBack.Services
{
    public class RoleImportationService : IRoleImportationService
    {
        public List<Role> ImportExcel(string path, long userId)
        {
            List<Role> ListRole = [];

            XLWorkbook WorkBook = new(path);

            var Rows = WorkBook.Worksheet(1).RangeUsed()!.RowsUsed();

            foreach (var row in Rows)
            {
                var rowNumber = row.RowNumber();

                if (rowNumber > 1)
                {
                    string Name = row.Cell(7).GetString();
                    

                    Role Role = new()
                    {
                        RoleId = 0,
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        UserCreationId = userId,
                        UserLastModificationId = userId,
                        Name = Name,
                        
                    };

                    ListRole.Add(Role);
                }
            }

            return ListRole;
        }
    }
}