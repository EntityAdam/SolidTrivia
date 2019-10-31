using System;

namespace SolidTrivia.Game.Data
{
    public static class SmsParser
    {
        public static SmsResult Parse(string originalString)
        {
            var result = new SmsResult()
            {
                OriginalString = originalString,
                FormattedString = FormatString(originalString)
            };

            var text = result.FormattedString;

            if (string.IsNullOrWhiteSpace(text))
            {
                return result;
            }

            result.SmsCommand = GetCommandType(text);

            if (result.SmsCommand == SmsCommandType.Join)
            {
                result.Session = GetSession(text);
            }

            return result;
        }

        private static string FormatString(string originalString)
        {
            return originalString.Trim().ToLower();
        }

        private static string GetSession(string originalString)
        {
            var session = "";

            var arr = originalString.Split(' ');

            //check if hypen is present
            if (arr[1].IndexOf('-') > 0)
            {
                return session = originalString.Split(' ')[1];
            }

            if (arr.Length >= 3)
            {
                return session = $"{arr[1]}-{arr[2]}";
            }
            return session;
        }

        private static SmsCommandType GetCommandType(string text)
        {
            var arr = text.Split(' ');
            var firstWord = arr[0];

            if (string.Equals("join", firstWord, StringComparison.InvariantCultureIgnoreCase))
            {
                return SmsCommandType.Join;
            }

            if (string.Equals("leave", firstWord, StringComparison.InvariantCultureIgnoreCase) && (arr.Length == 1))
            {
                return SmsCommandType.Leave;
            }

            if (string.Equals("dispute", firstWord, StringComparison.InvariantCultureIgnoreCase) || string.Equals("judges", firstWord, StringComparison.InvariantCultureIgnoreCase))
            {
                return SmsCommandType.Dispute;
            }

            return SmsCommandType.Response;
        }
    }

    public class SmsResult
    {
        public string OriginalString { get; set; }

        public string FormattedString { get; set; }

        public SmsCommandType SmsCommand { get; set; }

        public string Session { get; set; }
    }

    public enum SmsCommandType
    {
        Unknown,
        Join,
        InvalidJoin,
        Leave,
        Dispute,
        Response
    }
}