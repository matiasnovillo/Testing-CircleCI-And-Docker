using FiyiStack.Areas.CMS.RoleBack.Entities;
using FiyiStack.Areas.CMS.RoleBack.DTOs;
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

namespace FiyiStack.Areas.CMS.RoleBack.Interfaces
{
    public partial interface IRoleRepository
    {
        #region Async Queries
        Task<int> CountAsync();

        Task<Role?> GetOneByRoleIdAsync(long roleId);

        Task<List<Role>> GetAllAsync();

        Task<List<Role>> GetAllByRoleIdCheckedAsync(List<long> listCheckedRoleIds);

        Task<List<Role>> GetAllByRoleIdForModalAsync(string textToSearch);

        Task<PaginatedRoleDTO> GetAllByRoleIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Async Non-Queries
        Task<int> AddAsync(Role role);

        Task<int> AddRangeAsync(List<Role> listRole);

        Task<int> UpdateAsync(Role role);

        Task<int> DeleteOneByRoleIdAsync(long roleId);

        Task<int> DeleteAllAsync();

        Task<int> DeleteManyAsync(List<Role> listRole);
        #endregion

        #region Methods for DataTable
        Task<DataTable> GetAllByRoleIdInDataTableAsync(List<long> listCheckedRoleIds);

        Task<DataTable> GetAllInDataTableAsync();
        #endregion
    }
}