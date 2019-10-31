namespace SolidTrivia.Game.Models
{
    public class SmsResponseMessage
    {
        public bool Success { get; set; }
        public string Body { get; set; }
        public bool HasMessage => !string.IsNullOrEmpty(Body);
    }
}