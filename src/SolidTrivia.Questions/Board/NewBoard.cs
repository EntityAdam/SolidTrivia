using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewBoard
    {
        public List<NewCategory> Categories { get; set; } = new List<NewCategory>();
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
