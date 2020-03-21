using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewQuestion
    {
        public Guid Id { get; set; }
        public string MarkdownContent { get; set; }
        public NewQuestionType QuestionType { get; set; }
        public string CorrectResponse { get; set; }
        public string Category { get; set; }
    }
}
