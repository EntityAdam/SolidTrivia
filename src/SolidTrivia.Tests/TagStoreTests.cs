﻿using SolidTrivia.Questions;
using System;
using System.Linq;
using Xunit;

namespace SolidTrivia.Tests
{
    public class TagStoreTests
    {
        public static Guid g1 => Guid.Parse("82734205-7b14-4922-9288-196e87513cf5");
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
            var tag = facade.ListTags().First();

            Assert.True(facade.TagExists(tag.Id));
            Assert.True(facade.TagExists("tag1"));
        }

        [Fact]
        public void DeleteTag()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag1");
            var tag = facade.ListTags().First();
            facade.DeleteTag(tag.Id);
            Assert.Empty(facade.ListTags());
            Assert.Throws<ArgumentException>(() => facade.DeleteTag(tag.Id));
        }

        [Fact]
        public void RenameTag()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());
            facade.CreateTag("tag1");
            var tag = facade.ListTags().First();
            facade.RenameTag(tag.Id, "newName");
            Assert.Equal("newName", tag.Name);
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

        //[Fact]
        //public void TagQuestion()
        //{
        //    var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreMock(), new VoteStoreDummy());

        //    facade.CreateNewQuestion(new NewQuestion() { Id = g1 });
        //    facade.CreateTag("tag1");
        //    facade.CreateTag("tag2");
        //    facade.CreateTag("tag3");

        //    var availableTags = facade.ListAvailableTags(1);
        //    Assert.Equal(3, availableTags.Count());

        //    facade.TagQuestion(1, 1);
        //    availableTags = facade.ListAvailableTags(1);
        //    Assert.Equal(2, availableTags.Count());

        //    facade.TagQuestion(1, 2);
        //    availableTags = facade.ListAvailableTags(1);
        //    Assert.Single(availableTags);

        //    Assert.Throws<ArgumentException>(() => facade.TagQuestion(1, 2));
        //}
    }
}
