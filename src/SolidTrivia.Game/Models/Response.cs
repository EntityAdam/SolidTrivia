using System;

namespace SolidTrivia.Game.Models
{
    //todo: handle per round score?
    public class Response
    {
        public Response(string sessionId, string playerId, bool isCorrect, DateTime time)
        {
            PlayerId = playerId;
            SessionId = sessionId;
            IsCorrect = isCorrect;
            Time = time;
        }

        public string PlayerId { get; }

        public string SessionId { get; }

        public bool IsCorrect { get; }

        public DateTime Time { get; }
    }
}