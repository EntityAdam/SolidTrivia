using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewComment
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? CommentId { get; set; }
        public string Text { get; set; }
    }
}
