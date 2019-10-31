namespace SolidTrivia.Game
{
    public class GameResponse
    {
        public bool HasResponse => !string.IsNullOrEmpty(Body);
        public string Body { get; set; }
    }
}