using Microsoft.Azure.Cosmos.Table;
using System;

namespace SolidTrivia.Questions.Storage
{
    public class QuestionEntity : TableEntity
    {
        public QuestionEntity()
        {
        }

        public QuestionEntity(string category, string questionType, Guid questionId)
        {
            PartitionKey = $"{category}_{questionType}";
            RowKey = questionId.ToString();
        }
    }
}
