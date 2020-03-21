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

        public static Guid g1 => Guid.Parse("82734205-7b14-4922-9288-196e87513cf5");


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
            facade.CreateNewQuestion(new NewQuestion() { Id = g1 });

            facade.ReplyToQuestion(g1, "comment1");
            facade.ReplyToQuestion(g1, "comment2");

            var comments = facade.ListComments(g1);

            //Assert.Equal(2, comments.Count());

            //facade.ReplyToComment(1, 1, "reply1");
            //facade.ReplyToComment(1, 1, "reply2");

            //var replies = facade.ListRepliesOfComment(1, 1);

            //Assert.Equal(2, replies.Count());

            //TODO NOT FINSISHED
        }

        [Fact]
        public void CreateBoard()
        {
            var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateBoard(null));
            Assert.Throws<ArgumentNullException>(() => facade.CreateBoard(string.Empty));
            facade.CreateBoard("name");
            var board = facade.ListBoards().First();
            Assert.True(board.Name == "name");
        }

        [Fact]
        public void DeleteBoard()
        {
            var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateBoard("name");
            var board = facade.ListBoards().First();
            facade.DeleteBoard(board.Id);
            Assert.Empty(facade.ListBoards());
        }

        [Fact]
        public void RenameBoard()
        {
            var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateBoard("name");
            var board = facade.ListBoards().First();
            facade.RenameBoard(board.Id, "newName");
            Assert.True(board.Name == "newName");
        }
    }
}

