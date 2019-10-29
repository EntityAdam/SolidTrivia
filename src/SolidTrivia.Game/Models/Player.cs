namespace SolidTrivia.Game
{
    public class Player
    {
        public Player(string smsNumber, string sessionId, string id)
        {
            this.SmsNumber = smsNumber;
            this.Id = id;
            this.SessionId = sessionId;
        }
        public string SmsNumber { get; set; }

        public string SessionId { get; set; }

        public string Id { get; set; }

        public Score Score { get; set; }
    }
}
