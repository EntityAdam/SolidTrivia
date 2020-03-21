using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SolidTrivia.Tests
{
    public class QuestionStoreTests
    {
        public static Guid g1 => Guid.Parse("82734205-7b14-4922-9288-196e87513cf5");

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
            facade.CreateNewQuestion(new NewQuestion());
            facade.CreateNewQuestion(new NewQuestion());
            facade.CreateNewQuestion(new NewQuestion());

            Assert.Equal(3, facade.ListQuestions().Count());
        }

        [Fact]
        public void DeleteQuestion()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreDummy(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateNewQuestion(new NewQuestion());
            facade.CreateNewQuestion(new NewQuestion());
            facade.CreateNewQuestion(new NewQuestion());

            Assert.Equal(3, facade.ListQuestions().Count());

            var question = facade.ListQuestions().First();
            facade.DeleteQuestion(question.Id);
            Assert.Equal(2, facade.ListQuestions().Count());
            Assert.Throws<ArgumentException>(() => facade.DeleteQuestion(question.Id));
        }
    }
}
