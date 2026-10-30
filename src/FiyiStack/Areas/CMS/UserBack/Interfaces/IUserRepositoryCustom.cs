/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

using FiyiStack.Areas.CMS.UserBack.DTOs;

namespace FiyiStack.Areas.CMS.UserBack.Interfaces
{
    public partial interface IUserRepository
    {
        //Put your custom code here
        Task<User?> GetOneByEmailAndPassword(string email, string password);

        Task<PaginatedUserDTO> GetAllByEmailPaginatedAsync(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
    }
}