using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface IBoardStore
    {
        void Create(string name);
        NewBoard GetBoardByName(string name);
    }
}
