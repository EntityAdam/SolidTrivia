using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos.Table;
using SolidTrivia.Questions.Storage.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SolidTrivia.Questions.Storage.Tests
{

    public class IdentityUserAdapter : TableEntityAdapter<IdentityUser>
    {
        public IdentityUserAdapter()
        {
        }

        public IdentityUserAdapter(IdentityUser identityUser) : base(identityUser)
        {
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return base.WriteEntity(operationContext);
        }
    }


    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var user = new IdentityUser("adam@test.com");
            var adapter = new IdentityUserAdapter(user);

        }
    }
}
