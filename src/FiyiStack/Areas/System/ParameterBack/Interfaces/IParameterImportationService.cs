using FiyiStack.Areas.System.ParameterBack.Entities;

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
    public interface IParameterImportationService
    {
        List<Parameter> ImportExcel(string path, long userId);
    }
}