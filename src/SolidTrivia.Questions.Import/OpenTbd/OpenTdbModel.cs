using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SolidTrivia.Questions.Import
{
    //https://opentdb.com/

    public class OpenTdbModel
    {
        [JsonPropertyName("response_code")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("results")]
        public List<OpenTdbResultModel> Results { get; set; }
    }
}
