using System.Collections.Generic;

namespace SolidTrivia.Game
{
    public interface ISolidTrivia
    {
        GameSession CreateNewSession();

        void EndGameSession(string sessionId);

        int ActiveSessions();

        GameSession GetSession(string sessionId);

        IEnumerable<SessionInfo> GetSessionsInfo();

        (bool, string) Join(string smsNumber, string sessionId);

        (bool, string) Leave(string smsNumber);

        GameResponse ProcessUserMessage(string smsNumber, string body);
    }
}