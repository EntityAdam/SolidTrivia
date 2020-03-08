using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SolidTrivia.Common
{
    public class TagCreateViewModel : BindableBase
    {
        private readonly IQuestionFacade facade;

        private Dictionary<string, bool> inputClasses { get; set; } = new Dictionary<string, bool>() { { "form-control", true }, { "is-valid", false }, { "is-invalid", false } };

        public TagCreateViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            CreateCommand = new BlazorCommand(
                () => Create(),
                () => !string.IsNullOrEmpty(TagName) && !TagExists() && InputValidator.IsValidTagName(TagName)
            );

        }

        public IBlazorCommand CreateCommand { get; set; }

        public string TagName { get; set; }

        public bool? IsValid { get; set; } = null;

        private bool TagExists() => facade.TagExists(this.TagName);

        private void Create() {
            facade.CreateTag(this.TagName);
            Clear();
        }

        private void Clear() => TagName = string.Empty;

        public string InputClasses => string.Join(" ", inputClasses.Where(x => x.Value == true).Select(x => x.Key));
    }
}
