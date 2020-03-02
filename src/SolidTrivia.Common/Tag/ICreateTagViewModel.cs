namespace SolidTrivia.Common
{
    public interface ICreateTagViewModel
    {
        IBlazorCommand CreateCommand { get; set; }
        string TagName { get; set; }
    }
}