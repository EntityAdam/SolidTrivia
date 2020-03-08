using Ganss.XSS;
using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System.Linq;
using Xunit;

namespace SolidTrivia.Common.Tests
{
    public class CreateCategoryTests
    {
        [Fact]
        public void Test()
        {
            var htmlSanitizer = new HtmlSanitizer();
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock(), new CommentStoreMock(), new CategoryStoreMock(), new BoardStoreMock());

            facade.CreateCategory("a");
            facade.CreateCategory("b");
            facade.CreateCategory("c");
            facade.CreateCategory("d");
            facade.CreateCategory("e");
            facade.CreateCategory("f");

            //new vm
            var viewModel = new CategoryListViewModel(facade);

            //page should be empty
            Assert.Empty(viewModel.Categories);
            Assert.False(viewModel.PrevPageCommand.CanExecute(null));
            Assert.False(viewModel.NextPageCommand.CanExecute(null));

            viewModel.Load();
            Assert.Equal(4, viewModel.Categories.Count());
            Assert.False(viewModel.PrevPageCommand.CanExecute(null));
            Assert.True(viewModel.NextPageCommand.CanExecute(null));

            viewModel.NextPageCommand.Execute(null);
            Assert.Equal(2, viewModel.Categories.Count());
            Assert.True(viewModel.PrevPageCommand.CanExecute(null));
            Assert.False(viewModel.NextPageCommand.CanExecute(null));

            viewModel.PrevPageCommand.Execute(null);
            Assert.Equal(4, viewModel.Categories.Count());
            Assert.False(viewModel.PrevPageCommand.CanExecute(null));
            Assert.True(viewModel.NextPageCommand.CanExecute(null));
        }
    }
}
