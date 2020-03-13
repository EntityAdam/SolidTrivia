using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;

namespace SolidTrivia.Common.Tests
{
    public class CategoryFacadeFixture : IDisposable
    {

        public CategoryFacadeFixture()
        {
            Facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreMock(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock());
            CreateVm = new CategoryCreateViewModel(Facade);
            EditVm = new CategoryEditViewModel(Facade);
            DeleteVm = new CategoryDeleteViewModel(Facade);
            ListVm = new CategoryListViewModel(Facade);

            CreateCategories();

        }

        private void CreateCategories()
        {
            Facade.CreateCategory("CategoryA");
            Facade.CreateCategory("CategoryB");
            Facade.CreateCategory("CategoryC");
            Facade.CreateCategory("CategoryD");
            Facade.CreateCategory("CategoryE");
            Facade.CreateCategory("CategoryF");
        }

        public IQuestionFacade Facade { get; }
        public CategoryCreateViewModel CreateVm { get; }
        public CategoryEditViewModel EditVm { get; }
        public CategoryDeleteViewModel DeleteVm { get; }
        public CategoryListViewModel ListVm { get; }

        public void Dispose()
        {
            //nothing to clean up
        }
    }
}
