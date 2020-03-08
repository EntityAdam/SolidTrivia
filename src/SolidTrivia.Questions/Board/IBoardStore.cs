using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface IBoardStore
    {
        void Create(string name);
        NewBoard GetBoardByName(string name);
        bool Exists(int boardId);
        NewBoard GetBoardById(int boardId);
        void Delete(int boardId);
        void Rename(int boardId, string newBoardName);
    }
}
