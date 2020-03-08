using SolidTrivia.Questions;
using System.Collections.Generic;

namespace SolidTrivia.Tests
{
    internal class BoardStoreDummy : IBoardStore
    {
        public void Create(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int boardId)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int boardId)
        {
            throw new System.NotImplementedException();
        }

        public NewBoard GetBoardById(int boardId)
        {
            throw new System.NotImplementedException();
        }

        public NewBoard GetBoardByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Rename(int boardId, string newBoardName)
        {
            throw new System.NotImplementedException();
        }
    }
}