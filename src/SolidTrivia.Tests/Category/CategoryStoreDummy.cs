using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class CategoryStoreDummy : ICategoryStore
    {
        public void AddToBoard(Guid id, Guid boardId)
        {
            throw new NotImplementedException();
        }

        public Guid Create(string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsOrdinalIgnoreCase(string name)
        {
            throw new NotImplementedException();
        }

        public NewCategory GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListAvailable(Guid boardId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListCategoriesOfBoard(Guid boardId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromBoard(Guid id, Guid boardId)
        {
            throw new NotImplementedException();
        }

        public void Rename(Guid id, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
