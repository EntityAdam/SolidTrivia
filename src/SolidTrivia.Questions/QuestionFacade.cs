using System;
using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public class QuestionFacade : IQuestionFacade
    {
        private readonly IQuestionStore questionStore;
        private readonly ITagStore tagStore;
        private readonly IVoteStore voteStore;
        private readonly ICommentStore commentStore;
        private readonly ICategoryStore categoryStore;
        private readonly IBoardStore boardStore;

        public QuestionFacade(IQuestionStore questionStore, ITagStore tagStore, IVoteStore voteStore, ICommentStore commentStore, ICategoryStore categoryStore, IBoardStore boardStore)
        {
            this.questionStore = questionStore ?? throw new ArgumentNullException(nameof(questionStore));
            this.tagStore = tagStore ?? throw new ArgumentNullException(nameof(tagStore));
            this.voteStore = voteStore ?? throw new ArgumentNullException(nameof(voteStore));
            this.commentStore = commentStore ?? throw new ArgumentNullException(nameof(commentStore));
            this.categoryStore = categoryStore ?? throw new ArgumentNullException(nameof(categoryStore));
            this.boardStore = boardStore ?? throw new ArgumentNullException(nameof(boardStore));
        }


        //questions
        public void CreateQuestion(string question)
        {
            if (string.IsNullOrEmpty(question)) throw new ArgumentNullException(nameof(question));
            CreateNewQuestion(new NewQuestion() { MarkdownContent = question });
        }
        public void CreateNewQuestion(NewQuestion question)
        {
            if (question is null) throw new ArgumentNullException(nameof(question));
            questionStore.Create(question);
        }

        //tags
        //TODO : Tags must be case insensitive
        public void CreateTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) throw new ArgumentNullException(nameof(tagName));
            if (tagStore.ExistsOrdinalIgnoreCase(tagName)) throw new ArgumentException(nameof(tagName), $"A tag with '{tagName}' already exists");
            tagStore.Create(tagName);
        }

        public void DeleteTag(int tagId)
        {
            if (!tagStore.Exists(tagId)) throw new ArgumentException(nameof(tagId), $"A tag with '{tagId}' does not exist");
            tagStore.Delete(tagId);
        }

        public IEnumerable<NewTag> ListTags() => tagStore.ListTags();

        //tags and questions
        public IEnumerable<NewTag> ListAvailableTags(int questionId)
        {
            if (!questionStore.Exists(questionId)) throw new ArgumentException(nameof(questionId), $"Question with Id '{questionId}' does not exist");
            return tagStore.ListAvailableTags(questionId);
        }
        public void TagQuestion(int questionId, int tagId)
        {
            if (!tagStore.Exists(tagId)) throw new ArgumentException(nameof(tagId), $"Tag with Id '{tagId}' does not exist");
            if (!questionStore.Exists(questionId)) throw new ArgumentException(nameof(questionId), $"Question with Id '{questionId}' does not exist");
            if (tagStore.IsTagged(questionId, tagId)) throw new ArgumentException($"Question with Id '{questionId}' is already tagged with TagId '{tagId}'");
            tagStore.TagQuestion(questionId, tagId);
        }


        //votes and questions
        public void UpVote(string userId, int questionId) => voteStore.UpVote(userId, questionId);
        public void DownVote(string userId, int questionId) => voteStore.DownVote(userId, questionId);
        public (int, int) GetVotes(int questionId) => voteStore.GetVotes(questionId);


        //category
        public void CreateCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));
            categoryStore.Create(category);
        }

        public IEnumerable<NewCategory> ListCategories() => categoryStore.ListCategories();

        public void DeleteCategory(int categoryId) => categoryStore.DeleteCategory(categoryId);

        //category and boards
        public NewCategory GetCategoryOfBoard(int boardId, int categoryId) => categoryStore.GetCategoryOfBoard(boardId, categoryId);
        public IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId) => categoryStore.ListCategoriesOfBoard(boardId);
        public void AddCategoryToBoard(int boardId, int categoryId) => categoryStore.AddCategoryToBoard(boardId, categoryId);
        public void DeleteCategoryOfBoard(int boardId, int categoryId) => categoryStore.DeleteCategoryOfBoard(boardId, categoryId);

        //category and questions
        public void AddQuestionToCategory(int questionId, int categoryId)
        {
            if (!questionStore.Exists(questionId)) throw new ArgumentException(nameof(questionId), $"Question with id '{questionId}' does not exist");
            if (!categoryStore.Exists(categoryId)) throw new ArgumentException(nameof(categoryId), $"Category with id '{categoryId}' does not exist");
            if (questionStore.IsQuestionInCategory(questionId, categoryId)) throw new ArgumentException($"Question with id '{questionId}' already exists in Category with id '{categoryId}'");
            questionStore.AddQuestionToCategory(questionId, categoryId);
        }
        public IEnumerable<NewQuestion> ListQuestionsOfCategory(int categoryId) => questionStore.ListQuestionsOfCategory(categoryId);

        //comments
        public IEnumerable<NewComment> ListComments(int questionId) => commentStore.ListCommentsOfQuestion(questionId);

        //comments and questions
        public void ReplyToQuestion(int questionId, string comment) => commentStore.ReplyToQuestion(questionId, comment);

        public void ReplyToComment(int questionId, int commentId, string reply) => commentStore.ReplyToComment(questionId, commentId, reply);
        public IEnumerable<NewComment> ListRepliesOfComment(int questionId, int commentId) => commentStore.ListRepliesOfComment(questionId, commentId);


        //boards
        public void CreateBoard(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            boardStore.Create(name);
        }

        public NewBoard GetBoard(string name) => boardStore.GetBoardByName(name);

        public bool TagExists(string tagName) => tagStore.ExistsOrdinalIgnoreCase(tagName);
        public bool TagExists(int tagId) => tagStore.Exists(tagId);

        public NewCategory GetCategory(int categoryId) => categoryStore.GetById(categoryId);

        public void RenameCategory(int categoryId, string newCategoryName)
        {
            if (string.IsNullOrEmpty(newCategoryName)) throw new ArgumentException(nameof(newCategoryName));
            if (!categoryStore.Exists(categoryId)) throw new ArgumentException(nameof(categoryId), $"Category with id '{categoryId}' does not exist");
            categoryStore.Rename(categoryId, newCategoryName);
        }

        public NewTag GetTag(int tagId)
        {
            if (!tagStore.Exists(tagId)) throw new ArgumentException(nameof(tagId), $"Tag with id '{tagId}' does not exist");
            return tagStore.GetById(tagId);
        }

        public void RenameTag(int tagId, string newTagName)
        {
            if (string.IsNullOrEmpty(newTagName)) throw new ArgumentException(nameof(newTagName));
            if (!tagStore.Exists(tagId)) throw new ArgumentException(nameof(tagId), $"Tag with id '{tagId}' does not exist");
            tagStore.Rename(tagId, newTagName);
        }
    }
}
