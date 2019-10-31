using SolidTrivia.Game.Data;
using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Game.Models
{
    public class AnswerBoard : BindableBase
    {
        public AnswerBoard(string title, int round)
        {
            this.Title = title;
            this.Answers = TestData.Answers();
            this.Round = round;
        }

        public bool IsAnswerInProgress
        {
            get
            {
                return Answers.Any(a => a.IsAnswering);
            }
        }

        public int Round { get; private set; }

        public string Title { get; }

        public bool IsComplete { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

        public IEnumerable<string> Categories => Answers.Select(p => p.Category).Distinct();

        public IEnumerable<int> Values => Answers.Select(p => p.Value).Distinct().OrderBy(a => a);

        public Answer SelectNextAnswer(string category, int value) => Answers.FirstOrDefault(a => a.Category == category && a.Value == value);
    }
}