namespace SolidTrivia.Common
{
    public interface ITagCreateViewModel
    {
        IBlazorCommand CreateCommand { get; set; }
        string TagName { get; set; }
    }
}