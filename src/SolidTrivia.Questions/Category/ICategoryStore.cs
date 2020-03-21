using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface ICategoryStore
    {

        void Create(string name);
        void Delete(Guid id);
        void RemoveFromBoard(Guid id, Guid boardId);
        NewCategory GetById(Guid id);
        IEnumerable<NewCategory> ListCategories();
        IEnumerable<NewCategory> ListCategoriesOfBoard(Guid boardId);
        void AddToBoard(Guid id, Guid boardId);
        void Rename(Guid id, string newName);
        bool ExistsOrdinalIgnoreCase(string name);
        IEnumerable<NewCategory> ListAvailable(Guid boardId);
        bool Exists(Guid id);
    }
}
