using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Game.Models
{
    public class Category
    {
        private readonly IEnumerable<Answer> answers;

        public Category(string title, IEnumerable<Answer> answers)
        {
            this.Title = title;
            this.answers = answers;
        }

        public string Title { get; private set; }

        public IEnumerable<Answer> Answers => answers.OrderBy(a => a.Weight);
    }
}