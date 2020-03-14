using Ganss.XSS;
using Markdig;
using SolidTrivia.Questions;
using System;

namespace SolidTrivia.Common
{
    public class QuestionEditViewModel : BindableBase
    {
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IQuestionFacade facade;
        private string userInputMarkdown;

        public QuestionEditViewModel(IQuestionFacade facade)
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.facade = facade;

            SaveCommand = new BlazorCommand(
                () => SaveQuestionMarkdown(),
                () => !string.IsNullOrEmpty(UserInputMarkdown)
            );
        }

        public QuestionEditModel EditModel { get; set; }

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

        public void SaveQuestionMarkdown() => facade.EditQuestionContent(EditModel.Id, UserInputMarkdown);

        public void Load(int questionId)
        {
            var cat = facade.GetQuestion(questionId);
            EditModel = new QuestionEditModel()
            {
                Id = cat.Id,
                Content = cat.MarkdownContent
            };
            UserInputMarkdown = cat.MarkdownContent;
        }
    }
}
