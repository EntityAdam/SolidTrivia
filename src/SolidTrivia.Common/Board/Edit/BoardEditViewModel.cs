using SolidTrivia.Questions;
using System;

namespace SolidTrivia.Common
{
    public class BoardEditViewModel : BindableBase
    {
        private readonly IQuestionFacade facade;

        public BoardEditViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
        }

        public BoardEditModel EditModel { get; set; }
        public IBlazorCommand RenameCommand { get; set; }

        public void Load(int boardId)
        {
            var cat = facade.GetBoard(boardId);
            EditModel = new BoardEditModel()
            {
                Id = cat.Id,
                OldName = cat.Name,
                NewName = cat.Name
            };

            RenameCommand = new BlazorCommand(
                () => RenameBoard(),
                () => !string.IsNullOrEmpty(EditModel.NewName) && !string.Equals(EditModel.OldName, EditModel.NewName, StringComparison.OrdinalIgnoreCase)
            );
        }

        public void RenameBoard() => facade.RenameBoard(EditModel.Id, EditModel.NewName);
    }

}
