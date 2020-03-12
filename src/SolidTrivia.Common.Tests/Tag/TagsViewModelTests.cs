using SolidTrivia.Questions;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System;

namespace SolidTrivia.Common.Tests
{
    public class TagsViewModelTests : IClassFixture<TagFacadeFixture>
    {
        TagFacadeFixture fixture;

        public TagsViewModelTests(TagFacadeFixture fixture)
        {
            this.fixture = fixture;
        }
        public IQuestionFacade Facade => fixture.Facade;
        public TagCreateViewModel CreateVm => fixture.CreateVm;
        public TagEditViewModel EditVm => fixture.EditVm;
        public TagDeleteViewModel DeleteVm => fixture.DeleteVm;
        public TagListViewModel ListVm => fixture.ListVm;

        [Fact]
        public void CreateCommand_CanExecute_Exercise()
        {
            CreateVm.TagName = "tag";
            Assert.True(CreateVm.CreateCommand.CanExecute(null));

            CreateVm.TagName = "";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));
        }

        [Fact]
        public void CreateCommand_CanExecute_ShouldBeFalse_WithInvalidTags()
        {
            //XSS
            CreateVm.TagName = "<script>";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));

            //invalid chars
            CreateVm.TagName = "tim^tebow";
            Assert.False(CreateVm.CreateCommand.CanExecute(null));
        }

        [Fact]
        public void CreateCommand_CanExecute_ShouldBeTrue_WithSpecialChars()
        {
            //allowed chars
            CreateVm.TagName = "c++";
            Assert.True(CreateVm.CreateCommand.CanExecute(null));

            CreateVm.TagName = "c#";
            Assert.True(CreateVm.CreateCommand.CanExecute(null));
        }


        [Fact]
        public void CreateCommand_Execute_ShouldCreateTags()
        {
            CreateVm.TagName = "tag1";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.TagName = "tag2";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.TagName = "tag3";
            CreateVm.CreateCommand.Execute(null);

            ListVm.Load(pageSize: 25);
            Assert.Contains("tag1", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("tag2", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("tag3", ListVm.Tags.Select(x => x.Name));
        }

        [Fact]
        public void CreateCommand_Execute_CantCreateDuplicateTags()
        {
            CreateVm.TagName = "duplicateTag";
            CreateVm.CreateCommand.Execute(null);

            CreateVm.TagName = "duplicateTag";
            Assert.Throws<ArgumentException>(() => CreateVm.CreateCommand.Execute(null));

            ListVm.Load(pageSize: 25);
            Assert.Contains("duplicateTag", ListVm.Tags.Select(x => x.Name));
        }

        [Fact]
        public void Tags_Paging()
        {
            ListVm.Load(pageSize: 3);

            Assert.True(ListVm.NextPageCommand.CanExecute(null));
            Assert.False(ListVm.PrevPageCommand.CanExecute(null));
            Assert.Equal(3, ListVm.Tags.Count());
            Assert.Contains("TagA", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("TagB", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("TagC", ListVm.Tags.Select(x => x.Name));

            ListVm.NextPageCommand.Execute(null);

            Assert.Equal(3, ListVm.Tags.Count());
            Assert.Contains("TagD", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("TagE", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("TagF", ListVm.Tags.Select(x => x.Name));

            Assert.True(ListVm.PrevPageCommand.CanExecute(null));

            ListVm.PrevPageCommand.Execute(null);
            
            Assert.Equal(3, ListVm.Tags.Count());
            Assert.Contains("TagA", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("TagB", ListVm.Tags.Select(x => x.Name));
            Assert.Contains("TagC", ListVm.Tags.Select(x => x.Name));
        }

        [Fact]
        public void Tags_Paging2()
        {
            ListVm.Load(pageSize: 99);
            Assert.False(ListVm.NextPageCommand.CanExecute(null));
            Assert.False(ListVm.PrevPageCommand.CanExecute(null));
        }

        [Fact]
        public void Tags_Rename()
        {
            ListVm.Load(pageSize: 99);

            var tag = ListVm.Tags.First();
            Assert.True(tag.Id == 1);
            Assert.True(tag.Name == "TagA");

            EditVm.Load(tag.Id);
            
            Assert.True(EditVm.EditModel.NewName == EditVm.EditModel.OldName);
            Assert.False(EditVm.RenameCommand.CanExecute(null));

            EditVm.EditModel.NewName = "TagZ";
            Assert.True(EditVm.RenameCommand.CanExecute(null));

            EditVm.RenameCommand.Execute(null);

            ListVm.Load(pageSize: 99);
            var renamedTag = ListVm.Tags.First();
            Assert.True(renamedTag.Id == 1);
            Assert.True(renamedTag.Name == "TagZ");
        }

        [Fact]
        public void Tags_Delete()
        {
            CreateVm.TagName = "tagToDelete";
            CreateVm.CreateCommand.Execute(null);
            ListVm.Load(pageSize: 99);
            Assert.Contains("tagToDelete", ListVm.Tags.Select(x => x.Name));

            var tagModel = ListVm.Tags.Single(x=>x.Name == "tagToDelete");

            DeleteVm.Load(tagModel.Id);

            Assert.True(DeleteVm.DeleteCommand.CanExecute(null));
            DeleteVm.DeleteCommand.Execute(null);

            ListVm.Load(pageSize: 99);
            Assert.DoesNotContain("tagToDelete", ListVm.Tags.Select(x => x.Name));
        }
    }
}
