using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.MenuBack.DTOs;
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
    public partial interface IMenuRepository
    {
        #region Async Queries
        Task<int> CountAsync();

        Task<Menu?> GetOneByMenuIdAsync(long menuId);

        Task<List<Menu>> GetAllAsync();

        Task<List<Menu>> GetAllByMenuIdCheckedAsync(List<long> listCheckedMenuIds);

        Task<List<Menu>> GetAllByMenuIdForModalAsync(string textToSearch);

        Task<PaginatedMenuDTO> GetAllByMenuIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Async Non-Queries
        Task<int> AddAsync(Menu menu);

        Task<int> AddRangeAsync(List<Menu> listMenu);

        Task<int> UpdateAsync(Menu menu);

        Task<int> DeleteOneByMenuIdAsync(long menuId);

        Task<int> DeleteAllAsync();

        Task<int> DeleteManyAsync(List<Menu> listMenu);
        #endregion

        #region Methods for DataTable
        Task<DataTable> GetAllByMenuIdInDataTableAsync(List<long> listCheckedMenuIds);

        Task<DataTable> GetAllInDataTableAsync();
        #endregion
    }
}