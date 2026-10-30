using FiyiStack.Areas.CMS.RoleBack.Entities;

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
    public interface IRoleImportationService
    {
        List<Role> ImportExcel(string path, long userId);
    }
}