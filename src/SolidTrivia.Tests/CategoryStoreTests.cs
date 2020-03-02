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
        [Fact]
        public void CatagoryNullChecks()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreDummy());
            Assert.Throws<ArgumentNullException>(() => facade.CreateCategory(null));
            Assert.Throws<ArgumentNullException>(() => facade.CreateCategory(""));
        }

        [Fact]
        public void CreateCategory()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreDummy());
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
            facade.CreateCategory("category1");
            facade.CreateCategory("category2");
            facade.CreateCategory("category3");

            var catagories = facade.ListCategories();
            Assert.Equal(3, catagories.Count());
        }

        [Fact]
        public void DeleteCategory()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreDummy());
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
            facade.CreateCategory("category1");
            facade.CreateCategory("category2");
            facade.CreateCategory("category3");

            var catagories = facade.ListCategories();
            Assert.Equal(3, catagories.Count());

            facade.DeleteCategory(1);
            catagories = facade.ListCategories();
            Assert.Equal(2, catagories.Count());
        }
        [Fact]
        public void AddCategoryToBoard()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreMock());
            facade.CreateBoard("name");
            facade.CreateCategory("category1");
            facade.CreateCategory("category2");
            facade.AddCategoryToBoard(1, 1);
            facade.AddCategoryToBoard(1, 2);
            List<NewCategory> categories = facade.ListCategoriesOfBoard(1).ToList();
            Assert.Equal(2, categories.Count());
        }


        [Fact]
        public void AddQuestionToCategory()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreMock());
            facade.CreateCategory("category1");
            facade.CreateNewQuestion(new NewQuestion() { Id = 1 });
            facade.CreateNewQuestion(new NewQuestion() { Id = 2 });
            facade.AddQuestionToCategory(1, 1);
            Assert.Throws<ArgumentException>(() => facade.AddQuestionToCategory(1, 1)); //should fail
            facade.AddQuestionToCategory(2, 1);
            var questions = facade.ListQuestionsOfCategory(1);
            Assert.Equal(2, questions.Count());
        }

        [Fact]
        public void DeleteCategoryOfBoard()
        {
            var facade = new QuestionFacade(new QuestionStoreMock(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreMock());
            facade.CreateBoard("name");
            facade.CreateCategory("category1");
            facade.CreateCategory("category2");
            facade.CreateCategory("category3");
            facade.AddCategoryToBoard(1, 1);
            facade.AddCategoryToBoard(1, 2);
            facade.AddCategoryToBoard(1, 3);
            facade.DeleteCategoryOfBoard(1, 1);
            List<NewCategory> categories = facade.ListCategoriesOfBoard(1).ToList();
            Assert.Equal(2, categories.Count());
        }

        [Fact]
        public void CategoryBoard()
        {
            var facade = new QuestionFacade(new QuestionStoreDummy(), new TagStoreDummy(), new VoteStoreDummy(), new CommentStoreDummy(), new CategoryStoreMock(), new BoardStoreMock());
            facade.CreateBoard("name1");
            facade.CreateBoard("name2");
            facade.CreateCategory("category1");
            facade.CreateCategory("category2");
            facade.AddCategoryToBoard(1, 1);
            facade.AddCategoryToBoard(2, 2);

            facade.GetCategoryOfBoard(1, 1);
            facade.GetCategoryOfBoard(2, 2);
        }
    }
}
