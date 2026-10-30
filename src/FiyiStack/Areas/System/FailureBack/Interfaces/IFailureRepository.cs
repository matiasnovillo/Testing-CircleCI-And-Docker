using FiyiStack.Areas.System.FailureBack.Entities;
using FiyiStack.Areas.System.FailureBack.DTOs;
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

namespace FiyiStack.Areas.System.FailureBack.Interfaces
{
    public partial interface IFailureRepository
    {
        #region Async Queries
        Task<int> CountAsync();

        Task<Failure?> GetOneByFailureIdAsync(long failureId);

        Task<List<Failure>> GetAllAsync();

        Task<List<Failure>> GetAllByFailureIdCheckedAsync(List<long> listCheckedFailureIds);

        Task<List<Failure>> GetAllByFailureIdForModalAsync(string textToSearch);

        Task<PaginatedFailureDTO> GetAllByFailureIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Async Non-Queries
        Task<int> AddAsync(Failure failure);

        Task<int> AddRangeAsync(List<Failure> listFailure);

        Task<int> UpdateAsync(Failure failure);

        Task<int> DeleteOneByFailureIdAsync(long failureId);

        Task<int> DeleteAllAsync();

        Task<int> DeleteManyAsync(List<Failure> listFailure);
        #endregion

        #region Methods for DataTable
        Task<DataTable> GetAllByFailureIdInDataTableAsync(List<long> listCheckedFailureIds);

        Task<DataTable> GetAllInDataTableAsync();
        #endregion
    }
}