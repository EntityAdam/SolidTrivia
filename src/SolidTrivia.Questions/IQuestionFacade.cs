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
        NewQuestion GetQuestion(int questionId);
        NewBoard GetBoard(int boardId);
        NewCategory GetCategory(int categoryId);
        void DeleteQuestion(int questionId);
        bool TagExists(string tagName);
        void DeleteBoard(int boardId);
        bool TagExists(int tagId);
        void RenameBoard(int boardId, string newBoardName);
        (int, int) GetVotes(int questionId);
        void RenameTag(int tagId, string newTagName);
        void RenameCategory(int categoryId, string newCategoryName);
        IEnumerable<NewTag> ListAvailableTags(int questionId);
        IEnumerable<NewCategory> ListCategories();
        bool CategoryExists(string name);
        IEnumerable<NewQuestion> ListQuestions();
        IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId);
        void EditQuestionContent(int questionId, string userInputMarkdown);
        IEnumerable<NewBoard> ListBoards();
        IEnumerable<NewComment> ListComments(int questionId);
        IEnumerable<NewQuestion> ListQuestionsOfCategory(int categoryId);
        IEnumerable<NewComment> ListRepliesOfComment(int questionId, int commentId);
        IEnumerable<NewTag> ListTags();
        void ReplyToComment(int questionId, int commentId, string reply);
        void ReplyToQuestion(int questionId, string comment);
        void TagQuestion(int questionId, int tagId);
        void UpVote(string userId, int questionId);
        void RemoveCategoryFromBoard(int boardId, int categoryId);
        IEnumerable<NewCategory> ListAvailableCategories(int boardId);
    }
}