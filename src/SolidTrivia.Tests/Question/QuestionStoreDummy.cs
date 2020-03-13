using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class QuestionStoreDummy : IQuestionStore
    {
        public void AddQuestionToCategory(int questionId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Create(NewQuestion question)
        {
            throw new NotImplementedException();
        }

        public void Delete(int questionId)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int questionId)
        {
            throw new NotImplementedException();
        }

        public NewQuestion GetById(int questionId)
        {
            throw new NotImplementedException();
        }

        public bool IsQuestionInCategory(int questionId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewQuestion> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewQuestion> ListQuestionsOfCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
