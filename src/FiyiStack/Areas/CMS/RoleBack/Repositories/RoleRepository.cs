using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.RoleBack.Entities;
using FiyiStack.Areas.CMS.RoleBack.DTOs;
using FiyiStack.Areas.CMS.RoleBack.Interfaces;
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

namespace FiyiStack.Areas.CMS.RoleBack.Repositories
{
    public partial class RoleRepository(
        IDbContextFactory<FiyiStackDbContext> _factory) : IRoleRepository
    {
        #region Async Queries    
        public async Task<int> CountAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();                
            
            int RowsNumber = await DbContext.Role
                .AsNoTracking()
                .CountAsync();

            return RowsNumber;
        }

        public async Task<Role?> GetOneByRoleIdAsync(long roleId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();
                
            Role? Role = await DbContext.Role
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RoleId == roleId);

            return Role;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Role> ListRole = await DbContext.Role
                .AsNoTracking()
                .ToListAsync();

            return ListRole;
        }

        public async Task<List<Role>> GetAllByRoleIdCheckedAsync(List<long> listCheckedRoleIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Role> ListRole = await DbContext.Role
                .AsNoTracking()
                .Where(x => listCheckedRoleIds.Contains(x.RoleId))
                .ToListAsync();

            return ListRole;
        }

        public async Task<List<Role>> GetAllByRoleIdForModalAsync(string textToSearch)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Role> ListRole = await DbContext.Role
                .AsNoTracking()
                .Where(x => x.RoleId.ToString().Contains(textToSearch))
                .ToListAsync();

            return ListRole;
        }

        public async Task<PaginatedRoleDTO> GetAllByRoleIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

                string[] Words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                List<Role> ListRole = await DbContext.Role
                    .AsNoTracking()
                    .Where(x => strictSearch ?
                        Words.All(word => x.RoleId.ToString().Contains(word)) :
                        Words.Any(word => x.RoleId.ToString().Contains(word)))
                    .OrderByDescending(x => x.DateTimeLastModification)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                long TotalRecordsInDatabase = await DbContext.Role.LongCountAsync();

                int TotalRecordsInTheList = ListRole.Count;

                List<long> PossibleUserCreationIds = ListRole
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserCreation = await DbContext.User
                    .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                    .ToListAsync();

                List<long> PossibleUserLastModificationIds = ListRole
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserLastModification = await DbContext.User
                    .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                    .ToListAsync();

                return new PaginatedRoleDTO
                {
                    ListRole = ListRole,
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
        public async Task<int> AddAsync(Role role)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            EntityEntry<Role> RoleToAdd = await DbContext.Role.AddAsync(role);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> AddRangeAsync(List<Role> listRole)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            await DbContext.Role.AddRangeAsync(listRole);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> UpdateAsync(Role role)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Role.Update(role);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteOneByRoleIdAsync(long roleId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Role
                .Where(x => x.RoleId == roleId)
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Role
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteManyAsync(List<Role> listRole)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Role.RemoveRange(listRole);

            int AffectedRows = await DbContext.SaveChangesAsync();

            return AffectedRows;
        }
        #endregion

        #region Methods for DataTable
        public async Task<DataTable> GetAllByRoleIdInDataTableAsync(List<long> listCheckedRoleIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DataTable DataTable = new();

            DataTable.Columns.Add("RoleId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Name", typeof(string));
            

            var ListCheckedRole = await DbContext.Role
                .AsNoTracking()
                .Where(x => listCheckedRoleIds.Contains(x.RoleId))
                .ToListAsync();

            foreach (Role Role in ListCheckedRole)
            {
                if (Role != null)
                {
                    DataTable.Rows.Add(
                    Role.RoleId,					Role.DateTimeCreation,					Role.DateTimeLastModification,					Role.UserCreationId,					Role.UserLastModificationId,					Role.Active,					Role.Name
                    );
                }
            }                

            return DataTable;
        }

        public async Task<DataTable> GetAllInDataTableAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Role> ListRole = await DbContext.Role
                .AsNoTracking()
                .ToListAsync();

            DataTable DataTable = new();
            DataTable.Columns.Add("RoleId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Name", typeof(string));
            

            foreach (Role Role in ListRole)
            {
                DataTable.Rows.Add(
                    Role.RoleId,					Role.DateTimeCreation,					Role.DateTimeLastModification,					Role.UserCreationId,					Role.UserLastModificationId,					Role.Active,					Role.Name
                    );
            }

            return DataTable;
        }
        #endregion
    }
}