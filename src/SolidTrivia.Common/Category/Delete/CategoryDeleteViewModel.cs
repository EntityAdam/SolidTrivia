using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class CategoryDeleteViewModel
    {

        private readonly IQuestionFacade facade;

        public CategoryDeleteViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            DeleteCommand = new BlazorCommand(
                () => DeleteCategory()
            );
        }

        public CategoryDeleteModel DeleteModel { get; set; }

        public IBlazorCommand DeleteCommand { get; set; }

        public void Load(Guid tagId)
        {
            var tag = facade.GetCategory(tagId);
            DeleteModel = new CategoryDeleteModel()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public void DeleteCategory() => facade.DeleteCategory(DeleteModel.Id); //TODO : Confirm Modal
    }
}
