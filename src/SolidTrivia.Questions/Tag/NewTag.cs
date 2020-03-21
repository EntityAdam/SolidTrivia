using System;

namespace SolidTrivia.Questions
{
    public class NewTag
    {
        public Guid Id { get; set; }

        public Guid? TagId { get; set; }

        public Guid? QuestionId { get; set; }
        
        public string Name { get; set; }
    }
}