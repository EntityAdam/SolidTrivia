namespace SolidTrivia.Game.Models
{
    public class Player : BindableBase
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
    }
}