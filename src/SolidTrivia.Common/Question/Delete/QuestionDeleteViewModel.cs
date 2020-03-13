using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class QuestionDeleteViewModel
    {

        private readonly IQuestionFacade facade;

        public QuestionDeleteViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            DeleteCommand = new BlazorCommand(
                () => DeleteQuestion()
            );
        }

        public QuestionDeleteModel DeleteModel { get; set; }

        public IBlazorCommand DeleteCommand { get; set; }

        public void Load(int questionId)
        {
            var question = facade.GetQuestion(questionId);
            DeleteModel = new QuestionDeleteModel()
            {
                Id = question.Id
            };
        }

        public void DeleteQuestion() => facade.DeleteQuestion(DeleteModel.Id); //TODO : Confirm Modal
    }
}
