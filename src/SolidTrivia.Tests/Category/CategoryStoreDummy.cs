using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class CategoryStoreDummy : ICategoryStore
    {
        public void AddCategoryToBoard(int boardId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Create(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategoryOfBoard(int boardId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int categoryId)
        {
            throw new NotImplementedException();
        }

        public NewCategory GetCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public NewCategory GetCategoryOfBoard(int boardId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId)
        {
            throw new NotImplementedException();
        }
    }
}
