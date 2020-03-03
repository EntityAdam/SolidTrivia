using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolidTrivia.Common.Tests
{
    public class TagsViewModelTests
    {
        [Fact]
        public void Test()
        {
            var questionFacade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreMock(), new CommentStoreMock(), new CategoryStoreMock(), new BoardStoreMock());
            var viewModel = new CreateTagViewModel(questionFacade);

            //new vm
            //tagname should be empty
            Assert.True(string.IsNullOrEmpty(viewModel.TagName));
            //create command should not work with an empty tagname
            Assert.False(viewModel.CreateCommand.CanExecute(null));  

            //enter tag name
            viewModel.TagName = "tag";

            //assert create button enabled
            Assert.True(viewModel.CreateCommand.CanExecute(null));

            //execute
            viewModel.CreateCommand.Execute(null);

            //assert create button disabled, because 'tag' already exists  //TODO : THIS EXECUTES IO CALL. DO I WANT THAT?
            Assert.False(viewModel.CreateCommand.CanExecute(null));

            //change tag name
            viewModel.TagName = "tag1";

            //assert create button is again enabled
            Assert.True(viewModel.CreateCommand.CanExecute(null));


            //checking invalid tag names
            viewModel.TagName = "control";
            Assert.True(viewModel.CreateCommand.CanExecute(null));

            //oh no you don't XSS attack!
            viewModel.TagName = "<script>";
            Assert.False(viewModel.CreateCommand.CanExecute(null));

            viewModel.TagName = "tim^tebow";
            Assert.False(viewModel.CreateCommand.CanExecute(null));

            viewModel.TagName = "c++";
            Assert.True(viewModel.CreateCommand.CanExecute(null));

            viewModel.TagName = "c#";
            Assert.True(viewModel.CreateCommand.CanExecute(null));
        }
    }
}
