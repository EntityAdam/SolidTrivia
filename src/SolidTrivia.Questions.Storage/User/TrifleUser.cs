using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
using SolidTrivia.Questions.Storage.Category;
using System.Threading;
using System.Linq;

namespace SolidTrivia.Questions.Storage
{


    public class IdentityUserAdapter : TableEntityAdapter<IdentityUser>
    {
        public IdentityUserAdapter()
        {
        }

        public IdentityUserAdapter(IdentityUser identityUser) : base(identityUser)
        {
            this.PartitionKey = "user";
            this.RowKey = identityUser.Id;
        }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            this.OriginalEntity = EntityPropertyConverter.ConvertBack<IdentityUser>(properties, operationContext);
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return EntityPropertyConverter.Flatten(this.OriginalEntity, operationContext);
        }
    }

    public class TrifleUserStore : 
        IUserStore<IdentityUser>, 
        IUserEmailStore<IdentityUser>, 
        IUserPasswordStore<IdentityUser>, 
        IUserPhoneNumberStore<IdentityUser>, 
        IUserLoginStore<IdentityUser>
    {
        private const string userPartitionKey = "user";
        private const string userTableName = "Users";
        private static CloudStorageAccount CloudStorageAccount = null;
        private static CloudTable CloudTable = null;

        public TrifleUserStore()
        {
            CloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTable = CategoryThing.GetTable(CloudStorageAccount, userTableName);
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            return await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken = default)
        {
            return await DeleteUser(user, cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            //nothing to dispose
        }

        public async Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            var query = new TableQuery<IdentityUserAdapter>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userPartitionKey),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("NormalizedEmail", QueryComparisons.Equal, normalizedEmail)
                 ));

            return await UserQuery(query);
        }

        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await UserPointQuery(userPartitionKey, userId);
        }



        public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            var query = new TableQuery<IdentityUserAdapter>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userPartitionKey),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("NormalizedUserName", QueryComparisons.Equal, normalizedUserName)
                 ));

            return await UserQuery(query);
        }

        public Task<string> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash.Length > 0);
        }

        public async Task SetEmailAsync(IdentityUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task SetNormalizedEmailAsync(IdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await InsertOrMergeUser(user, cancellationToken);
        }


        private static async Task<IdentityResult> InsertOrMergeUser(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            try
            {
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
                var adapter = new IdentityUserAdapter(user);
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(adapter);
                TableResult result = await CloudTable.ExecuteAsync(insertOrMergeOperation);
                return IdentityResult.Success;
            }
            catch (StorageException ex)
            {
                return IdentityResult.Failed(new IdentityError() { Description = ex.Message });
            }
        }

        private static async Task<IdentityResult> DeleteUser(IdentityUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentException(nameof(user));
            try
            {
                var entity = new IdentityUserAdapter(user);
                TableOperation deleteOperation = TableOperation.Delete(entity);
                TableResult result = await CloudTable.ExecuteAsync(deleteOperation);
                return IdentityResult.Success;
            }
            catch (StorageException ex)
            {
                return IdentityResult.Failed(new IdentityError() { Description = ex.Message });
            }
        }

        private static async Task<IdentityUser> UserQuery(TableQuery<IdentityUserAdapter> query)
        {
            try
            {
                var results = new List<IdentityUserAdapter>();
                TableContinuationToken token = null;
                do
                {
                    TableQuerySegment<IdentityUserAdapter> segment = await CloudTable.ExecuteQuerySegmentedAsync(query, token);
                    token = segment.ContinuationToken;
                    results.AddRange(segment);
                }
                while (token != null);
                var result = results.SingleOrDefault()?.OriginalEntity; //todo assert single!
                return result;
            }
            catch (StorageException ex)
            {
                //todo log error
                throw;
            }
            catch (InvalidOperationException ex)
            {
                //todo log
                throw;
            }
        }
        private async Task<IdentityUser> UserPointQuery(string partitionKey, string userId)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<IdentityUserAdapter>("user", userId);
                TableResult result = await CloudTable.ExecuteAsync(retrieveOperation);
                var user = result.Result as IdentityUserAdapter;
                return user.OriginalEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task<string> GetPhoneNumberAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public async Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            await InsertOrMergeUser(user, cancellationToken);
        }


        public async Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            await InsertOrMergeUser(user, cancellationToken);
        }

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(IdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
