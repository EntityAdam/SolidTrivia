using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Game.Models
{
    public class Category
    {
        private readonly IEnumerable<IPrompt> prompts;

        public Category(string title, IEnumerable<IPrompt> prompts)
        {
            this.Title = title;
            this.prompts = prompts;
        }

        public string Title { get; private set; }

        public IEnumerable<IPrompt> Prompts => prompts.OrderBy(a => a.Weight);
    }
}