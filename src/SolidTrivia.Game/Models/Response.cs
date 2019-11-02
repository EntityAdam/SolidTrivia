using System;

namespace SolidTrivia.Game.Models
{
    //todo: handle per round score?
    public class Response
    {
        public Response(string playerId, Guid answerId, bool isCorrect, DateTime time)
        {
            PlayerId = playerId;
            AnswerId = answerId;
            IsCorrect = isCorrect;
            Time = time;
        }

        public string PlayerId { get; }
        
        public Guid AnswerId { get; }
        
        public bool IsCorrect { get; }

        public DateTime Time { get; }
    }
}