using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class CommentStoreMock : ICommentStore
    {
        public List<NewComment> QuestionComments { get; set; } = new List<NewComment>();

        public CommentStoreMock()
        {

        }

        public void ReplyToQuestion(Guid questionId, string comment) =>
            QuestionComments.Add(new NewComment() { QuestionId = questionId, Text = comment });

        public IEnumerable<NewComment> ListCommentsOfQuestion(Guid questionId) => QuestionComments.Where(qc => qc.QuestionId == questionId);

        public IEnumerable<NewComment> ListRepliesOfComment(Guid questionId, Guid commentId) => QuestionComments.Where(c=>c.QuestionId == questionId && c.CommentId == commentId);

        public void ReplyToComment(Guid questionId, Guid commentId, string reply) =>
            QuestionComments.Add(new NewComment() { QuestionId = questionId, CommentId = commentId, Text = reply });
    }
}
