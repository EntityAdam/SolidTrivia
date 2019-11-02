using System;

namespace SolidTrivia.Game.Models
{
    public class Answer : BindableBase
    {
        public Answer(string answerText, int weight, string[] acceptableResponses)
        {
            AnswerText = answerText;
            AcceptableResponses = acceptableResponses;
            Weight = weight;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public int Weight { get; set; }

        public string AnswerText { get; set; }

        public string[] AcceptableResponses { get; set; }

        public bool IsAnswering { get; set; }

        private bool isAnswered;

        public bool IsAnswered
        {
            get => isAnswered;
            set
            {
                if (isAnswered != value)
                {
                    SetField(ref isAnswered, value);
                }
            }
        }

        public void MarkAsAnswered()
        {
            this.IsAnswering = false;
            this.IsAnswered = true;
        }
    }
}