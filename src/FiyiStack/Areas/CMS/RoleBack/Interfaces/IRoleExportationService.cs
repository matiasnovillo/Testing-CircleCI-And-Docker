using FiyiStack.Areas.CMS.RoleBack.Entities;
using System.Data;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.RoleBack.Interfaces
{
    public interface IRoleExportationService
    {
        void ExportToExcel(string path, DataTable dataTableRole);

        void ExportToCSV(string path, List<Role> listRole);

        void ExportToPDF(string path, List<Role> listRole);
    }
}