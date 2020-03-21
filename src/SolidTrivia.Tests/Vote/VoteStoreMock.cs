using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class VoteStoreMock : IVoteStore
    {
        public List<NewVote> Votes { get; set; } = new List<NewVote>();

        public void DownVote(string userId, Guid questionId)
        {
            var vote = Votes.SingleOrDefault(v => v.UserId == userId && v.QuestionId == questionId);
            if (vote is null) Votes.Add(new NewVote() { UserId = userId, QuestionId = questionId, Value = false });
            else vote.Value = false;
        }

        public (int, int) GetVotes(Guid questionId) => (Votes.Count(v=>v.QuestionId == questionId && v.Value == true), Votes.Count(v => v.QuestionId == questionId && v.Value == false));

        public void UpVote(string userId, Guid questionId)
        {
            var vote = Votes.SingleOrDefault(v => v.UserId == userId && v.QuestionId == questionId);
            if (vote is null) Votes.Add(new NewVote() { UserId = userId, QuestionId = questionId, Value = true });
            else vote.Value = true;
        }
    }
}