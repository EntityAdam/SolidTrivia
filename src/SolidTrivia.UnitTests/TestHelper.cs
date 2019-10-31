using SolidTrivia.Game;
using System.Linq;

namespace SolidTrivia.UnitTests
{
    public static class TestHelper
    {
        public static void AddPlayers(SolidTriviaGame game, GameSession session, int playerCount)
        {
            for (var i = 0; i < playerCount; i++)
            {
                var count = (game.AllPlayers().Count() + 1).ToString();
                game.Join(count, session.Id);
            }
        }

        public static void CreateSessions(SolidTriviaGame game, int sessionCount)
        {
            for (var i = 0; i < sessionCount; i++)
            {
                game.CreateNewSession();
            }
        }
    }
}