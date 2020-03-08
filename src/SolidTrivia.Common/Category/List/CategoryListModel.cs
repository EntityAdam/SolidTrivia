using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class CategoryListModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
