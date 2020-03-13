using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class QuestionListModel
    {
        [Required]
        public int Id { get; set; }
    }
}
