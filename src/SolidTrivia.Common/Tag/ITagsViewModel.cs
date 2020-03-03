using System.ComponentModel;

namespace SolidTrivia.Common
{
    public interface ITagsViewModel
    {
        IBlazorCommand NextPageCommand { get; set; }

        IBlazorCommand PrevPageCommand { get; set; }

        IBlazorCommand UpdateCommand { get; set; }

        IBlazorCommand DeleteCommand { get; set; }

        BindingList<TagModel> Tags { get; set; }

        void Load();
    }
}