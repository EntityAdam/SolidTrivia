using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class TagDeleteViewModel
    {
        private readonly IQuestionFacade facade;

        public TagDeleteViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            DeleteCommand = new BlazorCommand(
                () => DeleteTag()
            );
        }

        public TagDeleteModel DeleteModel { get; set; }

        public IBlazorCommand DeleteCommand { get; set; }

        public void Load(int tagId)
        {
            var tag = facade.GetTag(tagId);
            DeleteModel = new TagDeleteModel()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public void DeleteTag() => facade.DeleteTag(DeleteModel.Id); //TODO : Confirm Modal
    }
}
