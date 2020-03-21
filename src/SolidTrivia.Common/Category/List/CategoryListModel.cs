using System;
using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class CategoryListModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
