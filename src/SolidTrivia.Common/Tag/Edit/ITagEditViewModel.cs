namespace SolidTrivia.Common
{
    public interface ITagEditViewModel
    {
        TagEditModel EditModel { get; set; }
        IBlazorCommand RenameCommand { get; set; }

        void Load(int categoryId);
        void RenameCategory();
    }
}