using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class CommentStoreDummy : ICommentStore
    {
        public IEnumerable<NewComment> ListCommentsOfQuestion(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewComment> ListRepliesOfComment(Guid questionId, Guid commentId)
        {
            throw new NotImplementedException();
        }

        public void ReplyToComment(Guid questionId, Guid commentId, string reply)
        {
            throw new NotImplementedException();
        }

        public void ReplyToQuestion(Guid questionId, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
