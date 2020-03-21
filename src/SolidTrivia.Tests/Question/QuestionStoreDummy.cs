using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class QuestionStoreDummy : IQuestionStore
    {
        public void AddQuestionToCategory(Guid questionId, Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public void Create(NewQuestion question)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public void EditContent(Guid questionId, string userInputMarkdown)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public NewQuestion GetById(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public bool IsQuestionInCategory(Guid questionId, Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewQuestion> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewQuestion> ListQuestionsOfCategory(Guid categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
