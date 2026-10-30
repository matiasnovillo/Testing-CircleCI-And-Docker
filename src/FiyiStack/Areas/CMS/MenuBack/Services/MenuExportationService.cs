using ClosedXML.Excel;
using IronPdf;
using CsvHelper;
using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.MenuBack.Interfaces;
using System.Data;
using System.Globalization;

namespace FiyiStack.Areas.CMS.MenuBack.Services
{
    public class MenuExportationService : IMenuExportationService
    {
        public void ExportToExcel(string path, DataTable dataTableMenu)
        {
            using XLWorkbook XLWorkbook = new();

            DataTable DataTableMenuOriginal = new()
            {
                TableName = "Menu"
            };

            //We define another DataTable DataTableMenuCopy to avoid issue related to DateTime conversion
            DataTable DataTableMenuCopy = new()
            {
                TableName = "Menu"
            };

            #region Define columns for DataTableMenuCopy
            DataColumn DataTableColumnMenuIdForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "MenuId"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnMenuIdForDataTableMenuCopy);

            DataColumn DataTableColumnDateTimeCreationForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeCreation"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnDateTimeCreationForDataTableMenuCopy);

            DataColumn DataTableColumnDateTimeLastModificationForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeLastModification"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnDateTimeLastModificationForDataTableMenuCopy);

            DataColumn DataTableColumnUserCreationIdForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserCreationId"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnUserCreationIdForDataTableMenuCopy);

            DataColumn DataTableColumnUserLastModificationIdForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserLastModificationId"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnUserLastModificationIdForDataTableMenuCopy);

            DataColumn DataTableColumnActiveForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Active"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnActiveForDataTableMenuCopy);

            DataColumn DataTableColumnNameForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Name"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnNameForDataTableMenuCopy);

            DataColumn DataTableColumnMenuFatherIdForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "MenuFatherId"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnMenuFatherIdForDataTableMenuCopy);

            DataColumn DataTableColumnURLPathForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "URLPath"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnURLPathForDataTableMenuCopy);

            DataColumn DataTableColumnIconURLPathForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "IconURLPath"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnIconURLPathForDataTableMenuCopy);

            DataColumn DataTableColumnOrderForDataTableMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Order"
            };
            DataTableMenuCopy.Columns.Add(DataTableColumnOrderForDataTableMenuCopy);

            
            #endregion

            DataTableMenuOriginal = dataTableMenu;

            foreach (DataRow DataRow in DataTableMenuOriginal.Rows)
            {
                DataTableMenuCopy.Rows.Add(DataRow.ItemArray);
            }

            IXLWorksheet XLWorksheet = XLWorkbook.Worksheets.Add(DataTableMenuCopy);

            XLWorksheet.ColumnsUsed().AdjustToContents();

            XLWorkbook.SaveAs(path);
        }

        public void ExportToCSV(string path, List<Menu> ListMenu)
        {
            StreamWriter StreamWriter = new(path);

            using CsvWriter CsvWriter = new(StreamWriter, CultureInfo.InvariantCulture);

            CsvWriter.WriteRecords(ListMenu);
        }

        public void ExportToPDF(string path, List<Menu> ListMenu)
        {
            string ProjectName = "FiyiStack";

            string Table = "Menu";

            ChromePdfRenderer ChromePdfRenderer = new();

            string RowsAsHTML = "";

            foreach (Menu? Menu in ListMenu)
            {
                RowsAsHTML += $@"
<tr>
    
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.MenuId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.Name}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.MenuFatherId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.URLPath}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.IconURLPath}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Menu.Order}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">MenuId</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">MenuFatherId</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">URLPath</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">IconURLPath</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Order</span>
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