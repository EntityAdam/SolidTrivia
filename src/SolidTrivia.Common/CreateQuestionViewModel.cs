using Ganss.XSS;
using Markdig;
using SolidTrivia.Questions;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class CreateQuestionViewModel : ICreateQuestionViewModel
    {
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IQuestionFacade questionFacade;

        public event PropertyChangedEventHandler PropertyChanged;

        public IBlazorCommand SaveCommand { get; set; }

        public CreateQuestionViewModel(IQuestionFacade questionFacade)
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.questionFacade = questionFacade;

            SaveCommand = new BlazorCommand(
                () => SaveQuestionMarkdown(),
                () => !string.IsNullOrEmpty(UserInputMarkdown)
            );
        }

        public string UserPrincipal { get; set; }

        private string userInputMarkdown;
        public string UserInputMarkdown
        {
            get => userInputMarkdown;
            set
            {
                userInputMarkdown = value;
                DisplaySanitizedHtml();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SanitizedHtml)));
            }
        }

        public string SanitizedHtml { get; set; }

        public void DisplaySanitizedHtml() => SanitizedHtml = htmlSanitizer.Sanitize(RenderMarkdownToHtml(UserInputMarkdown));

        private static string RenderMarkdownToHtml(string markdown) => Markdown.ToHtml(markdown, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());

        public void SaveQuestionMarkdown() => questionFacade.CreateQuestion(UserInputMarkdown);
    }
}
