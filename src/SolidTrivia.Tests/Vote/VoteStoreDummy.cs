using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class VoteStoreDummy : IVoteStore
    {
        public void DownVote(string userId, int questionId)
        {
            throw new NotImplementedException();
        }

        public (int, int) GetVotes(int questionId)
        {
            throw new NotImplementedException();
        }

        public void UpVote(string userId, int questionId)
        {
            throw new NotImplementedException();
        }
    }
}
