using Ganss.XSS;
using Markdig;
using SolidTrivia.Questions;

namespace SolidTrivia.Common
{
    public class QuestionCreateViewModel : BindableBase
    {
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IQuestionFacade questionFacade;
        private string userInputMarkdown;

        public QuestionCreateViewModel(IQuestionFacade questionFacade)
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.questionFacade = questionFacade;

            SaveCommand = new BlazorCommand(
                () => SaveQuestionMarkdown(),
                () => !string.IsNullOrEmpty(UserInputMarkdown)
            );
        }

        public string UserInputMarkdown
        {
            get => userInputMarkdown;
            set
            {
                userInputMarkdown = value;
                DisplaySanitizedHtml();
                OnPropertyChanged(nameof(SanitizedHtmlDisplay));
            }
        }

        public IBlazorCommand SaveCommand { get; set; }

        public string SanitizedHtmlDisplay { get; set; }

        public void DisplaySanitizedHtml() => SanitizedHtmlDisplay = htmlSanitizer.Sanitize(RenderMarkdownToHtml(UserInputMarkdown));
        private static string RenderMarkdownToHtml(string markdown) => Markdown.ToHtml(markdown, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
        public void SaveQuestionMarkdown() => questionFacade.CreateQuestion(UserInputMarkdown);
    }
}
