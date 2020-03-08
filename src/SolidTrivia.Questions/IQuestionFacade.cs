using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface IQuestionFacade
    {
        void AddCategoryToBoard(int boardId, int categoryId);
        void AddQuestionToCategory(int questionId, int categoryId);
        void CreateBoard(string name);
        void CreateCategory(string category);
        void CreateNewQuestion(NewQuestion question);
        void CreateQuestion(string question);
        void CreateTag(string tagName);
        void DeleteTag(int tagId);
        void DeleteCategory(int categoryId);
        NewTag GetTag(int tagId);
        void DeleteCategoryOfBoard(int boardId, int categoryId);
        void DownVote(string userId, int questionId);
        NewBoard GetBoard(string name);
        NewBoard GetBoard(int boardId);
        NewCategory GetCategory(int categoryId);
        bool TagExists(string tagName);
        void DeleteBoard(int boardId);
        bool TagExists(int tagId);
        NewCategory GetCategoryOfBoard(int boardId, int categoryId);
        void RenameBoard(int boardId, string newBoardName);
        (int, int) GetVotes(int questionId);
        void RenameTag(int tagId, string newTagName);
        void RenameCategory(int categoryId, string newCategoryName);
        IEnumerable<NewTag> ListAvailableTags(int questionId);
        IEnumerable<NewCategory> ListCategories();
        bool CategoryExists(string name);
        IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId);
        IEnumerable<NewComment> ListComments(int questionId);
        IEnumerable<NewQuestion> ListQuestionsOfCategory(int categoryId);
        IEnumerable<NewComment> ListRepliesOfComment(int questionId, int commentId);
        IEnumerable<NewTag> ListTags();
        void ReplyToComment(int questionId, int commentId, string reply);
        void ReplyToQuestion(int questionId, string comment);
        void TagQuestion(int questionId, int tagId);
        void UpVote(string userId, int questionId);
    }
}