using SolidTrivia.Questions;
using SolidTrivia.Tests;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace SolidTrivia.Common.Tests
{
    public class CategoryViewModelTests : IClassFixture<CategoryFacadeFixture>
    {
        readonly CategoryFacadeFixture fixture;

        public CategoryViewModelTests(CategoryFacadeFixture fixture)
        {
            this.fixture = fixture;
        }
        public IQuestionFacade Facade => fixture.Facade;
        public CategoryCreateViewModel CreateVm => fixture.CreateVm;
        public CategoryEditViewModel EditVm => fixture.EditVm;
        public CategoryDeleteViewModel DeleteVm => fixture.DeleteVm;
        public CategoryListViewModel ListVm => fixture.ListVm;

        [Fact]
        public void CreateCommand_CanExecute_Exercise()
        {
            CreateVm.CreateModel.Name = "category name";
            Assert.True(CreateVm.CreateCommand.CanExecute(null));

            CreateVm.CreateModel.Name = "";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));
        }

        [Fact]
        public void CreateCommand_CanExecute_ShouldBeFalse_WithInvalidTags()
        {
            //XSS
            CreateVm.CreateModel.Name = "<script>";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));

            //invalid chars
            CreateVm.CreateModel.Name = "tim^tebow";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));

            CreateVm.CreateModel.Name = "c++";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));

            CreateVm.CreateModel.Name = "c#";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));
        }

        [Fact]
        public void CreateCommand_CanExecute_ShouldBeTrue_WithSpecialChars()
        { 
        }


        [Fact]
        public void CreateCommand_Execute_ShouldCreateTags()
        {
            CreateVm.CreateModel.Name = "category1";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.CreateModel.Name = "category2";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.CreateModel.Name = "category3";
            CreateVm.CreateCommand.Execute(null);

            ListVm.Load(pageSize: 25);
            Assert.Contains("category1", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("category2", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("category3", ListVm.Categories.Select(x => x.Name));
        }

        [Fact]
        public void Categories_Paging()
        {
            ListVm.Load(pageSize: 3);

            Assert.True(ListVm.NextPageCommand.CanExecute(null));
            Assert.False(ListVm.PrevPageCommand.CanExecute(null));
            Assert.Equal(3, ListVm.Categories.Count());
            Assert.Contains("CategoryA", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("CategoryB", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("CategoryC", ListVm.Categories.Select(x => x.Name));

            ListVm.NextPageCommand.Execute(null);

            Assert.Equal(3, ListVm.Categories.Count());
            Assert.Contains("CategoryD", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("CategoryE", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("CategoryF", ListVm.Categories.Select(x => x.Name));

            Assert.True(ListVm.PrevPageCommand.CanExecute(null));

            ListVm.PrevPageCommand.Execute(null);

            Assert.Equal(3, ListVm.Categories.Count());
            Assert.Contains("CategoryA", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("CategoryB", ListVm.Categories.Select(x => x.Name));
            Assert.Contains("CategoryC", ListVm.Categories.Select(x => x.Name));
        }

        [Fact]
        public void Categories_Paging2()
        {
            ListVm.Load(pageSize: 99);
            Assert.False(ListVm.NextPageCommand.CanExecute(null));
            Assert.False(ListVm.PrevPageCommand.CanExecute(null));
        }

        [Fact]
        public void Categories_Rename()
        {
            ListVm.Load(pageSize: 99);

            var tag = ListVm.Categories.First();
            //Assert.True(tag.Id == 1);
            Assert.True(tag.Name == "CategoryA");

            EditVm.Load(tag.Id);

            Assert.True(EditVm.EditModel.NewName == EditVm.EditModel.OldName);
            Assert.False(EditVm.RenameCommand.CanExecute(null));

            EditVm.EditModel.NewName = "CategoryZ";
            Assert.True(EditVm.RenameCommand.CanExecute(null));

            EditVm.RenameCommand.Execute(null);

            ListVm.Load(pageSize: 99);
            var renamedTag = ListVm.Categories.First();
            //Assert.True(renamedTag.Id == 1);
            Assert.True(renamedTag.Name == "CategoryZ");
        }

        [Fact]
        public void Tags_Delete()
        {
            CreateVm.CreateModel.Name = "categoryToDelete";
            CreateVm.CreateCommand.Execute(null);
            ListVm.Load(pageSize: 99);
            Assert.Contains("categoryToDelete", ListVm.Categories.Select(x => x.Name));

            var tagModel = ListVm.Categories.Single(x => x.Name == "categoryToDelete");

            DeleteVm.Load(tagModel.Id);

            Assert.True(DeleteVm.DeleteCommand.CanExecute(null));
            DeleteVm.DeleteCommand.Execute(null);

            ListVm.Load(pageSize: 99);
            Assert.DoesNotContain("categoryToDelete", ListVm.Categories.Select(x => x.Name));
        }
    }
}
