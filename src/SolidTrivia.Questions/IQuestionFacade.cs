using System;
using System.Collections.Generic;

namespace SolidTrivia.Questions
{
    public interface IQuestionFacade
    {
        void AddCategoryToBoard(Guid boardId, Guid categoryId);
        void AddQuestionToCategory(Guid questionId, Guid categoryId);
        void CreateBoard(string name);
        void CreateCategory(string category);
        void CreateNewQuestion(NewQuestion question);
        void CreateQuestion(string question);
        void CreateTag(string tagName);
        void DeleteTag(Guid tagId);
        void DeleteCategory(Guid categoryId);
        NewTag GetTag(Guid tagId);
        void DeleteCategoryOfBoard(Guid boardId, Guid categoryId);
        void DownVote(string userId, Guid questionId);
        NewQuestion GetQuestion(Guid questionId);
        NewBoard GetBoard(Guid boardId);
        NewCategory GetCategory(Guid categoryId);
        void DeleteQuestion(Guid questionId);
        bool TagExists(string tagName);
        void DeleteBoard(Guid boardId);
        bool TagExists(Guid tagId);
        void RenameBoard(Guid boardId, string newBoardName);
        (int, int) GetVotes(Guid questionId);
        void RenameTag(Guid tagId, string newTagName);
        void RenameCategory(Guid categoryId, string newCategoryName);
        IEnumerable<NewTag> ListAvailableTags(Guid questionId);
        IEnumerable<NewCategory> ListCategories();
        bool CategoryExists(string name);
        IEnumerable<NewQuestion> ListQuestions();
        IEnumerable<NewCategory> ListCategoriesOfBoard(Guid boardId);
        void EditQuestionContent(Guid questionId, string userInputMarkdown);
        IEnumerable<NewBoard> ListBoards();
        IEnumerable<NewComment> ListComments(Guid questionId);
        IEnumerable<NewQuestion> ListQuestionsOfCategory(Guid categoryId);
        IEnumerable<NewComment> ListRepliesOfComment(Guid questionId, Guid commentId);
        IEnumerable<NewTag> ListTags();
        void ReplyToComment(Guid questionId, Guid commentId, string reply);
        void ReplyToQuestion(Guid questionId, string comment);
        void TagQuestion(Guid questionId, Guid tagId);
        void UpVote(string userId, Guid questionId);
        void RemoveCategoryFromBoard(Guid boardId, Guid categoryId);
        IEnumerable<NewCategory> ListAvailableCategories(Guid boardId);
    }
}