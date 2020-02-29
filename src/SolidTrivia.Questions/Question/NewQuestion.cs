using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewQuestion
    {
        public int Id { get; set; }
        public int? BoardId { get; set; }
        public int? CategoryId { get; set; }
        public bool Difficulty { get; set; }
        public string MarkdownContent { get; set; }
    }
}
