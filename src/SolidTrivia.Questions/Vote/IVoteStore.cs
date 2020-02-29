namespace SolidTrivia.Questions
{
    public interface IVoteStore
    {
        void UpVote(string userId, int questionId);
        void DownVote(string userId, int questionId);
        (int, int) GetVotes(int questionId);
    }
}