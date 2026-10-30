/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

using FiyiStack.Areas.CMS.MenuBack.Entities;

namespace FiyiStack.Areas.CMS.RoleMenuBack.Interfaces
{
    public partial interface IRoleMenuRepository
    {
        Task<List<Menu>> GetAllByRoleIdAsync(long roleId, List<Menu> listMenu);

        Task<List<Menu>> GetAllByRoleIdAndPathForPermissionAsync(long roleId, string path);

        Task<List<Menu>> GetAllWithStateByRoleIdAsync(long roleId, List<Menu> listMenu);
    }
}