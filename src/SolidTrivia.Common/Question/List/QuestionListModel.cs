using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class QuestionListModel
    {
        [Required]
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
