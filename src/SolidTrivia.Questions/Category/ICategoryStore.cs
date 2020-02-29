using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface ICategoryStore
    {
        void Create(string categoryName);
        void DeleteCategory(int categoryId);
        void DeleteCategoryOfBoard(int boardId, int categoryId);

        NewCategory GetCategory(int categoryId);
        NewCategory GetCategoryOfBoard(int boardId, int categoryId);


        IEnumerable<NewCategory> ListCategories();
        IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId);
        void AddCategoryToBoard(int boardId, int categoryId);
        bool Exists(int categoryId);
    }
}
