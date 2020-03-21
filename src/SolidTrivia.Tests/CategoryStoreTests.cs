using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SolidTrivia.Tests
{
    public class CategoryStoreTests
    {
        public static Guid g1 => Guid.Parse("82734205-7b14-4922-9288-196e87513cf5");
        public static Guid g2 => Guid.Parse("0b1a9f60-8474-44d6-9127-0d1c6d8fe3a2");
        public static Guid g3 => Guid.Parse("c9f6e665-8a29-4b68-acab-6989220287b1");
        public static Guid g4 => Guid.Parse("baafda43-c036-49ad-b5fa-00af03499f20");
        public static Guid g5 => Guid.Parse("d518264c-02fe-45c4-afc9-376facef36f9");
        public static Guid g6 => Guid.Parse("1d5dcc60-cdae-4e90-b15e-0d38cc884bc5");

        [Fact]
        public void CatagoryNullChecks()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateCategory(null));
            Assert.Throws<ArgumentNullException>(() => facade.CreateCategory(""));
        }

        [Fact]
        public void CreateCategory()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());

            facade.CreateCategory("category1");

            var category1 = facade.ListCategories().First();
            Assert.Equal("category1", category1.Name);

            facade.CreateCategory("category2");
            facade.CreateCategory("category3");
            var catagories = facade.ListCategories();
            Assert.Equal(3, catagories.Count());
        }

        [Fact]
        public void CreateCategory_CantCreateDuplicate()
        {
            var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
            facade.CreateCategory("category1");
            Assert.True(facade.CategoryExists("category1"));
            Assert.Throws<ArgumentException>(() => facade.CreateCategory("category1"));
        }

        //[Fact]
        //public void DeleteCategory()
        //{
        //    var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
        //    facade.CreateNewQuestion(new NewQuestion() { Id = g1 });
        //    facade.CreateCategory("category1");
        //    facade.CreateCategory("category2");
        //    facade.CreateCategory("category3");

        //    var catagories = facade.ListCategories();
        //    Assert.Equal(3, catagories.Count());

        //    facade.DeleteCategory(1);
        //    catagories = facade.ListCategories();
        //    Assert.Equal(2, catagories.Count());
        //}

        //[Fact]
        //public void RenameCategory()
        //{
        //    var facade = new QuestionFacade(new BoardStoreDummy(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy());
        //    facade.CreateCategory("category1");
        //    var category1 = facade.GetCategory(1);
        //    facade.RenameCategory(1, "newName");
        //    Assert.Equal("newName", category1.Name);
        //}

        //[Fact]
        //public void AddCategoryToBoard()
        //{
        //    var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
        //    facade.CreateBoard("name");
        //    facade.CreateCategory("category1");
        //    facade.CreateCategory("category2");
        //    facade.AddCategoryToBoard(1, 1);
        //    facade.AddCategoryToBoard(1, 2);
        //    List<NewCategory> categories = facade.ListCategoriesOfBoard(1).ToList();
        //    Assert.Equal(2, categories.Count());
        //}



        //[Fact]
        //public void AddQuestionToCategory()
        //{
        //    var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
        //    facade.CreateCategory("category1");
        //    facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
        //    facade.CreateNewQuestion(new NewQuestion() { Id = 2 });
        //    facade.AddQuestionToCategory(1, 1);
        //    Assert.Throws<ArgumentException>(() => facade.AddQuestionToCategory(1, 1)); //should fail
        //    facade.AddQuestionToCategory(2, 1);
        //    var questions = facade.ListQuestionsOfCategory(1);
        //    Assert.Equal(2, questions.Count());
        //}

        //[Fact]
        //public void DeleteCategoryOfBoard()
        //{
        //    var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy());
        //    facade.CreateBoard("name");
        //    facade.CreateCategory("category1");
        //    facade.CreateCategory("category2");
        //    facade.CreateCategory("category3");
        //    facade.AddCategoryToBoard(1, 1);
        //    facade.AddCategoryToBoard(1, 2);
        //    facade.AddCategoryToBoard(1, 3);
        //    facade.DeleteCategoryOfBoard(1, 1);
        //    List<NewCategory> categories = facade.ListCategoriesOfBoard(1).ToList();
        //    Assert.Equal(2, categories.Count());
        //}

        //[Fact]
        //public void CategoryBoard()
        //{
        //    var facade = new QuestionFacade(new BoardStoreMock(), new CategoryStoreMock(), new CommentStoreDummy(), new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy());
        //    facade.CreateBoard("name1");
        //    facade.CreateBoard("name2");
        //    facade.CreateCategory("category1");
        //    facade.CreateCategory("category2");
        //    facade.AddCategoryToBoard(1, 1);
        //    facade.AddCategoryToBoard(2, 2);

        //    facade.RemoveCategoryFromBoard(1, 1);
        //    facade.RemoveCategoryFromBoard(2, 2);
        //}
    }
}
