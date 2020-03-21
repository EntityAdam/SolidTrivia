using System;

namespace SolidTrivia.Questions
{
    public interface IVoteStore
    {
        void UpVote(string userId, Guid questionId);
        void DownVote(string userId, Guid questionId);
        (int, int) GetVotes(Guid questionId);
    }
}