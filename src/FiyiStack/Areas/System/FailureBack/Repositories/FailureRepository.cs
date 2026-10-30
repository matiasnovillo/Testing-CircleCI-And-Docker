using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.System.FailureBack.Entities;
using FiyiStack.Areas.System.FailureBack.DTOs;
using FiyiStack.Areas.System.FailureBack.Interfaces;
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

namespace FiyiStack.Areas.System.FailureBack.Repositories
{
    public partial class FailureRepository(
        IDbContextFactory<FiyiStackDbContext> _factory) : IFailureRepository
    {
        #region Async Queries    
        public async Task<int> CountAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();                
            
            int RowsNumber = await DbContext.Failure
                .AsNoTracking()
                .CountAsync();

            return RowsNumber;
        }

        public async Task<Failure?> GetOneByFailureIdAsync(long failureId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();
                
            Failure? Failure = await DbContext.Failure
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FailureId == failureId);

            return Failure;
        }

        public async Task<List<Failure>> GetAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Failure> ListFailure = await DbContext.Failure
                .AsNoTracking()
                .ToListAsync();

            return ListFailure;
        }

        public async Task<List<Failure>> GetAllByFailureIdCheckedAsync(List<long> listCheckedFailureIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Failure> ListFailure = await DbContext.Failure
                .AsNoTracking()
                .Where(x => listCheckedFailureIds.Contains(x.FailureId))
                .ToListAsync();

            return ListFailure;
        }

        public async Task<List<Failure>> GetAllByFailureIdForModalAsync(string textToSearch)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Failure> ListFailure = await DbContext.Failure
                .AsNoTracking()
                .Where(x => x.FailureId.ToString().Contains(textToSearch))
                .ToListAsync();

            return ListFailure;
        }

        public async Task<PaginatedFailureDTO> GetAllByFailureIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

                string[] Words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                List<Failure> ListFailure = await DbContext.Failure
                    .AsNoTracking()
                    .Where(x => strictSearch ?
                        Words.All(word => x.FailureId.ToString().Contains(word)) :
                        Words.Any(word => x.FailureId.ToString().Contains(word)))
                    .OrderByDescending(x => x.DateTimeLastModification)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                long TotalRecordsInDatabase = await DbContext.Failure.LongCountAsync();

                int TotalRecordsInTheList = ListFailure.Count;

                List<long> PossibleUserCreationIds = ListFailure
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserCreation = await DbContext.User
                    .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                    .ToListAsync();

                List<long> PossibleUserLastModificationIds = ListFailure
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserLastModification = await DbContext.User
                    .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                    .ToListAsync();

                return new PaginatedFailureDTO
                {
                    ListFailure = ListFailure,
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
        public async Task<int> AddAsync(Failure failure)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            EntityEntry<Failure> FailureToAdd = await DbContext.Failure.AddAsync(failure);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> AddRangeAsync(List<Failure> listFailure)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            await DbContext.Failure.AddRangeAsync(listFailure);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> UpdateAsync(Failure failure)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Failure.Update(failure);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteOneByFailureIdAsync(long failureId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Failure
                .Where(x => x.FailureId == failureId)
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Failure
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteManyAsync(List<Failure> listFailure)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Failure.RemoveRange(listFailure);

            int AffectedRows = await DbContext.SaveChangesAsync();

            return AffectedRows;
        }
        #endregion

        #region Methods for DataTable
        public async Task<DataTable> GetAllByFailureIdInDataTableAsync(List<long> listCheckedFailureIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DataTable DataTable = new();

            DataTable.Columns.Add("FailureId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Message", typeof(string));
            DataTable.Columns.Add("EmergencyLevel", typeof(string));
            DataTable.Columns.Add("StackTrace", typeof(string));
            DataTable.Columns.Add("Source", typeof(string));
            DataTable.Columns.Add("Comment", typeof(string));
            

            var ListCheckedFailure = await DbContext.Failure
                .AsNoTracking()
                .Where(x => listCheckedFailureIds.Contains(x.FailureId))
                .ToListAsync();

            foreach (Failure Failure in ListCheckedFailure)
            {
                if (Failure != null)
                {
                    DataTable.Rows.Add(
                    Failure.FailureId,					Failure.DateTimeCreation,					Failure.DateTimeLastModification,					Failure.UserCreationId,					Failure.UserLastModificationId,					Failure.Active,					Failure.Message,					Failure.EmergencyLevel,					Failure.StackTrace,					Failure.Source,					Failure.Comment
                    );
                }
            }                

            return DataTable;
        }

        public async Task<DataTable> GetAllInDataTableAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Failure> ListFailure = await DbContext.Failure
                .AsNoTracking()
                .ToListAsync();

            DataTable DataTable = new();
            DataTable.Columns.Add("FailureId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Message", typeof(string));
            DataTable.Columns.Add("EmergencyLevel", typeof(string));
            DataTable.Columns.Add("StackTrace", typeof(string));
            DataTable.Columns.Add("Source", typeof(string));
            DataTable.Columns.Add("Comment", typeof(string));
            

            foreach (Failure Failure in ListFailure)
            {
                DataTable.Rows.Add(
                    Failure.FailureId,					Failure.DateTimeCreation,					Failure.DateTimeLastModification,					Failure.UserCreationId,					Failure.UserLastModificationId,					Failure.Active,					Failure.Message,					Failure.EmergencyLevel,					Failure.StackTrace,					Failure.Source,					Failure.Comment
                    );
            }

            return DataTable;
        }
        #endregion
    }
}