using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface ICategoryStore
    {

        void Create(string categoryName);
        void Delete(int categoryId);
        void RemoveFromBoard(int boardId, int categoryId);
        NewCategory GetById(int categoryId);
        IEnumerable<NewCategory> ListCategories();
        IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId);
        void AddToBoard(int boardId, int categoryId);
        bool Exists(int categoryId);
        void Rename(int categoryId, string newName);
        bool ExistsOrdinalIgnoreCase(string categoryName);
        IEnumerable<NewCategory> ListAvailable(int boardId);
    }
}
