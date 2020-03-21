using System;
using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class QuestionListModel
    {
        [Required]
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}
