using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
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

namespace FiyiStack.Areas.CMS.RoleMenuBack.Interfaces
{
    public interface IRoleMenuExportationService
    {
        void ExportToExcel(string path, DataTable dataTableRoleMenu);

        void ExportToCSV(string path, List<RoleMenu> listRoleMenu);

        void ExportToPDF(string path, List<RoleMenu> listRoleMenu);
    }
}