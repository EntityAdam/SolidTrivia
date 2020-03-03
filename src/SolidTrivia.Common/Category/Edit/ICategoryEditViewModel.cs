namespace SolidTrivia.Common
{
    public interface ICategoryEditViewModel
    {
        CategoryEditModel EditModel { get; set; }
        void Load(int categoryId);
        void RenameCategory();
        IBlazorCommand RenameCommand { get; set; }
    }
}