using SolidTrivia.Questions;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class QuestionListViewModel : BindableBase
    {
        private const int defaultPageSize = 4; // TODO : Add PageSize to UI

        private readonly IQuestionFacade facade;

        private PagedEnumerable<QuestionListModel> PagedQuestions { get; set; }

        public QuestionListViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            NextPageCommand = new BlazorCommand(
                () => Next(),
                () => (PagedQuestions != null) ? PagedQuestions.CanExecuteNext : false
            );
            PrevPageCommand = new BlazorCommand(
                () => Prev(),
                () => (PagedQuestions != null) ? PagedQuestions.CanExecutePrev : false
            );

            UpdateCommand = new BlazorCommand(() => Load());
        }

        private void Prev() => UpdateList(PagedQuestions.Prev());

        private void Next() => UpdateList(PagedQuestions.Next());

        public BindingList<QuestionListModel> Questions { get; set; } = new BindingList<QuestionListModel>();

        public IBlazorCommand NextPageCommand { get; set; }
        public IBlazorCommand PrevPageCommand { get; set; }
        public IBlazorCommand UpdateCommand { get; set; }

        public void Load(int pageSize = defaultPageSize)
        {
            var questions = facade.ListQuestions().Select(c => new QuestionListModel() { Id = c.Id, Content = c.MarkdownContent });
            PagedQuestions = new PagedEnumerable<QuestionListModel>(questions, pageSize);
            UpdateList(PagedQuestions.Next());
        }


        private void UpdateList(IEnumerable<QuestionListModel> page)
        {
            Questions.Clear();
            foreach (var c in page)
            {
                Questions.Add(new QuestionListModel()
                {
                    Id = c.Id,
                    Content = c.Content
                });
            }
        }
    }

}
