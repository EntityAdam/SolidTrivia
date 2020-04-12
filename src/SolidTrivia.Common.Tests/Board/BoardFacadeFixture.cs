using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;

namespace SolidTrivia.Common.Tests
{
    public class BoardFacadeFixture : IDisposable
    {

        public BoardFacadeFixture()
        {
            Facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreMock(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock());
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            //nothing to dispose
        }
    }
}
