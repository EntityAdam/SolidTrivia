using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SolidTrivia.Common
{
    public class BoardAddCategoryViewModel
    {
        private const int defaultPageSize = 4; // TODO : Add PageSize to UI

        private readonly IQuestionFacade facade;

        private PagedEnumerable<CategoryListModel> PagedCategories { get; set; }

        public BoardAddCategoryViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
            AddCategoryCommand = new BlazorCommand<int>(
                (categoryId) => AddCategory(categoryId),
                (categoryId) => BoardCategories.Count() < 6
            );

            RemoveCategoryCommand = new BlazorCommand<int>(
                (categoryId) => RemoveCategory(categoryId),
                (categoryId) => BoardCategories.Count() > 0
            );

            NextPageCommand = new BlazorCommand(
                () => Next(),
                () => (PagedCategories != null) ? PagedCategories.CanExecuteNext : false
            );

            PrevPageCommand = new BlazorCommand(
                () => Prev(),
                () => (PagedCategories != null) ? PagedCategories.CanExecutePrev : false
            );

            UpdateCommand = new BlazorCommand(() => Load(AddCategoryModel.BoardId));
        }

        public BoardAddCategoryModel AddCategoryModel { get; set; }

        public BindingList<CategoryListModel> Categories { get; set; } = new BindingList<CategoryListModel>();

        public BindingList<CategoryListModel> BoardCategories { get; set; } = new BindingList<CategoryListModel>();

        public IBlazorCommand NextPageCommand { get; set; }

        public IBlazorCommand PrevPageCommand { get; set; }

        public IBlazorCommand UpdateCommand { get; set; }

        public IBlazorCommand AddCategoryCommand { get; set; }

        public IBlazorCommand RemoveCategoryCommand { get; set; }

        public void Load(int boardId)
        {
            var board = facade.GetBoard(boardId);

            var selected = facade.ListCategoriesOfBoard(boardId);

            var available = facade.ListAvailableCategories(boardId).Select(c => new CategoryListModel() { Id = c.Id, Name = c.Name });

            PagedCategories = new PagedEnumerable<CategoryListModel>(available, defaultPageSize);

            UpdateList(PagedCategories.Next());

            AddCategoryModel = new BoardAddCategoryModel()
            {
                BoardId = board.Id,
                SelectedCategoryId = null,
                BoardCategories = available
            };
        }

        private void Prev() => UpdateList(PagedCategories.Prev());

        private void Next() => UpdateList(PagedCategories.Next());

        public void AddCategory(int categoryId)
        {
            try
            {
                facade.AddCategoryToBoard(AddCategoryModel.BoardId, categoryId);
                var cat = facade.GetCategory(categoryId);
                BoardCategories.Add(new CategoryListModel() { Id = cat.Id, Name = cat.Name });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveCategory(int categoryId)
        {
            try
            {
                facade.RemoveCategoryFromBoard(AddCategoryModel.BoardId, categoryId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateList(IEnumerable<CategoryListModel> page)
        {
            Categories.Clear();
            foreach (var c in page)
            {
                Categories.Add(new CategoryListModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }
    }
}
