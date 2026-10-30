using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.MenuBack.DTOs;
using FiyiStack.Areas.CMS.MenuBack.Interfaces;
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

namespace FiyiStack.Areas.CMS.MenuBack.Repositories
{
    public partial class MenuRepository(
        IDbContextFactory<FiyiStackDbContext> _factory) : IMenuRepository
    {
        #region Async Queries    
        public async Task<int> CountAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();                
            
            int RowsNumber = await DbContext.Menu
                .AsNoTracking()
                .CountAsync();

            return RowsNumber;
        }

        public async Task<Menu?> GetOneByMenuIdAsync(long menuId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();
                
            Menu? Menu = await DbContext.Menu
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MenuId == menuId);

            return Menu;
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Menu> ListMenu = await DbContext.Menu
                .AsNoTracking()
                .ToListAsync();

            return ListMenu;
        }

        public async Task<List<Menu>> GetAllByMenuIdCheckedAsync(List<long> listCheckedMenuIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Menu> ListMenu = await DbContext.Menu
                .AsNoTracking()
                .Where(x => listCheckedMenuIds.Contains(x.MenuId))
                .ToListAsync();

            return ListMenu;
        }

        public async Task<List<Menu>> GetAllByMenuIdForModalAsync(string textToSearch)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Menu> ListMenu = await DbContext.Menu
                .AsNoTracking()
                .Where(x => x.MenuId.ToString().Contains(textToSearch))
                .ToListAsync();

            return ListMenu;
        }

        public async Task<PaginatedMenuDTO> GetAllByMenuIdPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

                string[] Words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                List<Menu> ListMenu = await DbContext.Menu
                    .AsNoTracking()
                    .Where(x => strictSearch ?
                        Words.All(word => x.MenuId.ToString().Contains(word)) :
                        Words.Any(word => x.MenuId.ToString().Contains(word)))
                    .OrderByDescending(x => x.DateTimeLastModification)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                long TotalRecordsInDatabase = await DbContext.Menu.LongCountAsync();

                int TotalRecordsInTheList = ListMenu.Count;

                List<long> PossibleUserCreationIds = ListMenu
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserCreation = await DbContext.User
                    .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                    .ToListAsync();

                List<long> PossibleUserLastModificationIds = ListMenu
                    .Select(x => x.UserCreationId)
                    .Distinct()
                    .ToList();

                List<User> ListUserLastModification = await DbContext.User
                    .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                    .ToListAsync();

                return new PaginatedMenuDTO
                {
                    ListMenu = ListMenu,
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
        public async Task<int> AddAsync(Menu menu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            EntityEntry<Menu> MenuToAdd = await DbContext.Menu.AddAsync(menu);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> AddRangeAsync(List<Menu> listMenu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            await DbContext.Menu.AddRangeAsync(listMenu);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> UpdateAsync(Menu menu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Menu.Update(menu);

            int AffectedRowsNumber = await DbContext.SaveChangesAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteOneByMenuIdAsync(long menuId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Menu
                .Where(x => x.MenuId == menuId)
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteAllAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int AffectedRowsNumber = await DbContext.Menu
                .ExecuteDeleteAsync();

            return AffectedRowsNumber;
        }

        public async Task<int> DeleteManyAsync(List<Menu> listMenu)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DbContext.Menu.RemoveRange(listMenu);

            int AffectedRows = await DbContext.SaveChangesAsync();

            return AffectedRows;
        }
        #endregion

        #region Methods for DataTable
        public async Task<DataTable> GetAllByMenuIdInDataTableAsync(List<long> listCheckedMenuIds)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            DataTable DataTable = new();

            DataTable.Columns.Add("MenuId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Name", typeof(string));
            DataTable.Columns.Add("MenuFatherId", typeof(string));
            DataTable.Columns.Add("URLPath", typeof(string));
            DataTable.Columns.Add("IconURLPath", typeof(string));
            DataTable.Columns.Add("Order", typeof(string));
            

            var ListCheckedMenu = await DbContext.Menu
                .AsNoTracking()
                .Where(x => listCheckedMenuIds.Contains(x.MenuId))
                .ToListAsync();

            foreach (Menu Menu in ListCheckedMenu)
            {
                if (Menu != null)
                {
                    DataTable.Rows.Add(
                    Menu.MenuId,					Menu.DateTimeCreation,					Menu.DateTimeLastModification,					Menu.UserCreationId,					Menu.UserLastModificationId,					Menu.Active,					Menu.Name,					Menu.MenuFatherId,					Menu.URLPath,					Menu.IconURLPath,					Menu.Order
                    );
                }
            }                

            return DataTable;
        }

        public async Task<DataTable> GetAllInDataTableAsync()
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Menu> ListMenu = await DbContext.Menu
                .AsNoTracking()
                .ToListAsync();

            DataTable DataTable = new();
            DataTable.Columns.Add("MenuId", typeof(string));
            DataTable.Columns.Add("DateTimeCreation", typeof(string));
            DataTable.Columns.Add("DateTimeLastModification", typeof(string));
            DataTable.Columns.Add("UserCreationId", typeof(string));
            DataTable.Columns.Add("UserLastModificationId", typeof(string));
            DataTable.Columns.Add("Active", typeof(string));
            DataTable.Columns.Add("Name", typeof(string));
            DataTable.Columns.Add("MenuFatherId", typeof(string));
            DataTable.Columns.Add("URLPath", typeof(string));
            DataTable.Columns.Add("IconURLPath", typeof(string));
            DataTable.Columns.Add("Order", typeof(string));
            

            foreach (Menu Menu in ListMenu)
            {
                DataTable.Rows.Add(
                    Menu.MenuId,					Menu.DateTimeCreation,					Menu.DateTimeLastModification,					Menu.UserCreationId,					Menu.UserLastModificationId,					Menu.Active,					Menu.Name,					Menu.MenuFatherId,					Menu.URLPath,					Menu.IconURLPath,					Menu.Order
                    );
            }

            return DataTable;
        }
        #endregion
    }
}