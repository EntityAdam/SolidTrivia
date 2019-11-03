using System;

namespace SolidTrivia.Game.Models
{
    public class Response
    {
        public Response(string playerId, Guid answerId, string text, bool isCorrect, DateTime time)
        {
            PlayerId = playerId;
            AnswerId = answerId;
            IsCorrect = isCorrect;
            Time = time;
        }

        public string PlayerId { get; }
        
        public Guid AnswerId { get; }

        public string Text { get; }
        
        public bool IsCorrect { get; }

        public DateTime Time { get; }
    }
}