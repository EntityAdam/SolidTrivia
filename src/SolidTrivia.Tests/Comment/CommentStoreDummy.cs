using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class CommentStoreDummy : ICommentStore
    {
        public IEnumerable<NewComment> ListCommentsOfQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewComment> ListRepliesOfComment(int questionId, int commentId)
        {
            throw new NotImplementedException();
        }

        public void ReplyToComment(int questionId, int commendId, string reply)
        {
            throw new NotImplementedException();
        }

        public void ReplyToQuestion(int questionId, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
