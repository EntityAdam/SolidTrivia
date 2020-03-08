using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common.Board.Create
{
    public class BoardCreateViewModel
    {
        private readonly IQuestionFacade facade;

        public BoardCreateViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
        }

        public void Create() => facade.CreateBoard("");


    }

    public class BoardCreateModel
    {
        public string Name { get; set; }
    }
}
