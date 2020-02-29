using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidTrivia.Tests
{
    internal class QuestionStoreMock : IQuestionStore
    {
        public List<NewQuestion> Questions { get; set; } = new List<NewQuestion>();
        public List<NewQuestionsCategory> QuestionsCategories { get; set; } = new List<NewQuestionsCategory>();

        public QuestionStoreMock()
        {
        }

        //questions
        public void Create(NewQuestion question) => Questions.Add(question);
        public bool Exists(int questionId) => Questions.Any(q => q.Id == questionId);

        //questions and categories
        public IEnumerable<NewQuestion> ListQuestionsOfCategory(int categoryId) => Questions.Where(q => QuestionsCategories.Any(cq => cq.CategoryId == categoryId));
        public void AddQuestionToCategory(int questionId, int categoryId) => QuestionsCategories.Add(new NewQuestionsCategory() { QuestionId = questionId, CategoryId = categoryId });
        public bool IsQuestionInCategory(int questionId, int categoryId) => QuestionsCategories.Any(qc => qc.QuestionId == questionId && qc.CategoryId == categoryId);
    }
}
