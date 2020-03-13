using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class BoardAddCategoryModel
    {
        public int BoardId { get; set; }

        public int? SelectedCategoryId { get; set; }

        public IEnumerable<CategoryListModel> BoardCategories { get; set; }

        
    }
}
