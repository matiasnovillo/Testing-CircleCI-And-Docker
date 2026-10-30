using FiyiStack.Areas.CMS.UserBack.Entities;
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

namespace FiyiStack.Areas.CMS.UserBack.Interfaces
{
    public interface IUserExportationService
    {
        void ExportToExcel(string path, DataTable dataTableUser);

        void ExportToCSV(string path, List<User> listUser);

        void ExportToPDF(string path, List<User> listUser);
    }
}