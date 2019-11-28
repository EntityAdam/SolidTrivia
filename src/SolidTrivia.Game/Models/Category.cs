using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Game.Models
{
    public class Category
    {
        private readonly IEnumerable<Prompt> answers;

        public Category(string title, IEnumerable<Prompt> answers)
        {
            this.Title = title;
            this.answers = answers;
        }

        public string Title { get; private set; }

        public IEnumerable<Prompt> Answers => answers.OrderBy(a => a.Weight);
    }
}