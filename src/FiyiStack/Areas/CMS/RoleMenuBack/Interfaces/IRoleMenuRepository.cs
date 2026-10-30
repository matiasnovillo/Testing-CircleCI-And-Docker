using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.DTOs;
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
    public partial interface IRoleMenuRepository
    {
        #region Async Queries
        Task<int> CountAsync();

        Task<RoleMenu?> GetOneByRoleMenuIdAsync(long rolemenuId);

        Task<List<RoleMenu>> GetAllAsync();

        Task<List<RoleMenu>> GetAllByRoleMenuIdCheckedAsync(List<long> listCheckedRoleMenuIds);

        Task<List<RoleMenu>> GetAllByRoleMenuIdForModalAsync(string textToSearch);

        Task<PaginatedRoleMenuDTO> GetAllByRoleMenuIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Async Non-Queries
        Task<int> AddAsync(RoleMenu rolemenu);

        Task<int> AddRangeAsync(List<RoleMenu> listRoleMenu);

        Task<int> UpdateAsync(RoleMenu rolemenu);

        Task<int> DeleteOneByRoleMenuIdAsync(long rolemenuId);

        Task<int> DeleteAllAsync();

        Task<int> DeleteManyAsync(List<RoleMenu> listRoleMenu);
        #endregion

        #region Methods for DataTable
        Task<DataTable> GetAllByRoleMenuIdInDataTableAsync(List<long> listCheckedRoleMenuIds);

        Task<DataTable> GetAllInDataTableAsync();
        #endregion
    }
}