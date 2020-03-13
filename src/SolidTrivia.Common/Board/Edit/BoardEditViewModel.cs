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

            RenameCommand = new BlazorCommand(
                () => RenameBoard(),
                () => !string.IsNullOrEmpty(EditModel.NewName) && !string.Equals(EditModel.OldName, EditModel.NewName, StringComparison.OrdinalIgnoreCase)
            );
        }

        public BoardEditModel EditModel { get; set; }
        public IBlazorCommand RenameCommand { get; set; }

        public void Load(int boardId)
        {
            var board = facade.GetBoard(boardId);
            EditModel = new BoardEditModel()
            {
                Id = board.Id,
                OldName = board.Name,
                NewName = board.Name
            };
        }

        public void RenameBoard() => facade.RenameBoard(EditModel.Id, EditModel.NewName);
    }
}