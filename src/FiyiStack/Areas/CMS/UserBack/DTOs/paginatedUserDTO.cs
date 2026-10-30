using FiyiStack.Areas.CMS.UserBack.Entities;
using FiyiStack.Areas.CMS.RoleBack.Entities;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.UserBack.DTOs
{
    public record PaginatedUserDTO
    {
        public List<User> ListUser { get; init; } = [];
        public List<User> ListUserCreation { get; init; } = [];
        public List<User> ListUserLastModification { get; init; } = [];

        //FOREIGN LISTS (TABLES)
        public List<Role> ListRole { get; init; } = [];
        

        public long TotalRecordsInDatabase { get; set; }
        public int TotalRecordsInTheList { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecordsInTheList / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}