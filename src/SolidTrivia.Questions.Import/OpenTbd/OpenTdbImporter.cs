using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace SolidTrivia.Questions.Import
{


    public static class OpenTdbImporter
    {
        private static readonly HttpClient client = new HttpClient();

        private const string sessionToken = "OpenTdbImporter";
        private const string encoding = "base64";
        private const string count = "50";
        private const string categoryendpoint = "https://opentdb.com/api_category.php";
        private const string apiendpoint = "https://opentdb.com/api.php";

        //private string endpoint = $"{apiendpoint}";


        public static async Task<OpenTdbModel> ImportFromFile()
        {
            //var allText = File.ReadAllTextAsync("sample-opentdb-data.json");
            //return await ConvertFromJson(await allText);

            var stream = File.OpenText("sample-opentdb-data.json");
            return await JsonSerializer.DeserializeAsync<OpenTdbModel>(stream.BaseStream);

        }
        public static async Task<OpenTdbModel> FetchFromApi()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync($"{apiendpoint}?amount=5");
            return await ConvertFromJson(streamTask);
            //return await JsonSerializer.DeserializeAsync<OpenTdbModel>(await streamTask);
        }


        public async static Task<OpenTdbModel> ConvertFromJson(Task<Stream> streamTask)
        {
            return await JsonSerializer.DeserializeAsync<OpenTdbModel>(await streamTask);
        }

        //public async static Task<OpenTdbModel> ConvertFromJson(string allText)
        //{
        //    return JsonSerializer.Deserialize<OpenTdbModel>(allText);
        //}

        public static IEnumerable<NewQuestion> ConvertFromOpenTdb(IEnumerable<OpenTdbResultModel> questions)
        {
            return questions.Select(q => new NewQuestion()
            {
                Category = q.Category,
                QuestionType = (q.Difficulty == "boolean") ? NewQuestionType.TrueFalse : NewQuestionType.MultipleChoice,
                MarkdownContent = q.Question,
                CorrectResponse = q.CorrectAnswer
            });
        }
    }
}
