using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EntityAdam.IdGenerator.Tests
{
    public class IdGeneratorTests
    {
        [Fact]
        public void IdGenerator_DoesNotProduceDuplicates()
        {
            var service = new IdGeneratorService();

            var list = new HashSet<string>();

            for (var i = 0; i < 200000; i++)
            {
                list.Add(service.GetNext());
            }

            Assert.Equal(200000, list.Count());
        }
    }
}
