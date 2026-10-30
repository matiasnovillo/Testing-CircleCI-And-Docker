using FiyiStack.Areas.CMS.RoleBack.Entities;
using FiyiStack.Areas.CMS.UserBack.DTOs;
using FiyiStack.Areas.CMS.UserBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
    public partial class UserRepository : IUserRepository
    {
        public async Task<User?> GetOneByEmailAndPassword(string email, string password)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            User? User = await DbContext.User
               .Where(x => x.Password == password)
               .Where(x => x.Email == email)
               .FirstOrDefaultAsync();

            return User;
        }

        public async Task<PaginatedUserDTO> GetAllByEmailPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize)
        {
            await using var DbContext = await _factory.CreateDbContextAsync();

            string[] Words = Regex
                .Replace(textToSearch
                .Trim(), @"\s+", " ")
                .Split(" ");

            int TotalRecordsInTheList = await DbContext.User.CountAsync();

            List<User> ListUser = await DbContext.User
                .AsQueryable()
                .Where(x => strictSearch ?
                    Words.All(word => x.Email.Contains(word)) :
                    Words.Any(word => x.Email.Contains(word)))
                .OrderByDescending(x => x.DateTimeLastModification)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<long> PossibleUserCreationIds = DbContext.User
                .Select(x => x.UserCreationId)
                .Distinct()
                .ToList();

            List<User> ListUserCreation = await DbContext.User
                .Where(x => PossibleUserCreationIds.Contains(x.UserId))
                .ToListAsync();

            List<long> PossibleUserLastModificationIds = DbContext.User
                .Select(x => x.UserCreationId)
                .Distinct()
                .ToList();

            List<User> ListUserLastModification = await DbContext.User
                .Where(x => PossibleUserLastModificationIds.Contains(x.UserId))
                .ToListAsync();

            List<long> PossibleRoleIds = DbContext.User
                .Select(x => x.RoleId)
                .Distinct()
                .ToList();

            List<Role> ListRole = await DbContext.Role
                .Where(x => PossibleRoleIds.Contains(x.RoleId))
                .ToListAsync();

            return new PaginatedUserDTO
            {
                ListUser = ListUser,
                ListUserCreation = ListUserCreation,
                ListUserLastModification = ListUserLastModification,
                ListRole = ListRole,
                TotalRecordsInTheList = TotalRecordsInTheList,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}