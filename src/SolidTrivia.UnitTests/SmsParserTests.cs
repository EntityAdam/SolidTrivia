using SolidTrivia.Game.Data;
using SolidTrivia.Game.Models;
using Xunit;

namespace SolidTrivia.UnitTests
{
    public class SmsParserTests
    {
        [Fact]
        public void Defaults()
        {
            var sms = new SmsResult();
            Assert.Equal(UserCommandType.Unknown, sms.UserCommand);
        }

        [Theory]
        [InlineData("join one-two")]
        [InlineData("JOIN ONE-two")]
        [InlineData("JOIN one-TWO")]
        [InlineData("JOIN one TWO")]
        [InlineData("JOIN one TWO   ")]
        [InlineData("   JOIN one TWO   ")]
        public void Join(string smsBody)
        {
            var result = SmsParser.Parse(smsBody);
            Assert.Equal(UserCommandType.Join, result.UserCommand);
            Assert.Equal("one-two", result.SessionToJoin);
        }

        [Theory]
        [InlineData("leave")]
        [InlineData("LEAVE")]
        [InlineData("  LEAVE  ")]
        public void Leave(string smsBody)
        {
            var result = SmsParser.Parse(smsBody);
            Assert.Equal(UserCommandType.Leave, result.UserCommand);
        }

        [Theory]
        [InlineData("leaver")]
        [InlineData("LEAVE it to beaver")]
        [InlineData("  LEAVE me alone")]
        [InlineData("buscuits and gravy")]
        public void DontAccidentallyLeave(string smsBody)
        {
            var result = SmsParser.Parse(smsBody);
            Assert.Equal(UserCommandType.Response, result.UserCommand);
        }


        [Fact]
        public void Test()
        {
            //nothing sent.. probably can't happen.
            var result1 = SmsParser.Parse(""); 
            Assert.Null(result1);

            //join with no session id supplied
            var result2 = SmsParser.Parse("join");
            Assert.Null(result2);

            //join with half a session id?
            var result3 = SmsParser.Parse("join invalid");
            Assert.Null(result3);
        }
    }
}