using System;

namespace SolidTrivia.Game.Models
{
    public class Response
    {
        //todo: get rid of weight - kip
        public Response(string playerId, Guid answerId, int weight, string text, bool isCorrect, DateTime time)
        {
            PlayerId = playerId;
            PromptId = answerId;
            Weight = weight;
            Text = text;
            IsCorrect = isCorrect;
            Time = time;
        }

        public string PlayerId { get; }

        public Guid PromptId { get; }

        public string Text { get; }

        public bool IsCorrect { get; }

        public DateTime Time { get; }

        private int Weight { get; }

        public int Score => IsCorrect ? (Weight * 100) : (Weight * -1 * 100);

        public GradeType Grade { get; private set; }
        
        public void GradeCorrect() => Grade = GradeType.Correct;

        public void GradeIncorrect() => Grade = GradeType.Incorrect;

        public enum GradeType
        {
            NotGraded,
            Correct,
            Incorrect
        }
    }
}