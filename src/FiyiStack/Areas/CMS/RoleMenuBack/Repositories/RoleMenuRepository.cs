using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.DTOs;
using FiyiStack.Areas.CMS.RoleMenuBack.Interfaces;
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

namespace FiyiStack.Areas.CMS.RoleMenuBack.Repositories
{
    public partial class RoleMenuRepository(
        IDbContextFactory<FiyiStackDbContext> _factory) : IRoleMenuRepository
    {
        #region Async Queries    
        public async Task<int> CountAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();                
            
            int RowsNumber = await DbContext.RoleMenu
                .AsNoTracking()
                .CountAsync();

            return RowsNumber;
        }

        public async Task<RoleMenu?> GetOneByRoleMenuIdAsync(long rolemenuId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();
                
            RoleMenu? RoleMenu = await DbContext.RoleMenu
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RoleMenuId == rolemenuId);

            return RoleMenu;
        }

        public async Task<List<RoleMenu>> GetAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<RoleMenu> ListRoleMenu = await DbContext.RoleMenu
                .AsNoTracking()
                .ToListAsync();

            return ListRoleMenu;
        }

        public async Task<List<RoleMenu>> GetAllByRoleMenuIdCheckedAsync(List<long> listCheckedRoleMenuIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<RoleMenu> ListRoleMenu = await DbContext.RoleMenu
                .AsNoTracking()
                .Where(x => listCheckedRoleMenuIds.Contains(x.RoleMenuId))
                .ToListAsync();

            return ListRoleMenu;
        }

        public async Task<List<RoleMenu>> GetAllByRoleMenuIdForModalAsync(string textToSearch)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<RoleMenu> ListRoleMenu = await DbContext.RoleMenu
                .AsNoTracking()
                .Where(x => x.RoleMenuId.ToString().Contains(textToSearch))
                .ToListAsync();

            return ListRoleMenu;
        }

        public async Task<PaginatedRoleMenuDTO> GetAllByRoleMenuIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

                string[] Words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                List<RoleMenu> ListRoleMenu = await DbContext.RoleMenu
                    .AsNoTracking()
                    .Where(x => strictSearch ?
                        Words.All(word => x.RoleMenuId.ToString().Contains(word)) :
                        Words.Any(word => x.RoleMenuId.ToString().Contains(word)))
                    .OrderByDescending(x => x.DateTimeLastModification)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                long TotalRecordsInDatabase = await DbContext.RoleMenu.LongCountAsync();

                int TotalRecordsInTheList = ListRoleMenu.Count;

                List<long> PossibleUserCreationIds = ListRoleMenu
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserCreation = await DbContext.User
                    .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                    .ToListAsync();

                List<long> PossibleUserLastModificationIds = ListRoleMenu
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserLastModification = await DbContext.User
                    .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                    .ToListAsync();

                return new PaginatedRoleMenuDTO
                {
                    ListRoleMenu = ListRoleMenu,
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
        public async Task<int> AddAsync(RoleMenu rolemenu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            EntityEntry<RoleMenu> RoleMenuToAdd = await DbContext.RoleMenu.AddAsync(rolemenu);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> AddRangeAsync(List<RoleMenu> listRoleMenu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            await DbContext.RoleMenu.AddRangeAsync(listRoleMenu);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> UpdateAsync(RoleMenu rolemenu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.RoleMenu.Update(rolemenu);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteOneByRoleMenuIdAsync(long rolemenuId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.RoleMenu
                .Where(x => x.RoleMenuId == rolemenuId)
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.RoleMenu
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteManyAsync(List<RoleMenu> listRoleMenu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.RoleMenu.RemoveRange(listRoleMenu);

            int AffectedRows = await DbContext.SaveChangesAsync();

            return AffectedRows;
        }
        #endregion

        #region Methods for DataTable
        public async Task<DataTable> GetAllByRoleMenuIdInDataTableAsync(List<long> listCheckedRoleMenuIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DataTable DataTable = new();

            DataTable.Columns.Add("RoleMenuId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("MenuId", typeof(string));
            DataTable.Columns.Add("RoleId", typeof(string));
            

            var ListCheckedRoleMenu = await DbContext.RoleMenu
                .AsNoTracking()
                .Where(x => listCheckedRoleMenuIds.Contains(x.RoleMenuId))
                .ToListAsync();

            foreach (RoleMenu RoleMenu in ListCheckedRoleMenu)
            {
                if (RoleMenu != null)
                {
                    DataTable.Rows.Add(
                    RoleMenu.RoleMenuId,					RoleMenu.DateTimeCreation,					RoleMenu.DateTimeLastModification,					RoleMenu.UserCreationId,					RoleMenu.UserLastModificationId,					RoleMenu.Active,					RoleMenu.MenuId,					RoleMenu.RoleId
                    );
                }
            }                

            return DataTable;
        }

        public async Task<DataTable> GetAllInDataTableAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<RoleMenu> ListRoleMenu = await DbContext.RoleMenu
                .AsNoTracking()
                .ToListAsync();

            DataTable DataTable = new();
            DataTable.Columns.Add("RoleMenuId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("MenuId", typeof(string));
            DataTable.Columns.Add("RoleId", typeof(string));
            

            foreach (RoleMenu RoleMenu in ListRoleMenu)
            {
                DataTable.Rows.Add(
                    RoleMenu.RoleMenuId,					RoleMenu.DateTimeCreation,					RoleMenu.DateTimeLastModification,					RoleMenu.UserCreationId,					RoleMenu.UserLastModificationId,					RoleMenu.Active,					RoleMenu.MenuId,					RoleMenu.RoleId
                    );
            }

            return DataTable;
        }
        #endregion
    }
}