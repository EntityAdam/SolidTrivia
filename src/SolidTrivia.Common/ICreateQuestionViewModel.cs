using System.ComponentModel;

namespace SolidTrivia.Common
{
    public interface ICreateQuestionViewModel : INotifyPropertyChanged
    {
        string SanitizedHtml { get; set; }
        string UserInputMarkdown { get; set; }
        string UserPrincipal { get; set; }

        IBlazorCommand SaveCommand { get; set; }

        void DisplaySanitizedHtml();
        void SaveQuestionMarkdown();
    }
}