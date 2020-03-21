using SolidTrivia.Questions;
using System;
using System.Collections.Generic;

namespace SolidTrivia.Tests
{
    internal class BoardStoreDummy : IBoardStore
    {
        public void Create(string name)
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

        public NewBoard GetBoardById(Guid id)
        {
            throw new NotImplementedException();
        }

        public NewBoard GetBoardByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewBoard> List()
        {
            throw new NotImplementedException();
        }

        public void Rename(Guid id, string newName)
        {
            throw new NotImplementedException();
        }
    }
}