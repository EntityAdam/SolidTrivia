using SolidTrivia.Questions;
using System;

namespace SolidTrivia.Common
{
    public class QuestionEditViewModel : BindableBase
    {
        private readonly IQuestionFacade facade;

        public QuestionEditViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
        }

        public QuestionEditModel EditModel { get; set; }

        public void Load(int questionId)
        {
            var cat = facade.GetQuestion(questionId);
            EditModel = new QuestionEditModel()
            {
                Id = cat.Id,
            };
        }
    }
}
