using Ganss.XSS;
using Markdig;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace SolidTrivia.Questions.Web.Pages
{
    public class CreateQuestionBase : ComponentBase
    {
        [Inject] public IHtmlSanitizer HtmlSanitizer 
            { 
            get; 
            set; 
            }

        protected MarkupString SanitizedHtml { get; set; }

        protected string ContentValue { get; set; } = string.Empty;

        protected void TextChanged(ChangeEventArgs e)
        {
            var val = string.Empty;
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            ContentValue = val;
        }

        protected async override Task OnInitializedAsync()
        {
            ContentValue = "Enter your question in Markdown format.";
        }

        protected void SaveChanges()
        {
            Clean();
            Save();
        }
        private void Clean()
        {
            var html = Markdig.Markdown.ToHtml(ContentValue, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            SanitizedHtml = new MarkupString(HtmlSanitizer.Sanitize(html));

        }
        private void Save()
        {
            var x = SanitizedHtml;
        }
    }
}