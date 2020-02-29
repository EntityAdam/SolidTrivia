using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface ICommentStore
    {
        void ReplyToQuestion(int questionId, string comment);
        void ReplyToComment(int questionId, int CommentId, string reply);
        IEnumerable<NewComment> ListCommentsOfQuestion(int questionId);
        IEnumerable<NewComment> ListRepliesOfComment(int questionId, int commentId);
    }
}