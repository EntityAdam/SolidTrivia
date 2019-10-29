using System;

namespace SolidTrivia.Game
{

    public class Answer
    {
        public Answer(string body, string category, int value, string[] acceptableResponses)
        {
            AnswerText = body;
            AcceptableResponses = acceptableResponses;
            Category = category;
            Value = value;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Category { get; set; }

        public int Value { get; set; }

        public string AnswerText { get; set; }

        public string[] AcceptableResponses { get; set; }

        public bool IsAnswering { get; set; }

        public bool IsAnswered { get; set; }

        public void MarkAsAnswered()
        {
            this.IsAnswering = false;
            this.IsAnswered = true;
        }
    }
}
