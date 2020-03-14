using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class QuestionEditModel
    {
        [Required]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}