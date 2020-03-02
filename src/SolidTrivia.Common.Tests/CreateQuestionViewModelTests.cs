using SolidTrivia.Questions;
using System;
using Xunit;
using SolidTrivia.Tests;
using Ganss.XSS;

namespace SolidTrivia.Common.Tests
{
    public class CreateQuestionViewModelTests
    {
        [Fact]
        public void Test1()
        {
            var htmlSanitizer = new HtmlSanitizer();
            var questionFacade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock(), new CommentStoreMock(), new CategoryStoreMock(), new BoardStoreMock());
            var viewModel = new CreateQuestionViewModel(questionFacade);

            viewModel.UserInputMarkdown = "# Hello World!";
            viewModel.DisplaySanitizedHtml();

            var html = viewModel.SanitizedHtml;
            var expected = "<h1>Hello World!</h1>\n";

            Assert.Equal(expected, html);

            viewModel.SaveQuestionMarkdown();

            //TODO: Assert question was created

        }
    }
}
