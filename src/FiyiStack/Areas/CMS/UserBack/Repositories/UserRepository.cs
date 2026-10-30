using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.UserBack.DTOs;
using FiyiStack.Areas.CMS.UserBack.Interfaces;
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

namespace FiyiStack.Areas.CMS.UserBack.Repositories
{
    public partial class UserRepository(
        IDbContextFactory<FiyiStackDbContext> _factory) : IUserRepository
    {
        #region Async Queries    
        public async Task<int> CountAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();                
            
            int RowsNumber = await DbContext.User
                .AsNoTracking()
                .CountAsync();

            return RowsNumber;
        }

        public async Task<User?> GetOneByUserIdAsync(long userId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();
                
            User? User = await DbContext.User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            return User;
        }

        public async Task<List<User>> GetAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<User> ListUser = await DbContext.User
                .AsNoTracking()
                .ToListAsync();

            return ListUser;
        }

        public async Task<List<User>> GetAllByUserIdCheckedAsync(List<long> listCheckedUserIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<User> ListUser = await DbContext.User
                .AsNoTracking()
                .Where(x => listCheckedUserIds.Contains(x.UserId))
                .ToListAsync();

            return ListUser;
        }

        public async Task<List<User>> GetAllByUserIdForModalAsync(string textToSearch)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<User> ListUser = await DbContext.User
                .AsNoTracking()
                .Where(x => x.UserId.ToString().Contains(textToSearch))
                .ToListAsync();

            return ListUser;
        }

        public async Task<PaginatedUserDTO> GetAllByUserIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

                string[] Words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                List<User> ListUser = await DbContext.User
                    .AsNoTracking()
                    .Where(x => strictSearch ?
                        Words.All(word => x.UserId.ToString().Contains(word)) :
                        Words.Any(word => x.UserId.ToString().Contains(word)))
                    .OrderByDescending(x => x.DateTimeLastModification)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                long TotalRecordsInDatabase = await DbContext.User.LongCountAsync();

                int TotalRecordsInTheList = ListUser.Count;

                List<long> PossibleUserCreationIds = ListUser
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserCreation = await DbContext.User
                    .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                    .ToListAsync();

                List<long> PossibleUserLastModificationIds = ListUser
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserLastModification = await DbContext.User
                    .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                    .ToListAsync();

                return new PaginatedUserDTO
                {
                    ListUser = ListUser,
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
        public async Task<int> AddAsync(User user)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            EntityEntry<User> UserToAdd = await DbContext.User.AddAsync(user);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> AddRangeAsync(List<User> listUser)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            await DbContext.User.AddRangeAsync(listUser);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> UpdateAsync(User user)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.User.Update(user);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteOneByUserIdAsync(long userId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.User
                .Where(x => x.UserId == userId)
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.User
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteManyAsync(List<User> listUser)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.User.RemoveRange(listUser);

            int AffectedRows = await DbContext.SaveChangesAsync();

            return AffectedRows;
        }
        #endregion

        #region Methods for DataTable
        public async Task<DataTable> GetAllByUserIdInDataTableAsync(List<long> listCheckedUserIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DataTable DataTable = new();

            DataTable.Columns.Add("UserId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Email", typeof(string));
            DataTable.Columns.Add("Password", typeof(string));
            DataTable.Columns.Add("RoleId", typeof(string));
            

            var ListCheckedUser = await DbContext.User
                .AsNoTracking()
                .Where(x => listCheckedUserIds.Contains(x.UserId))
                .ToListAsync();

            foreach (User User in ListCheckedUser)
            {
                if (User != null)
                {
                    DataTable.Rows.Add(
                    User.UserId,					User.DateTimeCreation,					User.DateTimeLastModification,					User.UserCreationId,					User.UserLastModificationId,					User.Active,					User.Email,					User.Password,					User.RoleId
                    );
                }
            }                

            return DataTable;
        }

        public async Task<DataTable> GetAllInDataTableAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<User> ListUser = await DbContext.User
                .AsNoTracking()
                .ToListAsync();

            DataTable DataTable = new();
            DataTable.Columns.Add("UserId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Email", typeof(string));
            DataTable.Columns.Add("Password", typeof(string));
            DataTable.Columns.Add("RoleId", typeof(string));
            

            foreach (User User in ListUser)
            {
                DataTable.Rows.Add(
                    User.UserId,					User.DateTimeCreation,					User.DateTimeLastModification,					User.UserCreationId,					User.UserLastModificationId,					User.Active,					User.Email,					User.Password,					User.RoleId
                    );
            }

            return DataTable;
        }
        #endregion
    }
}