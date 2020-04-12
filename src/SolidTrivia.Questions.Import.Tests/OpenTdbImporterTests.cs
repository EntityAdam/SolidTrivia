using SolidTrivia.Questions.Storage.Category;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SolidTrivia.Questions.Import.Tests
{
    public class OpenTdbImporterTests
    {
        [Fact(Skip = "Actual IO call to API")]
        public async Task CanReachApi() { }

        [Fact]
        public async Task CanReadFromFile()
        {
            var x = await OpenTdbImporter.ImportFromFile();
            var y = x;
        }
        public static async Task CanDeserialize() { }
        public static async Task CanDecodeBase64() { }
        public static async Task CanDecodeUrl() { }

        [Fact]
        //[Fact(Skip = "Actual Convert and store operation")]
        public async Task RunImport()
        {

            var store = new CategoryTableStore();
            var request = await OpenTdbImporter.FetchFromApi();
            var questions = OpenTdbImporter.ConvertFromOpenTdb(request.Results);

            foreach (var q in questions)
            {
                store.Create(q.Category);
            }

        }
    }
}
