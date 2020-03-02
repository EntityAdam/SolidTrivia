using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolidTrivia.Tests
{
    public class VoteStoreTests
    {
        [Fact]
        public void VoteQuestion()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreMock(), new CommentStoreDummy(), new CategoryStoreDummy(), new BoardStoreDummy());
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
            facade.UpVote("test", 1);
            var (up, down) = facade.GetVotes(1);
            Assert.Equal(1, up);
            Assert.Equal(0, down);

            facade.UpVote("test", 1);
            (up, down) = facade.GetVotes(1);
            Assert.Equal(1, up);
            Assert.Equal(0, down);

            facade.DownVote("test", 1);
            (up, down) = facade.GetVotes(1);
            Assert.Equal(0, up);
            Assert.Equal(1, down);
        }
    }
}
