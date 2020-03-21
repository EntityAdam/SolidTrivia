using System.Text.Json.Serialization;

namespace SolidTrivia.Questions.Import
{
    public class OpenTdbResultModel
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; }

        //[JsonPropertyName("incorrect_answers")]
        //public string[] IncorrectAnswers { get; set; }

        //public string license => "https://creativecommons.org/licenses/by-sa/4.0/";
        //public string source => "https://opentdb.com";
    }
}
