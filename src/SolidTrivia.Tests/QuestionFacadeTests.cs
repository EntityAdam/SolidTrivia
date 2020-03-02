using SolidTrivia.Questions;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SolidTrivia.Common.Tests"),
           InternalsVisibleTo("SolidTrivia.Questions.Web")] //TODO : TEMPORARY FOR DEVELOPMENT, MUST DELETE AND REGISTER REAL COMPONENTS

namespace SolidTrivia.Tests
{
    public class QuestionFacadeTests
    {
        [Fact]
        public void FacadeSubcomponentNullChecks()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(null, new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new QuestionStoreDummy(), null, new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new QuestionStoreDummy(), new TagStoreDummy(), null, new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy(), null, new CategoryStoreDummy(), new BoardStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), null, new BoardStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), null);
            });
        }

        [Fact]
        public void CreateComment()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreMock(), new CategoryStoreDummy(), new BoardStoreDummy());
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });

            facade.ReplyToQuestion(1, "comment1");
            facade.ReplyToQuestion(1, "comment2");

            var comments = facade.ListComments(1);

            Assert.Equal(2, comments.Count());

            facade.ReplyToComment(1, 1, "reply1");
            facade.ReplyToComment(1, 1, "reply2");

            var replies = facade.ListRepliesOfComment(1, 1);

            Assert.Equal(2, replies.Count());

            //TODO NOT FINSISHED
        }

        [Fact]
        public void CreateBoard()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreMock());
            Assert.Throws<ArgumentNullException>(() => facade.CreateBoard(null));
            Assert.Throws<ArgumentNullException>(() => facade.CreateBoard(string.Empty));
            facade.CreateBoard("name");

            var board = facade.GetBoard("name");
            Assert.True(board.Id == 1 && board.Name == "name");
        }
    }
}

