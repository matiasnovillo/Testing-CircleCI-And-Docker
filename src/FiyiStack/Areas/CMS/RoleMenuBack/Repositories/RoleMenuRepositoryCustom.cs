using FiyiStack.Areas.CMS.MenuBack.DTOs;
using FiyiStack.Areas.CMS.MenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.Entities;
using FiyiStack.Areas.CMS.RoleMenuBack.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    public partial class RoleMenuRepository : IRoleMenuRepository
    {
        public async Task<List<Menu>> GetAllByRoleIdAsync(long roleId, List<Menu> ListMenu)
        {
            List<RoleMenu> ListRoleMenu = await GetAllAsync();

            List<Menu> ListMenuResult =
                (from rm in ListRoleMenu
                 where rm.RoleId == roleId
                 join m in ListMenu on rm.MenuId equals m.MenuId
                 select m)
                .OrderBy(x => x.Order)
                .ToList();

            return ListMenuResult;
        }

        public async Task<List<Menu>> GetAllByRoleIdAndPathForPermissionAsync(long roleId, string path)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            var Query = from rolemenu in DbContext.RoleMenu
                        where rolemenu.RoleId == roleId
                        join menu in DbContext.Menu on rolemenu.MenuId equals menu.MenuId
                        where menu.URLPath == path
                        select new
                        {
                            Menu = menu,
                        };

            List<Menu> ListMenu = await Query
                .Select(result => result.Menu)
                .ToListAsync();

            return ListMenu;
        }


        public async Task<List<FolderAndPagesForCMSDTO>> GetAllPagesAndFoldersForCMSByRoleIdAsync(long roleId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            List<Menu> ListMenu = await (from rm in DbContext.RoleMenu
                                         where rm.RoleId == roleId
                                         join m in DbContext.Menu on rm.MenuId equals m.MenuId
                                         select m)
                                      .OrderBy(x => x.Order)
                                      .ToListAsync();

            List<FolderAndPagesForCMSDTO> ListFolderAndPagesForCMSDTO = ListMenu
                .Where(item => item.MenuFatherId == 0)
                .Select(folder => new FolderAndPagesForCMSDTO
                {
                    Folder = folder,
                    Pages = ListMenu.Where(page => page.MenuFatherId == folder.MenuId).ToList()
                })
                .ToList();

            return ListFolderAndPagesForCMSDTO;

        }

        public async Task<List<Menu>> GetAllWithStateByRoleIdAsync(long roleId, List<Menu> listMenu)
        {
            List<RoleMenu> ListRoleMenu = await GetAllAsync();

            List<Menu> ListMenuResult = listMenu
                .Select(menu =>
                    new Menu
                    {
                        MenuId = menu.MenuId,
                        Name = menu.Name,
                        MenuFatherId = menu.MenuFatherId,
                        Order = menu.Order,
                        URLPath = menu.URLPath,
                        IconURLPath = menu.IconURLPath,
                        Active = menu.Active,
                        IsAvailableForUser = ListRoleMenu
                            .Any(x => x.RoleId == roleId && x.MenuId == menu.MenuId)
                    }
                )
                .ToList();

            return ListMenuResult;
        }

        public async Task<int> DeleteByMenuIdAndRoleIdAsync(long roleId,
            long menuId)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            int RowsAffectedNumber = await DbContext.RoleMenu
                .Where(x => x.MenuId == menuId)
                .Where(x => x.RoleId == roleId)
                .ExecuteDeleteAsync();

            return RowsAffectedNumber;
        }
    }
}