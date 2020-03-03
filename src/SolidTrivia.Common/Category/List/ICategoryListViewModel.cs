using System.ComponentModel;

namespace SolidTrivia.Common
{
    public interface ICategoryListViewModel
    {
        IBlazorCommand NextPageCommand { get; set; }
        BindingList<CategoryListModel> Page { get; set; }
        IBlazorCommand PrevPageCommand { get; set; }
        void Load();
    }
}