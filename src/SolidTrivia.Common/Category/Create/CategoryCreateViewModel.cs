using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SolidTrivia.Common
{
    public class CategoryCreateViewModel : BindableBase
    {
        private readonly IQuestionFacade facade;
        private Dictionary<string, bool> inputClasses { get; set; } = new Dictionary<string, bool>() { { "form-control", true }, { "is-valid", false }, { "is-invalid", false } };
        public IBlazorCommand CreateCommand { get; set; }
        public CategoryCreateModel CreateModel { get; set; } = new CategoryCreateModel();
        public bool? IsValid { get; set; } = null;

        public CategoryCreateViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            CreateCommand = new BlazorCommand(
                () => Create(),
                () => !string.IsNullOrEmpty(CreateModel.Name) && !CategoryExists() && InputValidator.IsValidCategory(CreateModel.Name)
            );
        }

        private void Create()
        {
            facade.CreateCategory(CreateModel.Name);
            Clear();
        }

        private void Clear() => CreateModel = new CategoryCreateModel();

        private bool CategoryExists() => facade.CategoryExists(CreateModel.Name);

        public string InputClasses => string.Join(" ", inputClasses.Where(x => x.Value == true).Select(x => x.Key));
    }
}
