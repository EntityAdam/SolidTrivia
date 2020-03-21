using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace SolidTrivia.Common.Tests
{
    public class BoardViewModelTests : IClassFixture<BoardFacadeFixture>
    {
        BoardFacadeFixture fixture;

        public BoardViewModelTests(BoardFacadeFixture fixture)
        {
            this.fixture = fixture;
        }
        public IQuestionFacade Facade => fixture.Facade;
        public BoardCreateViewModel CreateVm => fixture.CreateVm;
        public BoardEditViewModel EditVm => fixture.EditVm;
        public BoardDeleteViewModel DeleteVm => fixture.DeleteVm;
        public BoardListViewModel ListVm => fixture.ListVm;

        [Fact]
        public void CreateCommand_CanExecute_Exercise()
        {
            CreateVm.CreateModel.Name = "Board";
            Assert.True(CreateVm.CreateCommand.CanExecute(null));

            CreateVm.CreateModel.Name = "";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));
        }

        [Fact]
        public void CreateCommand_CanExecute_ShouldBeFalse_WithInvalidTags()
        {
            ////XSS
            //CreateVm.CreateModel.Name = "<script>";
            //Assert.False(CreateVm.CreateCommand.CanExecute(null));

            ////invalid chars
            //CreateVm.CreateModel.Name = "tim^tebow";
            //Assert.False(CreateVm.CreateCommand.CanExecute(null));

            //CreateVm.CreateModel.Name = "c++";
            //Assert.False(CreateVm.CreateCommand.CanExecute(null));

            //CreateVm.CreateModel.Name = "c#";
            //Assert.False(CreateVm.CreateCommand.CanExecute(null));
        }

        [Fact]
        public void CreateCommand_CanExecute_ShouldBeTrue_WithSpecialChars()
        { 
        }


        [Fact]
        public void CreateCommand_Execute_ShouldCreateBoards()
        {
            CreateVm.CreateModel.Name = "Board1";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.CreateModel.Name = "Board2";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.CreateModel.Name = "Board3";
            CreateVm.CreateCommand.Execute(null);

            ListVm.Load(pageSize: 25);
            Assert.Contains("Board1", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("Board2", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("Board3", ListVm.Boards.Select(x => x.Name));
        }

        [Fact]
        public void Board_Paging()
        {
            ListVm.Load(pageSize: 3);

            Assert.True(ListVm.NextPageCommand.CanExecute(null));
            Assert.False(ListVm.PrevPageCommand.CanExecute(null));
            Assert.Equal(3, ListVm.Boards.Count());
            Assert.Contains("BoardA", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("BoardB", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("BoardC", ListVm.Boards.Select(x => x.Name));

            ListVm.NextPageCommand.Execute(null);

            Assert.Equal(3, ListVm.Boards.Count());
            Assert.Contains("BoardD", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("BoardE", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("BoardF", ListVm.Boards.Select(x => x.Name));

            Assert.True(ListVm.PrevPageCommand.CanExecute(null));

            ListVm.PrevPageCommand.Execute(null);

            Assert.Equal(3, ListVm.Boards.Count());
            Assert.Contains("BoardA", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("BoardB", ListVm.Boards.Select(x => x.Name));
            Assert.Contains("BoardC", ListVm.Boards.Select(x => x.Name));
        }

        [Fact]
        public void Board_Paging2()
        {
            ListVm.Load(pageSize: 99);
            Assert.False(ListVm.NextPageCommand.CanExecute(null));
            Assert.False(ListVm.PrevPageCommand.CanExecute(null));
        }

        [Fact]
        public void Board_Rename()
        {
            ListVm.Load(pageSize: 99);

            var board = ListVm.Boards.First();
            //Assert.True(board.Id == 1);
            Assert.True(board.Name == "BoardA");

            EditVm.Load(board.Id);

            Assert.True(EditVm.EditModel.NewName == EditVm.EditModel.OldName);
            Assert.False(EditVm.RenameCommand.CanExecute(null));

            EditVm.EditModel.NewName = "BoardZ";
            Assert.True(EditVm.RenameCommand.CanExecute(null));

            EditVm.RenameCommand.Execute(null);

            ListVm.Load(pageSize: 99);
            var renamedBoard = ListVm.Boards.First();
            //Assert.True(renamedBoard.Id == 1);
            Assert.True(renamedBoard.Name == "BoardZ");


            //put it back for paging tests :: fix this later so I can take it out
            EditVm.EditModel.NewName = "BoardA";
            EditVm.RenameCommand.Execute(null);
        }

        [Fact]
        public void Boards_Delete()
        {
            CreateVm.CreateModel.Name = "BoardToDelete";
            CreateVm.CreateCommand.Execute(null);
            ListVm.Load(pageSize: 99);
            Assert.Contains("BoardToDelete", ListVm.Boards.Select(x => x.Name));

            var boardModel = ListVm.Boards.Single(x => x.Name == "BoardToDelete");

            DeleteVm.Load(boardModel.Id);

            Assert.True(DeleteVm.DeleteCommand.CanExecute(null));
            DeleteVm.DeleteCommand.Execute(null);

            ListVm.Load(pageSize: 99);
            Assert.DoesNotContain("BoardToDelete", ListVm.Boards.Select(x => x.Name));
        }
    }
}
