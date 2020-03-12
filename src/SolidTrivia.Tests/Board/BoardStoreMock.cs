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

        public void Delete(int boardId) => Boards.RemoveAll(b => b.Id == boardId);

        public bool Exists(int boardId) => Boards.Any(b => b.Id == boardId);

        public NewBoard GetBoardById(int boardId) => Boards.Single(b => b.Id == boardId);

        public NewBoard GetBoardByName(string name) => Boards.First(b => b.Name == name);

        public IEnumerable<NewBoard> List() => Boards;

        public void Rename(int boardId, string newBoardName) => GetBoardById(boardId).Name = newBoardName;

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
