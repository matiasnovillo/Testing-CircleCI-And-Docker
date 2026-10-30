using ClosedXML.Excel;
using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.Interfaces;

namespace FiyiStack.Areas.CMS.RoleMenuBack.Services
{
    public class RoleMenuImportationService : IRoleMenuImportationService
    {
        public List<RoleMenu> ImportExcel(string path, long userId)
        {
            List<RoleMenu> ListRoleMenu = [];

            XLWorkbook WorkBook = new(path);

            var Rows = WorkBook.Worksheet(1).RangeUsed()!.RowsUsed();

            foreach (var row in Rows)
            {
                var rowNumber = row.RowNumber();

                if (rowNumber > 1)
                {
                    int MenuId = Convert.ToInt32(row.Cell(7).GetString());
                    int RoleId = Convert.ToInt32(row.Cell(8).GetString());
                    

                    RoleMenu RoleMenu = new()
                    {
                        RoleMenuId = 0,
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        UserCreationId = userId,
                        UserLastModificationId = userId,
                        MenuId = MenuId,
                        RoleId = RoleId,
                        
                    };

                    ListRoleMenu.Add(RoleMenu);
                }
            }

            return ListRoleMenu;
        }
    }
}