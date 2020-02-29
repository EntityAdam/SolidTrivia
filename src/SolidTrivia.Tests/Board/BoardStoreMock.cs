using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SolidTrivia.Tests
{
    internal class BoardStoreMock : IBoardStore
    {
        public List<NewBoard> Boards { get; set; } = new List<NewBoard>();

        public void Create(string name) => Boards.Add(new NewBoard() { Id = NewId(), Name = name });

        public NewBoard GetBoardByName(string name) => Boards.First(b => b.Name == name);

        private int NewId()
        {
            var id = 1;
            if (Boards.Count() > 0)
            {
                id = Boards.Max(c => c.Id) + 1;
            }
            return id;
        }
    }
}
