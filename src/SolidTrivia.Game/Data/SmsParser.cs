using SolidTrivia.Game.Models;
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
                return null;
            }

            result.UserCommand = GetCommandType(text);

            if (result.UserCommand == UserCommandType.Join)
            {
                var session = GetSession(text);
                if (session == null)
                {
                    return null;
                }
                result.SessionToJoin = session;
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

            if (arr.Length <= 1)
            {
                return null;
            }

            //check if hypen is present
            if (arr[1].IndexOf('-') > 0)
            {
                return session = originalString.Split(' ')[1];
            }

            if (arr.Length >= 3)
            {
                return session = $"{arr[1]}-{arr[2]}";
            }
            return null;
        }

        private static UserCommandType GetCommandType(string text)
        {
            var arr = text.Split(' ');
            var firstWord = arr[0];

            if (string.Equals("join", firstWord, StringComparison.InvariantCultureIgnoreCase))
            {
                return UserCommandType.Join;
            }

            if (string.Equals("leave", firstWord, StringComparison.InvariantCultureIgnoreCase) && (arr.Length == 1))
            {
                return UserCommandType.Leave;
            }

            if (string.Equals("dispute", firstWord, StringComparison.InvariantCultureIgnoreCase) || string.Equals("judges", firstWord, StringComparison.InvariantCultureIgnoreCase))
            {
                return UserCommandType.Dispute;
            }

            return UserCommandType.Response;
        }
    }

    public enum UserCommandType
    {
        Unknown,
        Join,
        InvalidJoin,
        Leave,
        Dispute,
        Response
    }
}