using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class BoardDeleteViewModel
    {

        private readonly IQuestionFacade facade;

        public BoardDeleteViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            DeleteCommand = new BlazorCommand(
                () => DeleteBoard()
            );
        }

        public BoardDeleteModel DeleteModel { get; set; }

        public IBlazorCommand DeleteCommand { get; set; }

        public void Load(int boardId)
        {
            var board = facade.GetBoard(boardId);
            DeleteModel = new BoardDeleteModel()
            {
                Id = board.Id,
                Name = board.Name
            };
        }

        public void DeleteBoard() => facade.DeleteBoard(DeleteModel.Id); //TODO : Confirm Modal
    }
}
