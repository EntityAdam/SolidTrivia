using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

        public BindingList<CategoryListModel> BoardCategories { get; set; } = new BindingList<CategoryListModel>();

        public IBlazorCommand RenameCommand { get; set; }

        public void Load(int boardId)
        {

            //get the categories
            var boardCategories = facade.ListCategoriesOfBoard(boardId);
            //add them to the view
            UpdateBoardCategories(boardCategories.Select(x => new CategoryListModel() { Id = x.Id, Name = x.Name }));

            var board = facade.GetBoard(boardId);
            EditModel = new BoardEditModel()
            {
                Id = board.Id,
                OldName = board.Name,
                NewName = board.Name
            };
        }

        public void RenameBoard() => facade.RenameBoard(EditModel.Id, EditModel.NewName);

        private void UpdateBoardCategories(IEnumerable<CategoryListModel> page)
        {
            BoardCategories.Clear();
            foreach (var c in page)
            {
                BoardCategories.Add(new CategoryListModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }
    }
}