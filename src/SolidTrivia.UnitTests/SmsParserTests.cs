using SolidTrivia.Game.Data;
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
            Assert.Equal("one-two", result.Session);
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
    }
}