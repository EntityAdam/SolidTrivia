using SolidTrivia.Game.Models;
using System.Collections.Generic;

namespace SolidTrivia.Game
{
    public interface ISolidTrivia
    {
        GameSession CreateNewSession();

        void EndGameSession(string sessionId);

        int ActiveSessions();

        GameSession GetSessionById(string sessionId);

        IEnumerable<SessionInfo> GetSessionsInfo();

        (bool, string) Join(string smsNumber, string sessionId);

        (bool, string) Leave(string smsNumber);

        SmsResponseMessage ProcessUserSmsMessage(string smsNumber, string body);
    }
}