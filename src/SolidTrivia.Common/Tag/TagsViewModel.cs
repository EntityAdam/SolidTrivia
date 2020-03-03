using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class TagsViewModel : BindableBase, ITagsViewModel
    {
        private const int defaultPageSize = 4;

        private readonly IQuestionFacade facade;

        private PagedEnumerable<TagModel> PagedTags { get; set; }

        public TagsViewModel(IQuestionFacade facade)
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

            DeleteCommand = new BlazorCommand<int>(
                (id) => Delete(id),
                (id) => TagExists(id)
            );
        }

        public BindingList<TagModel> Tags { get; set; } = new BindingList<TagModel>();

        public IBlazorCommand NextPageCommand { get; set; }
        public IBlazorCommand PrevPageCommand { get; set; }
        public IBlazorCommand UpdateCommand { get; set; }
        public IBlazorCommand DeleteCommand { get; set; }

        private void Prev() => UpdateList(PagedTags.Prev());

        private void Next() => UpdateList(PagedTags.Next());

        public void Load()
        {
            var tags = facade.ListTags().Select(x => new TagModel() { Id = x.Id, Name = x.Name });
            PagedTags = new PagedEnumerable<TagModel>(tags, defaultPageSize);
            UpdateList(PagedTags.Next());
        }


        private void UpdateList(IEnumerable<TagModel> page)
        {
            Tags.Clear();
            foreach (var c in page)
            {
                Tags.Add(new TagModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }

        private bool TagExists(int id) => facade.TagExists(id);
        private void Delete(int tagId) => facade.DeleteTag(tagId);
    }

    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    //REAL TIME TAG CLOUD?
    public class TagCloudModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
    }
}
