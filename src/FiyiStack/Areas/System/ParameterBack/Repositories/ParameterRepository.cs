using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.System.ParameterBack.Entities;
using FiyiStack.Areas.System.ParameterBack.DTOs;
using FiyiStack.Areas.System.ParameterBack.Interfaces;
using FiyiStack.DatabaseContexts;
using System.Text.RegularExpressions;
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

namespace FiyiStack.Areas.System.ParameterBack.Repositories
{
    public partial class ParameterRepository(
        IDbContextFactory<FiyiStackDbContext> _factory) : IParameterRepository
    {
        #region Async Queries    
        public async Task<int> CountAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();                
            
            int RowsNumber = await DbContext.Parameter
                .AsNoTracking()
                .CountAsync();

            return RowsNumber;
        }

        public async Task<Parameter?> GetOneByParameterIdAsync(long parameterId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();
                
            Parameter? Parameter = await DbContext.Parameter
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ParameterId == parameterId);

            return Parameter;
        }

        public async Task<List<Parameter>> GetAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Parameter> ListParameter = await DbContext.Parameter
                .AsNoTracking()
                .ToListAsync();

            return ListParameter;
        }

        public async Task<List<Parameter>> GetAllByParameterIdCheckedAsync(List<long> listCheckedParameterIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Parameter> ListParameter = await DbContext.Parameter
                .AsNoTracking()
                .Where(x => listCheckedParameterIds.Contains(x.ParameterId))
                .ToListAsync();

            return ListParameter;
        }

        public async Task<List<Parameter>> GetAllByParameterIdForModalAsync(string textToSearch)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Parameter> ListParameter = await DbContext.Parameter
                .AsNoTracking()
                .Where(x => x.ParameterId.ToString().Contains(textToSearch))
                .ToListAsync();

            return ListParameter;
        }

        public async Task<PaginatedParameterDTO> GetAllByParameterIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

                string[] Words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                List<Parameter> ListParameter = await DbContext.Parameter
                    .AsNoTracking()
                    .Where(x => strictSearch ?
                        Words.All(word => x.ParameterId.ToString().Contains(word)) :
                        Words.Any(word => x.ParameterId.ToString().Contains(word)))
                    .OrderByDescending(x => x.DateTimeLastModification)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                long TotalRecordsInDatabase = await DbContext.Parameter.LongCountAsync();

                int TotalRecordsInTheList = ListParameter.Count;

                List<long> PossibleUserCreationIds = ListParameter
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserCreation = await DbContext.User
                    .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                    .ToListAsync();

                List<long> PossibleUserLastModificationIds = ListParameter
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserLastModification = await DbContext.User
                    .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                    .ToListAsync();

                return new PaginatedParameterDTO
                {
                    ListParameter = ListParameter,
                    ListUserCreation = ListUserCreation,
                    ListUserLastModification = ListUserLastModification,
                    TotalRecordsInDatabase = TotalRecordsInDatabase,
                    TotalRecordsInTheList = TotalRecordsInTheList,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
        }
        #endregion

        #region Async Non-Queries
        public async Task<int> AddAsync(Parameter parameter)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            EntityEntry<Parameter> ParameterToAdd = await DbContext.Parameter.AddAsync(parameter);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> AddRangeAsync(List<Parameter> listParameter)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            await DbContext.Parameter.AddRangeAsync(listParameter);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> UpdateAsync(Parameter parameter)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Parameter.Update(parameter);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteOneByParameterIdAsync(long parameterId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Parameter
                .Where(x => x.ParameterId == parameterId)
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Parameter
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteManyAsync(List<Parameter> listParameter)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Parameter.RemoveRange(listParameter);

            int AffectedRows = await DbContext.SaveChangesAsync();

            return AffectedRows;
        }
        #endregion

        #region Methods for DataTable
        public async Task<DataTable> GetAllByParameterIdInDataTableAsync(List<long> listCheckedParameterIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DataTable DataTable = new();

            DataTable.Columns.Add("ParameterId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Name", typeof(string));
            DataTable.Columns.Add("Value", typeof(string));
            DataTable.Columns.Add("IsPrivate", typeof(string));
            

            var ListCheckedParameter = await DbContext.Parameter
                .AsNoTracking()
                .Where(x => listCheckedParameterIds.Contains(x.ParameterId))
                .ToListAsync();

            foreach (Parameter Parameter in ListCheckedParameter)
            {
                if (Parameter != null)
                {
                    DataTable.Rows.Add(
                    Parameter.ParameterId,					Parameter.DateTimeCreation,					Parameter.DateTimeLastModification,					Parameter.UserCreationId,					Parameter.UserLastModificationId,					Parameter.Active,					Parameter.Name,					Parameter.Value,					Parameter.IsPrivate
                    );
                }
            }                

            return DataTable;
        }

        public async Task<DataTable> GetAllInDataTableAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Parameter> ListParameter = await DbContext.Parameter
                .AsNoTracking()
                .ToListAsync();

            DataTable DataTable = new();
            DataTable.Columns.Add("ParameterId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Name", typeof(string));
            DataTable.Columns.Add("Value", typeof(string));
            DataTable.Columns.Add("IsPrivate", typeof(string));
            

            foreach (Parameter Parameter in ListParameter)
            {
                DataTable.Rows.Add(
                    Parameter.ParameterId,					Parameter.DateTimeCreation,					Parameter.DateTimeLastModification,					Parameter.UserCreationId,					Parameter.UserLastModificationId,					Parameter.Active,					Parameter.Name,					Parameter.Value,					Parameter.IsPrivate
                    );
            }

            return DataTable;
        }
        #endregion
    }
}