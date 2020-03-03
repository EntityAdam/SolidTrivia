using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class TagCreateViewModel : BindableBase, ITagCreateViewModel
    {
        private readonly IQuestionFacade facade;

        public IBlazorCommand CreateCommand { get; set; }

        public string TagName { get; set; }

        public TagCreateViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
            CreateCommand = new BlazorCommand(
                () => Create(),
                () => !string.IsNullOrEmpty(TagName) && !TagExists() && InputValidator.IsValidTagName(TagName)
           );
        }

        private bool TagExists() => facade.TagExists(this.TagName);

        private void Create() => facade.CreateTag(this.TagName);
    }
}
