using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SolidTrivia.Common.Board.Create
{
    public class BoardCreateViewModel
    {
        private readonly IQuestionFacade facade;

        private Dictionary<string, bool> inputClasses { get; set; } = new Dictionary<string, bool>() { { "form-control", true }, { "is-valid", false }, { "is-invalid", false } };

        public BoardCreateViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            CreateCommand = new BlazorCommand(
            () => Create(),
            () => !string.IsNullOrEmpty(Board.Name) && !BoardExists() && true //InputValidator.IsValidBoard(Board.Name)
        );
        }

        public IBlazorCommand CreateCommand { get; set; }

        public BoardCreateModel Board { get; set; }

        public bool? IsValid { get; set; } = null;

        private bool BoardExists() => false; //facade.BoardExists(Board.Name);

        private void Create()
        {
            facade.CreateBoard(Board.Name);
            Clear();
        }

        private void Clear() => Board.Name = string.Empty;

        public string InputClasses => string.Join(" ", inputClasses.Where(x => x.Value == true).Select(x => x.Key));

    }
}
