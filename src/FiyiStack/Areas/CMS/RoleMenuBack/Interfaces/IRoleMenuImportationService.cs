using FiyiStack.Areas.CMS.RoleMenuBack.Entities;

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
    public interface IRoleMenuImportationService
    {
        List<RoleMenu> ImportExcel(string path, long userId);
    }
}