using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface IBoardStore
    {
        void Create(string name);
        NewBoard GetBoardByName(string name);
        bool Exists(Guid id);
        NewBoard GetBoardById(Guid id);
        void Delete(Guid id);
        void Rename(Guid id, string newName);
        IEnumerable<NewBoard> List();
    }
}
