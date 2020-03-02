using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SolidTrivia.Tests
{
    public class TagStoreTests
    {
        [Fact]
        public void TagQuestionNulls()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
        }

        [Fact]
        public void DupeTagThrowsException()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
            facade.CreateTag("tag1");
            Assert.Throws<ArgumentException>(() => facade.CreateTag("tag1"));
            
        }

        [Fact]
        public void TagQuestion()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());

            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
            facade.CreateTag("tag1");
            facade.CreateTag("tag2");
            facade.CreateTag("tag3");

            var availableTags = facade.ListAvailableTags(1);
            Assert.Equal(3, availableTags.Count());

            facade.TagQuestion(1, 1);
            availableTags = facade.ListAvailableTags(1);
            Assert.Equal(2, availableTags.Count());

            facade.TagQuestion(1, 2);
            availableTags = facade.ListAvailableTags(1);
            Assert.Single(availableTags);

            Assert.Throws<ArgumentException>(() => facade.TagQuestion(1, 2));
        }
    }
}
