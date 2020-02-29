using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public class NewCategory
    {
        public int Id { get; set; }

        public int? BoardId { get; set; }

        public string Name { get; set; }

        public NewCategory Category { get; set; }
    }
}
