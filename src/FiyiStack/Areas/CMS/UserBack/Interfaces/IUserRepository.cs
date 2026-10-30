using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.UserBack.DTOs;
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
    public partial interface IUserRepository
    {
        #region Async Queries
        Task<int> CountAsync();

        Task<User?> GetOneByUserIdAsync(long userId);

        Task<List<User>> GetAllAsync();

        Task<List<User>> GetAllByUserIdCheckedAsync(List<long> listCheckedUserIds);

        Task<List<User>> GetAllByUserIdForModalAsync(string textToSearch);

        Task<PaginatedUserDTO> GetAllByUserIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Async Non-Queries
        Task<int> AddAsync(User user);

        Task<int> AddRangeAsync(List<User> listUser);

        Task<int> UpdateAsync(User user);

        Task<int> DeleteOneByUserIdAsync(long userId);

        Task<int> DeleteAllAsync();

        Task<int> DeleteManyAsync(List<User> listUser);
        #endregion

        #region Methods for DataTable
        Task<DataTable> GetAllByUserIdInDataTableAsync(List<long> listCheckedUserIds);

        Task<DataTable> GetAllInDataTableAsync();
        #endregion
    }
}