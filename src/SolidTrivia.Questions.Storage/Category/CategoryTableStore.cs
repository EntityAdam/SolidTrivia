using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;

namespace SolidTrivia.Questions.Storage.Category
{
    public class CategoryTableStore : ICategoryStore
    {
        private const string categoryTableName = "Category";
        private static CloudStorageAccount CloudStorageAccount = null;
        private static CloudTable CloudTable = null;

        public CategoryTableStore()
        {
            CloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTable = CategoryThing.GetTable(CloudStorageAccount, categoryTableName);
        }

        public void AddToBoard(Guid boardId, Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public void Create(string categoryName)
        {
            var entity = new CategoryEntity("system", Guid.NewGuid())
            {
                Name = categoryName
            };
            CategoryThing.InsertOrMergeEntityAsync(CloudTable, entity).Wait();
        }

        public void Delete(Guid categoryId) //need to change to Guid
        {
            var entity = new CategoryEntity("system", categoryId);
            CategoryThing.DeleteEntityAsync(CloudTable, entity).Wait();
        }

        public bool Exists(Guid categoryId)
        {
            try
            {
                var cat = GetById(categoryId);
                if (cat != null) return true;
                
            }
            catch (Exception)
            {
                return false;
            }
            
            return false;
        }

        public bool ExistsOrdinalIgnoreCase(string categoryName)
        {
            throw new NotImplementedException();
        }

        public NewCategory GetById(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListAvailable(Guid boardId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewCategory> ListCategoriesOfBoard(Guid boardId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromBoard(Guid boardId, Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public void Rename(Guid categoryId, string newName)
        {
            throw new NotImplementedException();
        }
    }

    public static class CategoryThing
    {
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public static CloudTable GetTable(CloudStorageAccount storageAccount, string tableName)
        {
            try
            {
                // Create a table client for interacting with the table service
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
               

                // Create a table client for interacting with the table service 
                CloudTable table = tableClient.GetTableReference(tableName);

                if (table.CreateIfNotExists())
                {
                    Console.WriteLine("table created");
                }
                else
                {
                    Console.WriteLine("table exists");
                }

                return table;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public static async Task<CategoryEntity> InsertOrMergeEntityAsync(CloudTable table, CategoryEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                CategoryEntity insertedCustomer = result.Result as CategoryEntity;

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task DeleteEntityAsync(CloudTable table, CategoryEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                TableOperation deleteOperation = TableOperation.Delete(entity);
                TableResult result = await table.ExecuteAsync(deleteOperation);
                CategoryEntity entityToDelete = result.Result as CategoryEntity;

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
