using ClosedXML.Excel;
using IronPdf;
using CsvHelper;
using FiyiStack.Areas.System.ParameterBack.Entities;
using FiyiStack.Areas.System.ParameterBack.Interfaces;
using System.Data;
using System.Globalization;

namespace FiyiStack.Areas.System.ParameterBack.Services
{
    public class ParameterExportationService : IParameterExportationService
    {
        public void ExportToExcel(string path, DataTable dataTableParameter)
        {
            using XLWorkbook XLWorkbook = new();

            DataTable DataTableParameterOriginal = new()
            {
                TableName = "Parameter"
            };

            //We define another DataTable DataTableParameterCopy to avoid issue related to DateTime conversion
            DataTable DataTableParameterCopy = new()
            {
                TableName = "Parameter"
            };

            #region Define columns for DataTableParameterCopy
            DataColumn DataTableColumnParameterIdForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "ParameterId"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnParameterIdForDataTableParameterCopy);

            DataColumn DataTableColumnDateTimeCreationForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeCreation"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnDateTimeCreationForDataTableParameterCopy);

            DataColumn DataTableColumnDateTimeLastModificationForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeLastModification"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnDateTimeLastModificationForDataTableParameterCopy);

            DataColumn DataTableColumnUserCreationIdForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserCreationId"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnUserCreationIdForDataTableParameterCopy);

            DataColumn DataTableColumnUserLastModificationIdForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserLastModificationId"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnUserLastModificationIdForDataTableParameterCopy);

            DataColumn DataTableColumnActiveForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Active"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnActiveForDataTableParameterCopy);

            DataColumn DataTableColumnNameForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Name"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnNameForDataTableParameterCopy);

            DataColumn DataTableColumnValueForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Value"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnValueForDataTableParameterCopy);

            DataColumn DataTableColumnIsPrivateForDataTableParameterCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "IsPrivate"
            };
            DataTableParameterCopy.Columns.Add(DataTableColumnIsPrivateForDataTableParameterCopy);

            
            #endregion

            DataTableParameterOriginal = dataTableParameter;

            foreach (DataRow DataRow in DataTableParameterOriginal.Rows)
            {
                DataTableParameterCopy.Rows.Add(DataRow.ItemArray);
            }

            IXLWorksheet XLWorksheet = XLWorkbook.Worksheets.Add(DataTableParameterCopy);

            XLWorksheet.ColumnsUsed().AdjustToContents();

            XLWorkbook.SaveAs(path);
        }

        public void ExportToCSV(string path, List<Parameter> ListParameter)
        {
            StreamWriter StreamWriter = new(path);

            using CsvWriter CsvWriter = new(StreamWriter, CultureInfo.InvariantCulture);

            CsvWriter.WriteRecords(ListParameter);
        }

        public void ExportToPDF(string path, List<Parameter> ListParameter)
        {
            string ProjectName = "FiyiStack";

            string Table = "Parameter";

            ChromePdfRenderer ChromePdfRenderer = new();

            string RowsAsHTML = "";

            foreach (Parameter? Parameter in ListParameter)
            {
                RowsAsHTML += $@"
<tr>
    
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.ParameterId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.Name}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.Value}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Parameter.IsPrivate}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
</tr>";
            }

            string FullHTMLContent = $@"
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""88%"" style=""width: 88% !important; min-width: 88%; max-width: 88%;"">
    <tr>
    <td align=""left"" valign=""top"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#1a1a1a"" style=""font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #1a1a1a; font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">{ProjectName}</span>
        </font>
        <div style=""height: 25px; line-height: 25px; font-size: 23px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#4c4c4c"" style=""font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of {Table}</span>
        </font>
        <div style=""height: 35px; line-height: 35px; font-size: 33px;"">&nbsp;</div>
    </td>
    </tr>
</table>
<br>
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""width: 100% !important; min-width: 100%; max-width: 100%;"">
    <tr>
        
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ParameterId</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserCreationId</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserLastModificationId</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Name</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Value</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">IsPrivate</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {DateTime.Now}</span>
</font>
";

            ChromePdfRenderer.RenderHtmlAsPdf(FullHTMLContent).SaveAs(path);
        }
    }
}