using SolidTrivia.Questions;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class CategoryListViewModel : BindableBase
    {
        private const int defaultPageSize = 4; // TODO : Add PageSize to UI

        private readonly IQuestionFacade facade;

        private PagedEnumerable<CategoryListModel> PagedCategories { get; set; }

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

            UpdateCommand = new BlazorCommand(() => Load());
        }

        private void Prev() => UpdateList(PagedCategories.Prev());

        private void Next() => UpdateList(PagedCategories.Next());

        public BindingList<CategoryListModel> Categories { get; set; } = new BindingList<CategoryListModel>();

        public IBlazorCommand NextPageCommand { get; set; }
        public IBlazorCommand PrevPageCommand { get; set; }
        public IBlazorCommand UpdateCommand { get; set; }

        public void Load(int pageSize = defaultPageSize)
        {
            var categories = facade.ListCategories().Select(c => new CategoryListModel() { Id = c.Id, Name = c.Name });
            PagedCategories = new PagedEnumerable<CategoryListModel>(categories, pageSize);
            UpdateList(PagedCategories.Next());
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
