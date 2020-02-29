using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewVote
    {
        public int QuestionId { get; set; }
        public bool? Value { get; set; }
        public string UserId { get; set; }
    }
}
