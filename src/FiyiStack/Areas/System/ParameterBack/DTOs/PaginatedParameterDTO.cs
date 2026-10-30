using FiyiStack.Areas.System.ParameterBack.Entities;


/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.System.ParameterBack.DTOs
{
    public record PaginatedParameterDTO
    {
        public List<Parameter> ListParameter { get; init; } = [];
        public List<User> ListUserCreation { get; init; } = [];
        public List<User> ListUserLastModification { get; init; } = [];

        //FOREIGN LISTS (TABLES)
        

        public long TotalRecordsInDatabase { get; set; }
        public int TotalRecordsInTheList { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecordsInTheList / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}