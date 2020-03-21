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
            AddCategoryCommand = new BlazorCommand<Guid>(
                (categoryId) => AddCategory(categoryId),
                (categoryId) => BoardCategories.Count() < 6
            );

            RemoveCategoryCommand = new BlazorCommand<Guid>(
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

        public void Load(Guid boardId)
        {
            //lookup board
            var board = facade.GetBoard(boardId);

            //get the categories
            var boardCategories = facade.ListCategoriesOfBoard(boardId);

            //add them to the view
            UpdateBoardCategories(boardCategories.Select(x => new CategoryListModel() { Id = x.Id, Name = x.Name }));

            //get the available categories
            var available = facade.ListAvailableCategories(boardId).Select(c => new CategoryListModel() { Id = c.Id, Name = c.Name });

            //page them
            PagedCategories = new PagedEnumerable<CategoryListModel>(available, defaultPageSize);

            //update the page
            UpdateList(PagedCategories.Next());

            AddCategoryModel = new BoardAddCategoryModel()
            {
                BoardId = board.Id,
                Name = board.Name
            };
        }

        private void Prev() => UpdateList(PagedCategories.Prev());

        private void Next() => UpdateList(PagedCategories.Next());

        public void AddCategory(Guid categoryId)
        {
            //lookup category
            var domainCat = facade.GetCategory(categoryId);

            //add to the view
            BoardCategories.Add(new CategoryListModel() { Id = domainCat.Id, Name = domainCat.Name });

            //remove it from the page ::: //todo is this going to cause problems with the paging? TEST IT! 
            var displayCat = Categories.First(x => x.Id == categoryId);
            Categories.Remove(displayCat);

            //persist it
            facade.AddCategoryToBoard(AddCategoryModel.BoardId, categoryId);

        }

        public void RemoveCategory(Guid categoryId)
        {
            //remove from the view
            var cat = BoardCategories.First(x => x.Id == categoryId);
            BoardCategories.Remove(cat);

            //todo how do I add this back to the page??
            //todo what if I switch pages then remove a cat from the board?? 

            //persist it
            facade.RemoveCategoryFromBoard(AddCategoryModel.BoardId, categoryId);
        }

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
