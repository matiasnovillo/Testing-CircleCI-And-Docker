using ClosedXML.Excel;
using FiyiStack.Areas.System.ParameterBack.Entities;
using FiyiStack.Areas.System.ParameterBack.Interfaces;

namespace FiyiStack.Areas.System.ParameterBack.Services
{
    public class ParameterImportationService : IParameterImportationService
    {
        public List<Parameter> ImportExcel(string path, long userId)
        {
            List<Parameter> ListParameter = [];

            XLWorkbook WorkBook = new(path);

            var Rows = WorkBook.Worksheet(1).RangeUsed()!.RowsUsed();

            foreach (var row in Rows)
            {
                var rowNumber = row.RowNumber();

                if (rowNumber > 1)
                {
                    string Name = row.Cell(7).GetString();
                    string Value = row.Cell(8).GetString();
                    bool IsPrivate = Convert.ToBoolean(row.Cell(9).GetString());
                    

                    Parameter Parameter = new()
                    {
                        ParameterId = 0,
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        UserCreationId = userId,
                        UserLastModificationId = userId,
                        Name = Name,
                        Value = Value,
                        IsPrivate = IsPrivate,
                        
                    };

                    ListParameter.Add(Parameter);
                }
            }

            return ListParameter;
        }
    }
}