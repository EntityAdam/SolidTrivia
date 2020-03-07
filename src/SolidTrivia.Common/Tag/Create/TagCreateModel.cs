using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SolidTrivia.Common
{
    public class TagCreateModel
    {
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Tag must be between 2 or more, and 10 or less characters")]
        public string Name { get; set;}
    }
}
