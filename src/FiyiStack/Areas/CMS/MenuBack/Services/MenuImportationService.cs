using ClosedXML.Excel;
using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.MenuBack.Interfaces;

namespace FiyiStack.Areas.CMS.MenuBack.Services
{
    public class MenuImportationService : IMenuImportationService
    {
        public List<Menu> ImportExcel(string path, long userId)
        {
            List<Menu> ListMenu = [];

            XLWorkbook WorkBook = new(path);

            var Rows = WorkBook.Worksheet(1).RangeUsed()!.RowsUsed();

            foreach (var row in Rows)
            {
                var rowNumber = row.RowNumber();

                if (rowNumber > 1)
                {
                    string Name = row.Cell(7).GetString();
                    int MenuFatherId = Convert.ToInt32(row.Cell(8).GetString());
                    string URLPath = row.Cell(9).GetString();
                    string IconURLPath = row.Cell(10).GetString();
                    int Order = Convert.ToInt32(row.Cell(11).GetString());
                    

                    Menu Menu = new()
                    {
                        MenuId = 0,
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        UserCreationId = userId,
                        UserLastModificationId = userId,
                        Name = Name,
                        MenuFatherId = MenuFatherId,
                        URLPath = URLPath,
                        IconURLPath = IconURLPath,
                        Order = Order,
                        
                    };

                    ListMenu.Add(Menu);
                }
            }

            return ListMenu;
        }
    }
}