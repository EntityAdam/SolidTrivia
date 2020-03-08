using SolidTrivia.Questions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Tests
{
    internal class CategoryStoreMock : ICategoryStore
    {
        public List<NewCategory> Categories { get; set; } = new List<NewCategory>();

        public CategoryStoreMock()
        {

        }

        public void Create(string categoryName) => Categories.Add(new NewCategory() { Id = NewId(), Name = categoryName });
        public void DeleteCategory(int categoryId) => Categories.RemoveAll(c => c.Id == categoryId && c.BoardId == null);
        public void DeleteCategoryOfBoard(int boardId, int categoryId) => Categories.RemoveAll(c => c.Id == categoryId && c.BoardId == boardId);
        public NewCategory GetById(int categoryId) => Categories.Where(c => c.BoardId == null).First(c => c.Id == categoryId);
        public NewCategory GetCategoryOfBoard(int boardId, int categoryId) => Categories.Where(c => c.BoardId == boardId).First(c => c.Id == categoryId);
        public IEnumerable<NewCategory> ListCategories() => Categories.Where(c => c.BoardId == null);
        public IEnumerable<NewCategory> ListCategoriesOfBoard(int boardId) => Categories.Where(c => c.BoardId == boardId);
        public void AddCategoryToBoard(int boardId, int categoryId) => Categories.Single(c => c.Id == categoryId).BoardId = boardId;

        private int NewId()
        {
            var id = 1;
            if (Categories.Count() > 0)
            {
                id = Categories.Max(c => c.Id) + 1;
            }
            return id;
        }

        public bool Exists(int categoryId) => Categories.Any(c => c.Id == categoryId);

        public void Rename(int categoryId, string newName) => GetById(categoryId).Name = newName;

        public bool ExistsOrdinalIgnoreCase(string categoryName) => Categories.Any(c=> string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));
    }
}
