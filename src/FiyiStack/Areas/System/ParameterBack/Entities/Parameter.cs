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

namespace FiyiStack.Areas.System.ParameterBack.Entities
{
    public partial class Parameter
    {
        [Key]
        public long ParameterId { get; set; }

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

        [String(nameToShowInUI: "", required: true, minimumLength: 1, maximumLength: 8000, pattern: "")]
        public string Value { get; set; } = string.Empty;

        public bool IsPrivate { get; set; }
    }
}