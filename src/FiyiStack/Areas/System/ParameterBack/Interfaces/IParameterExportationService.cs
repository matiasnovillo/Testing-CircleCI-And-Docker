using FiyiStack.Areas.System.ParameterBack.Entities;
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

namespace FiyiStack.Areas.System.ParameterBack.Interfaces
{
    public interface IParameterExportationService
    {
        void ExportToExcel(string path, DataTable dataTableParameter);

        void ExportToCSV(string path, List<Parameter> listParameter);

        void ExportToPDF(string path, List<Parameter> listParameter);
    }
}