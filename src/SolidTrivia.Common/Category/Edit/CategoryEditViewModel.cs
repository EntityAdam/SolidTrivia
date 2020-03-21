using SolidTrivia.Questions;
using System;

namespace SolidTrivia.Common
{
    public class CategoryEditViewModel : BindableBase
    {
        private readonly IQuestionFacade facade;

        public CategoryEditViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
        }

        public CategoryEditModel EditModel { get; set; }
        public IBlazorCommand RenameCommand { get; set; }

        public void Load(Guid categoryId)
        {
            var cat = facade.GetCategory(categoryId);
            EditModel = new CategoryEditModel()
            {
                Id = cat.Id,
                OldName = cat.Name,
                NewName = cat.Name
            };

            RenameCommand = new BlazorCommand(
                () => RenameCategory(),
                () => !string.IsNullOrEmpty(EditModel.NewName) && !string.Equals(EditModel.OldName, EditModel.NewName, StringComparison.OrdinalIgnoreCase)
            );
        }

        public void RenameCategory() => facade.RenameCategory(EditModel.Id, EditModel.NewName);
    }

}
