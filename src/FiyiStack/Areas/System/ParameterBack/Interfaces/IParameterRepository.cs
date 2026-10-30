using FiyiStack.Areas.System.ParameterBack.Entities;
using FiyiStack.Areas.System.ParameterBack.DTOs;
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

namespace FiyiStack.Areas.System.ParameterBack.Interfaces
{
    public partial interface IParameterRepository
    {
        #region Async Queries
        Task<int> CountAsync();

        Task<Parameter?> GetOneByParameterIdAsync(long parameterId);

        Task<List<Parameter>> GetAllAsync();

        Task<List<Parameter>> GetAllByParameterIdCheckedAsync(List<long> listCheckedParameterIds);

        Task<List<Parameter>> GetAllByParameterIdForModalAsync(string textToSearch);

        Task<PaginatedParameterDTO> GetAllByParameterIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Async Non-Queries
        Task<int> AddAsync(Parameter parameter);

        Task<int> AddRangeAsync(List<Parameter> listParameter);

        Task<int> UpdateAsync(Parameter parameter);

        Task<int> DeleteOneByParameterIdAsync(long parameterId);

        Task<int> DeleteAllAsync();

        Task<int> DeleteManyAsync(List<Parameter> listParameter);
        #endregion

        #region Methods for DataTable
        Task<DataTable> GetAllByParameterIdInDataTableAsync(List<long> listCheckedParameterIds);

        Task<DataTable> GetAllInDataTableAsync();
        #endregion
    }
}