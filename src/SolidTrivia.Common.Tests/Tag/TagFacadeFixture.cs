using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;

namespace SolidTrivia.Common.Tests
{
    public class TagFacadeFixture : IDisposable
    {

        public TagFacadeFixture()
        {
            Facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreMock(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock());
            CreateVm = new TagCreateViewModel(Facade);
            EditVm = new TagEditViewModel(Facade);
            DeleteVm = new TagDeleteViewModel(Facade);
            ListVm = new TagListViewModel(Facade);

            CreateTags();

        }

        private void CreateTags()
        {
            Facade.CreateTag("TagA");
            Facade.CreateTag("TagB");
            Facade.CreateTag("TagC");
            Facade.CreateTag("TagD");
            Facade.CreateTag("TagE");
            Facade.CreateTag("TagF");
        }

        public IQuestionFacade Facade { get; }
        public TagCreateViewModel CreateVm { get; }
        public TagEditViewModel EditVm { get; }
        public TagDeleteViewModel DeleteVm { get; }
        public TagListViewModel ListVm { get; }

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
