using SolidTrivia.Game.Data;

namespace SolidTrivia.Game.Models
{
    public class SmsResult
    {
        public string OriginalString { get; set; }

        public string FormattedString { get; set; }

        public UserCommandType UserCommand { get; set; }

        public string SessionToJoin { get; set; }
    }
}