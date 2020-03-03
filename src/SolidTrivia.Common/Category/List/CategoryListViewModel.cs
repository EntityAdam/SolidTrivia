using SolidTrivia.Questions;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class CategoryListViewModel : BindableBase, ICategoryListViewModel
    {
        private const int defaultPageSize = 4;

        private readonly IQuestionFacade facade;

        public CategoryListViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            NextPageCommand = new BlazorCommand(
                () => Next(),
                () => (PagedCategories != null) ? PagedCategories.CanExecuteNext : false
            );
            PrevPageCommand = new BlazorCommand(
                () => Prev(),
                () => (PagedCategories != null) ? PagedCategories.CanExecutePrev : false
            );
        }

        private void Prev() => UpdateList(PagedCategories.Prev());

        private void Next() => UpdateList(PagedCategories.Next());

        public BindingList<CategoryListModel> Page { get; set; } = new BindingList<CategoryListModel>();

        private PagedEnumerable<CategoryListModel> PagedCategories { get; set; }

        public IBlazorCommand NextPageCommand { get; set; }
        public IBlazorCommand PrevPageCommand { get; set; }

        public void Load()
        {
            PagedCategories = new PagedEnumerable<CategoryListModel>(EnumerateAll(), defaultPageSize);
            UpdateList(PagedCategories.Next());
        }

        private IEnumerable<CategoryListModel> EnumerateAll() => facade.ListCategories().Select(c => new CategoryListModel() { Id = c.Id, Name = c.Name });

        private void UpdateList(IEnumerable<CategoryListModel> page)
        {
            Page.Clear();
            foreach (var c in page)
            {
                Page.Add(new CategoryListModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }
    }

}
