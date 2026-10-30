using FiyiStack.Areas.CMS.MenuBack.Entities;

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
    public interface IMenuImportationService
    {
        List<Menu> ImportExcel(string path, long userId);
    }
}