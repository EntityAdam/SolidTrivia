using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Questions
{
    public interface IQuestionStore
    {
        //category
        void Create(NewQuestion question);
        bool Exists(int questionId);

        //category and question
        IEnumerable<NewQuestion> ListQuestionsOfCategory(int categoryId);
        void AddQuestionToCategory(int questionId, int categoryId);
        bool IsQuestionInCategory(int questionId, int categoryId);
    }
}
