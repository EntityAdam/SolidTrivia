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
            Assert.Equal(SmsCommandType.Unknown, sms.SmsCommand);
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
            Assert.Equal(SmsCommandType.Join, result.SmsCommand);
            Assert.Equal("one-two", result.Session);
        }

        [Theory]
        [InlineData("leave")]
        [InlineData("LEAVE")]
        [InlineData("  LEAVE  ")]
        public void Leave(string smsBody)
        {
            var result = SmsParser.Parse(smsBody);
            Assert.Equal(SmsCommandType.Leave, result.SmsCommand);
        }

        [Theory]
        [InlineData("leaver")]
        [InlineData("LEAVE it to beaver")]
        [InlineData("  LEAVE me alone")]
        [InlineData("buscuits and gravy")]
        public void DontAccidentallyLeave(string smsBody)
        {
            var result = SmsParser.Parse(smsBody);
            Assert.Equal(SmsCommandType.Response, result.SmsCommand);
        }
    }
}