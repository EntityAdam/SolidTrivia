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

        public void Create(string name) => Boards.Add(new NewBoard() { Id = Guid.NewGuid(), Name = name });

        public void Delete(Guid boardId) => Boards.RemoveAll(b => b.Id == boardId);

        public bool Exists(Guid boardId) => Boards.Any(b => b.Id == boardId);

        public NewBoard GetBoardById(Guid boardId) => Boards.Single(b => b.Id == boardId);

        public NewBoard GetBoardByName(string name) => Boards.First(b => b.Name == name);

        public IEnumerable<NewBoard> List() => Boards;

        public void Rename(Guid boardId, string newBoardName) => GetBoardById(boardId).Name = newBoardName;
    }
}
