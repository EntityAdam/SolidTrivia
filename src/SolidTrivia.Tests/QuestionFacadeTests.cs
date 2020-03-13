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
                var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), null, new TagStoreDummy(), new VoteStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreDummy(), null, new VoteStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreDummy(), new TagStoreDummy(), null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), null, new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(new BoardStoreDummy(), null, new CommentStoreDummy(), new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy());
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var facade = new QuestionFacade(null, new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy());
            });
        }

        [Fact]
        public void CreateComment()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreMock(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
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
            var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateBoard(null));
            Assert.Throws<ArgumentNullException>(() => facade.CreateBoard(string.Empty));
            facade.CreateBoard("name");
            var board = facade.GetBoard(1);
            Assert.True(board.Id == 1 && board.Name == "name");
        }

        [Fact]
        public void DeleteBoard()
        {
            var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateBoard("name");
            facade.DeleteBoard(1);
            Assert.Empty(facade.ListBoards());
        }

        [Fact]
        public void RenameBoard()
        {
            var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateBoard("name");
            facade.RenameBoard(1, "newName");
            var board = facade.GetBoard(1);
            Assert.True(board.Id == 1 && board.Name == "newName");
        }
    }
}

