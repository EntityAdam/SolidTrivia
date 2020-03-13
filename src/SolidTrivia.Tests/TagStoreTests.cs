using SolidTrivia.Questions;
using System;
using System.Linq;
using Xunit;

namespace SolidTrivia.Tests
{
    public class TagStoreTests
    {
        [Fact]
        public void TagQuestionNulls()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
        }

        [Fact]
        public void CreateTag()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag1");
            Assert.True(facade.TagExists(1));
            Assert.True(facade.TagExists("tag1"));
            var t = facade.GetTag(1);
            Assert.Equal(1, t.Id);
            Assert.Equal("tag1", t.Name);
        }

        [Fact]
        public void DeleteTag()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag1");
            facade.DeleteTag(1);
            Assert.Empty(facade.ListTags());
            Assert.Throws<ArgumentException>(() => facade.DeleteTag(1));
        }

        [Fact]
        public void RenameTag()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag1");
            facade.RenameTag(1, "newName");
            var t = facade.GetTag(1);
            Assert.Equal(1, t.Id);
            Assert.Equal("newName", t.Name);

        }


        [Fact]
        public void DupeTagThrowsException()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag1");
            Assert.Throws<ArgumentException>(() => facade.CreateTag("tag1"));
        }

        [Fact]
        public void TagsShouldBeCaseInsensitive()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag");
            Assert.Throws<ArgumentException>(() => facade.CreateTag("TAG"));
        }

        [Fact]
        public void TagQuestion()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());

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
