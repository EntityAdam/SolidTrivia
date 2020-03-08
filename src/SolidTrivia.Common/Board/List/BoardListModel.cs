using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class BoardListModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
