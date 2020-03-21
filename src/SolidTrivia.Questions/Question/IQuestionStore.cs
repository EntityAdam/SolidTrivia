using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface IQuestionStore
    {
        //category
        void Create(NewQuestion question);
        bool Exists(Guid questionId);

        //category and question
        IEnumerable<NewQuestion> ListQuestionsOfCategory(Guid categoryId);
        void AddQuestionToCategory(Guid questionId, Guid categoryId);
        bool IsQuestionInCategory(Guid questionId, Guid categoryId);
        NewQuestion GetById(Guid questionId);
        void Delete(Guid questionId);
        IEnumerable<NewQuestion> List();
        void EditContent(Guid questionId, string userInputMarkdown);
    }
}
