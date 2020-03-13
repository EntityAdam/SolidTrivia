using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolidTrivia.Tests
{
    public class QuestionStoreTests
    {
        [Fact]
        public void CreateQuestion()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateQuestion(null));
            Assert.Throws<ArgumentNullException>(() => facade.CreateQuestion(string.Empty));
        }
        [Fact]
        public void CreateNewQuestion()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateNewQuestion(null));
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });

            var q = facade.GetQuestion(1);
            Assert.Equal(1, q.Id);
        }

        [Fact]
        public void DeleteQuestion()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
            facade.DeleteQuestion(1);
            Assert.Empty(facade.ListQuestions());
            Assert.Throws<ArgumentException>(() => facade.DeleteQuestion(1));
        }
    }
}
