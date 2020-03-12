﻿using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;

namespace SolidTrivia.Common.Tests
{
    public class BoardFacadeFixture : IDisposable
    {

        public BoardFacadeFixture()
        {
            Facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock(), new CommentStoreMock(), new CategoryStoreMock(), new BoardStoreMock());
            CreateVm = new BoardCreateViewModel(Facade);
            EditVm = new BoardEditViewModel(Facade);
            DeleteVm = new BoardDeleteViewModel(Facade);
            ListVm = new BoardListViewModel(Facade);

            CreateCategories();

        }

        private void CreateCategories()
        {
            Facade.CreateBoard("BoardA");
            Facade.CreateBoard("BoardB");
            Facade.CreateBoard("BoardC");
            Facade.CreateBoard("BoardD");
            Facade.CreateBoard("BoardE");
            Facade.CreateBoard("BoardF");
        }

        public IQuestionFacade Facade { get; }
        public BoardCreateViewModel CreateVm { get; }
        public BoardEditViewModel EditVm { get; }
        public BoardDeleteViewModel DeleteVm { get; }
        public BoardListViewModel ListVm { get; }

        public void Dispose()
        {
            //nothing to clean up
        }
    }
}