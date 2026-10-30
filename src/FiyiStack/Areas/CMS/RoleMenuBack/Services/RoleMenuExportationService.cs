using ClosedXML.Excel;
using IronPdf;
using CsvHelper;
using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.Interfaces;
using System.Data;
using System.Globalization;

namespace FiyiStack.Areas.CMS.RoleMenuBack.Services
{
    public class RoleMenuExportationService : IRoleMenuExportationService
    {
        public void ExportToExcel(string path, DataTable dataTableRoleMenu)
        {
            using XLWorkbook XLWorkbook = new();

            DataTable DataTableRoleMenuOriginal = new()
            {
                TableName = "RoleMenu"
            };

            //We define another DataTable DataTableRoleMenuCopy to avoid issue related to DateTime conversion
            DataTable DataTableRoleMenuCopy = new()
            {
                TableName = "RoleMenu"
            };

            #region Define columns for DataTableRoleMenuCopy
            DataColumn DataTableColumnRoleMenuIdForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "RoleMenuId"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnRoleMenuIdForDataTableRoleMenuCopy);

            DataColumn DataTableColumnDateTimeCreationForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeCreation"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnDateTimeCreationForDataTableRoleMenuCopy);

            DataColumn DataTableColumnDateTimeLastModificationForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeLastModification"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnDateTimeLastModificationForDataTableRoleMenuCopy);

            DataColumn DataTableColumnUserCreationIdForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserCreationId"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnUserCreationIdForDataTableRoleMenuCopy);

            DataColumn DataTableColumnUserLastModificationIdForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserLastModificationId"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnUserLastModificationIdForDataTableRoleMenuCopy);

            DataColumn DataTableColumnActiveForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Active"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnActiveForDataTableRoleMenuCopy);

            DataColumn DataTableColumnMenuIdForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "MenuId"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnMenuIdForDataTableRoleMenuCopy);

            DataColumn DataTableColumnRoleIdForDataTableRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "RoleId"
            };
            DataTableRoleMenuCopy.Columns.Add(DataTableColumnRoleIdForDataTableRoleMenuCopy);

            
            #endregion

            DataTableRoleMenuOriginal = dataTableRoleMenu;

            foreach (DataRow DataRow in DataTableRoleMenuOriginal.Rows)
            {
                DataTableRoleMenuCopy.Rows.Add(DataRow.ItemArray);
            }

            IXLWorksheet XLWorksheet = XLWorkbook.Worksheets.Add(DataTableRoleMenuCopy);

            XLWorksheet.ColumnsUsed().AdjustToContents();

            XLWorkbook.SaveAs(path);
        }

        public void ExportToCSV(string path, List<RoleMenu> ListRoleMenu)
        {
            StreamWriter StreamWriter = new(path);

            using CsvWriter CsvWriter = new(StreamWriter, CultureInfo.InvariantCulture);

            CsvWriter.WriteRecords(ListRoleMenu);
        }

        public void ExportToPDF(string path, List<RoleMenu> ListRoleMenu)
        {
            string ProjectName = "FiyiStack";

            string Table = "RoleMenu";

            ChromePdfRenderer ChromePdfRenderer = new();

            string RowsAsHTML = "";

            foreach (RoleMenu? RoleMenu in ListRoleMenu)
            {
                RowsAsHTML += $@"
<tr>
    
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.RoleMenuId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.MenuId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RoleMenu.RoleId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RoleMenuId</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">MenuId</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RoleId</span>
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