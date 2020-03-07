using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class TagEditViewModel : BindableBase
    {
        private readonly IQuestionFacade facade;

        public TagEditViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            RenameCommand = new BlazorCommand(
                () => RenameCategory(),
                () => !string.IsNullOrEmpty(EditModel.NewName) && !string.Equals(EditModel.OldName, EditModel.NewName, StringComparison.OrdinalIgnoreCase)
            );
        }

        public TagEditModel EditModel { get; set; }

        public IBlazorCommand RenameCommand { get; set; }

        public void Load(int tagId)
        {
            var cat = facade.GetTag(tagId);
            EditModel = new TagEditModel()
            {
                Id = cat.Id,
                OldName = cat.Name,
                NewName = cat.Name
            };
        }

        public void RenameCategory() => facade.RenameTag(EditModel.Id, EditModel.NewName);
    }
}
