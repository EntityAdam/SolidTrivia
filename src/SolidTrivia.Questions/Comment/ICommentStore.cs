using System;
using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface ICommentStore
    {
        void ReplyToQuestion(Guid questionId, string comment);
        void ReplyToComment(Guid questionId, Guid commentId, string reply);
        IEnumerable<NewComment> ListCommentsOfQuestion(Guid questionId);
        IEnumerable<NewComment> ListRepliesOfComment(Guid questionId, Guid commentId);
    }
}