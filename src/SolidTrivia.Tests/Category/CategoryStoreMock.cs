using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Tests
{
    internal class CategoryStoreMock : ICategoryStore
    {
        public List<NewCategory> Categories { get; set; } = new List<NewCategory>();

        public List<BoardCategory> BoardCategories { get; set; } = new List<BoardCategory>();

        public CategoryStoreMock()
        {

        }

        #region Category
        //create
        public void Create(string categoryName) => Categories.Add(new NewCategory() { Id = NewId(), Name = categoryName });

        //edit
        public void Rename(int categoryId, string newName) => GetById(categoryId).Name = newName;

        //delete
        public void Delete(int categoryId) => Categories.RemoveAll(c => c.Id == categoryId);

        //list
        public NewCategory GetById(int categoryId) => Categories.First(c => c.Id == categoryId);
        public bool Exists(int categoryId) => Categories.Any(c => c.Id == categoryId);
        public bool ExistsOrdinalIgnoreCase(string categoryName) => Categories.Any(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));
        public IEnumerable<NewCategory> ListCategories() => Categories;
        #endregion

        #region Board Categories
        //create
        public void AddToBoard(int boardId, int categoryId)
        {
            if (BoardCategories.Any(x => x.BoardId == boardId && x.CategoryId == categoryId))
                throw new InvalidOperationException($"Cannot add duplicate category '{categoryId}' to board '{boardId}'");

            BoardCategories.Add(new BoardCategory() { BoardId = boardId, CategoryId = categoryId });
        }

        //edit

        //delete
        public void RemoveFromBoard(int boardId, int categoryId) => BoardCategories.RemoveAll(bc=>bc.BoardId == boardId && bc.CategoryId == categoryId);

        //list

        public IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId)
        {
            var boardCategoriesIds = BoardCategories.Where(bc => bc.BoardId == boardId).Select(bc => bc.CategoryId);
            return Categories.Where(c => boardCategoriesIds.Contains(c.Id));
        }

        public IEnumerable<NewCategory> ListAvailable(int boardId)
        {
            var boardCategoriesIds = BoardCategories.Where(bc => bc.BoardId == boardId).Select(bc => bc.CategoryId);
            return Categories.Where(c => !boardCategoriesIds.Contains(c.Id));
        }
        #endregion

        private int NewId()
        {
            var id = 1;
            if (Categories.Count() > 0)
            {
                id = Categories.Max(c => c.Id) + 1;
            }
            return id;
        }
    }

    public class BoardCategory
    {
        public int BoardId { get; set; }
        public int CategoryId { get; set; }
    }
}
