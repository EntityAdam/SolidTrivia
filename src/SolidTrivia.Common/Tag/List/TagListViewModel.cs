using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class TagListViewModel : BindableBase
    {
        private const int defaultPageSize = 4;

        private readonly IQuestionFacade facade;

        private PagedEnumerable<TagListModel> PagedTags { get; set; }

        public TagListViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            NextPageCommand = new BlazorCommand(
                () => Next(),
                () => (PagedTags != null) ? PagedTags.CanExecuteNext : false
            );
            PrevPageCommand = new BlazorCommand(
                () => Prev(),
                () => (PagedTags != null) ? PagedTags.CanExecutePrev : false
            );

            UpdateCommand = new BlazorCommand(() => Load());
        }

        public BindingList<TagListModel> Tags { get; set; } = new BindingList<TagListModel>();

        public IBlazorCommand NextPageCommand { get; set; }
        public IBlazorCommand PrevPageCommand { get; set; }
        public IBlazorCommand UpdateCommand { get; set; }

        private void Prev() => SetCurrentPageTo(PagedTags.Prev());
        private void Next() => SetCurrentPageTo(PagedTags.Next());


        public void Load(int pageSize = defaultPageSize)
        {
            var tags = facade.ListTags().Select(x => new TagListModel() { Id = x.Id, Name = x.Name });
            PagedTags = new PagedEnumerable<TagListModel>(tags, pageSize);
            SetCurrentPageTo(PagedTags.Next());
        }

        private void SetCurrentPageTo(IEnumerable<TagListModel> page)
        {
            Tags.Clear();
            foreach (var c in page)
            {
                Tags.Add(new TagListModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }
    }
}
