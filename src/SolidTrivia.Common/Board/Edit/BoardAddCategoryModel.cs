using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class BoardAddCategoryModel
    {
        public Guid BoardId { get; set; }
        public string Name { get; set; }
    }
}
