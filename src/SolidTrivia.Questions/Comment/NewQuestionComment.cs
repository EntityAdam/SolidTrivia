using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewComment
    {
        public Guid Id { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? CommentId { get; set; }
        public string Text { get; set; }
    }
}
