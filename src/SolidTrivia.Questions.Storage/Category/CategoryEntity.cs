using Microsoft.Azure.Cosmos.Table;
using System;

namespace SolidTrivia.Questions.Storage
{
    public class CategoryEntity : TableEntity
    {
        public CategoryEntity()
        {
        }

        public CategoryEntity(string user, Guid categoryId)
        {
            PartitionKey = user ?? "system";
            RowKey = categoryId.ToString();
        }

        public string Name { get; set; }
    }
}
