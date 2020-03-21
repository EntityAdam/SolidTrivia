using System;
using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class QuestionEditModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}