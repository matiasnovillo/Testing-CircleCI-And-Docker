using FiyiStack.Areas.CMS.MenuBack.Entities;
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

namespace FiyiStack.Areas.CMS.MenuBack.Interfaces
{
    public interface IMenuExportationService
    {
        void ExportToExcel(string path, DataTable dataTableMenu);

        void ExportToCSV(string path, List<Menu> listMenu);

        void ExportToPDF(string path, List<Menu> listMenu);
    }
}