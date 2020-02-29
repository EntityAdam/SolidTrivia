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

        public NewBoard GetBoardByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}