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
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateQuestion(null));
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
        }
    }
}
