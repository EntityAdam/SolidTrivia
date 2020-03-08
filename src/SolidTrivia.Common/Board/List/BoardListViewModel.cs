using SolidTrivia.Questions;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace SolidTrivia.Common
{
    public class BoardListViewModel : BindableBase
    {
        private const int defaultPageSize = 4; // TODO : Add PageSize to UI

        private readonly IQuestionFacade facade;

        private PagedEnumerable<BoardListModel> PagedBoards { get; set; }

        public BoardListViewModel(IQuestionFacade facade)
        {
            this.facade = facade;

            NextPageCommand = new BlazorCommand(
                () => Next(),
                () => (PagedBoards != null) ? PagedBoards.CanExecuteNext : false
            );
            PrevPageCommand = new BlazorCommand(
                () => Prev(),
                () => (PagedBoards != null) ? PagedBoards.CanExecutePrev : false
            );

            UpdateCommand = new BlazorCommand(() => Load());
        }

        private void Prev() => UpdateList(PagedBoards.Prev());

        private void Next() => UpdateList(PagedBoards.Next());

        public BindingList<BoardListModel> Boards { get; set; } = new BindingList<BoardListModel>();

        public IBlazorCommand NextPageCommand { get; set; }
        public IBlazorCommand PrevPageCommand { get; set; }
        public IBlazorCommand UpdateCommand { get; set; }

        public void Load(int pageSize = defaultPageSize)
        {
            var boards = facade.ListCategories().Select(c => new BoardListModel() { Id = c.Id, Name = c.Name });
            PagedBoards = new PagedEnumerable<BoardListModel>(boards, pageSize);
            UpdateList(PagedBoards.Next());
        }


        private void UpdateList(IEnumerable<BoardListModel> page)
        {
            Boards.Clear();
            foreach (var c in page)
            {
                Boards.Add(new BoardListModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }
    }

}
