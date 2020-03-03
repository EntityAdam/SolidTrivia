﻿using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    public class EditTagViewModel : BindableBase, ITagEditViewModel
    {
        private readonly IQuestionFacade facade;

        public EditTagViewModel(IQuestionFacade facade)
        {
            this.facade = facade;
        }

        public TagEditModel EditModel { get; set; }
        public IBlazorCommand RenameCommand { get; set; }

        public void Load(int categoryId)
        {
            var cat = facade.GetCategory(categoryId);
            EditModel = new TagEditModel()
            {
                Id = cat.Id,
                Name = cat.Name
            };

            RenameCommand = new BlazorCommand(
                () => RenameCategory(),
                () => !string.IsNullOrEmpty(EditModel.Name) // and category exists.
            );
        }

        public void RenameCategory() => facade.RenameCategory(EditModel.Id, EditModel.Name);
    }
}
