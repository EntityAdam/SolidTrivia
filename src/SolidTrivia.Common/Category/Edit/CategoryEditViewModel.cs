using SolidTrivia.Questions;

namespace SolidTrivia.Common
{
    public class CategoryEditViewModel : BindableBase, ICategoryEditViewModel
    {
        private readonly IQuestionFacade facade;

        public CategoryEditViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
        }

        public CategoryEditModel EditModel { get; set; }
        public IBlazorCommand RenameCommand { get; set; }

        public void Load(int categoryId)
        {
            var cat = facade.GetCategory(categoryId);
            EditModel = new CategoryEditModel()
            {
                Id = cat.Id,
                Name = cat.Name
            };

            RenameCommand = new BlazorCommand(
                () => RenameCategory(),
                () => !string.IsNullOrEmpty(EditModel.Name) // and category exists.
            );
        }

        public void RenameCategory() => facade.RenameCategory(EditModel.Id, EditModel.Name);
    }

}
