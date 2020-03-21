using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class VoteStoreDummy : IVoteStore
    {
        public void DownVote(string userId, Guid questionId)
        {
            throw new NotImplementedException();
        }

        public (int, int) GetVotes(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public void UpVote(string userId, Guid questionId)
        {
            throw new NotImplementedException();
        }
    }
}
