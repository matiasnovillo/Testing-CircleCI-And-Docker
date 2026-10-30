using System.ComponentModel.DataAnnotations.Schema;

/*
 * 
 * Coded by fiyistack.com
 * Copyright © 2026
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStack.Areas.CMS.MenuBack.Entities
{
    public partial class Menu
    {
        [NotMapped]
        public bool IsAvailableForUser { get; set; }
    }
}