using ClosedXML.Excel;
using IronPdf;
using CsvHelper;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.UserBack.Interfaces;
using System.Data;
using System.Globalization;

namespace FiyiStack.Areas.CMS.UserBack.Services
{
    public class UserExportationService : IUserExportationService
    {
        public void ExportToExcel(string path, DataTable dataTableUser)
        {
            using XLWorkbook XLWorkbook = new();

            DataTable DataTableUserOriginal = new()
            {
                TableName = "User"
            };

            //We define another DataTable DataTableUserCopy to avoid issue related to DateTime conversion
            DataTable DataTableUserCopy = new()
            {
                TableName = "User"
            };

            #region Define columns for DataTableUserCopy
            DataColumn DataTableColumnUserIdForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserId"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnUserIdForDataTableUserCopy);

            DataColumn DataTableColumnDateTimeCreationForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeCreation"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnDateTimeCreationForDataTableUserCopy);

            DataColumn DataTableColumnDateTimeLastModificationForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeLastModification"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnDateTimeLastModificationForDataTableUserCopy);

            DataColumn DataTableColumnUserCreationIdForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserCreationId"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnUserCreationIdForDataTableUserCopy);

            DataColumn DataTableColumnUserLastModificationIdForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserLastModificationId"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnUserLastModificationIdForDataTableUserCopy);

            DataColumn DataTableColumnActiveForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Active"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnActiveForDataTableUserCopy);

            DataColumn DataTableColumnEmailForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Email"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnEmailForDataTableUserCopy);

            DataColumn DataTableColumnPasswordForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Password"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnPasswordForDataTableUserCopy);

            DataColumn DataTableColumnRoleIdForDataTableUserCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "RoleId"
            };
            DataTableUserCopy.Columns.Add(DataTableColumnRoleIdForDataTableUserCopy);

            
            #endregion

            DataTableUserOriginal = dataTableUser;

            foreach (DataRow DataRow in DataTableUserOriginal.Rows)
            {
                DataTableUserCopy.Rows.Add(DataRow.ItemArray);
            }

            IXLWorksheet XLWorksheet = XLWorkbook.Worksheets.Add(DataTableUserCopy);

            XLWorksheet.ColumnsUsed().AdjustToContents();

            XLWorkbook.SaveAs(path);
        }

        public void ExportToCSV(string path, List<User> ListUser)
        {
            StreamWriter StreamWriter = new(path);

            using CsvWriter CsvWriter = new(StreamWriter, CultureInfo.InvariantCulture);

            CsvWriter.WriteRecords(ListUser);
        }

        public void ExportToPDF(string path, List<User> ListUser)
        {
            string ProjectName = "FiyiStack";

            string Table = "User";

            ChromePdfRenderer ChromePdfRenderer = new();

            string RowsAsHTML = "";

            foreach (User? User in ListUser)
            {
                RowsAsHTML += $@"
<tr>
    
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.UserId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.Email}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.Password}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
    <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{User.RoleId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserId</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Email</span>
        </font>
        <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
    </th>
    
    <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Password</span>
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