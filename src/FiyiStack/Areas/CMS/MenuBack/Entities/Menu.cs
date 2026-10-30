using FiyiStack.Library.ModelAttributeValidator;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public long MenuId { get; set; }

        [DateTimeRange(nameToShowInUI: "", required: true, minimumDate: "0001-01-01T00:00:00.0000000", maximumDate: "9999-12-31T23:59:59.9999999")]
        public DateTime DateTimeCreation { get; set; }

        [DateTimeRange(nameToShowInUI: "", required: true, minimumDate: "0001-01-01T00:00:00.0000000", maximumDate: "9999-12-31T23:59:59.9999999")]
        public DateTime DateTimeLastModification { get; set; }

        [Range(1, long.MaxValue)]
        public long UserCreationId { get; set; }

        [Range(1, long.MaxValue)]
        public long UserLastModificationId { get; set; }

        public bool Active { get; set; }

        [String(nameToShowInUI: "", required: true, minimumLength: 1, maximumLength: 100, pattern: "")]
        public string Name { get; set; } = string.Empty;

        [Long(nameToShowInUI: "", required: true, minimumLong: 0, maximumLong: 9223372036854775807)]        
        public long MenuFatherId { get; set; }

        [String(nameToShowInUI: "", required: false, minimumLength: 1, maximumLength: 2048, pattern: "")]
        public string? URLPath { get; set; }

        [String(nameToShowInUI: "", required: false, minimumLength: 1, maximumLength: 100, pattern: "")]
        public string? IconURLPath { get; set; }

        [Long(nameToShowInUI: "", required: true, minimumLong: 0, maximumLong: 9223372036854775807)]        
        public long Order { get; set; }
    }
}