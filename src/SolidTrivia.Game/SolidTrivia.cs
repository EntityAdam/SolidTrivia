using SolidTrivia.Game.Data;
using SolidTrivia.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidTrivia.Game
{
    public class SolidTriviaGame : ISolidTrivia
    {
        private readonly List<GameSession> gameSessions;

        public SolidTriviaGame()
        {
            gameSessions = new List<GameSession>();
        }

        public int ActiveSessions() => gameSessions.Count();

        public GameSession CreateNewSession()
        {
            var id = IdGenerator.GetNext(gameSessions.Select(g => g.Id).ToList());
            var session = new GameSession(id);
            gameSessions.Add(session);
            return session;
        }

        public void EndGameSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) throw new ArgumentNullException(nameof(sessionId));
            var session = GetSession(sessionId);
            gameSessions.Remove(session);
        }

        public GameSession GetSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) throw new ArgumentNullException(nameof(sessionId));
            try
            {
                return gameSessions.Single(g => g.Id == sessionId);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Session does not exist.", ex);
            }
        }

        //outcomes
        //true : playerId
        //false : errorMessage
        public (bool, string) Join(string smsNumber, string sessionId)
        {
            string errorMessage = null;

            if (string.IsNullOrEmpty(sessionId)) throw new ArgumentNullException(nameof(sessionId));
            if (smsNumber == null) throw new ArgumentNullException(nameof(smsNumber));

            var session = gameSessions.SingleOrDefault(s => s.Id == sessionId);

            if (session == null)
            {
                errorMessage = $"session does not exist. {sessionId}";
                return (false, errorMessage);
            }

            var players = AllPlayers();

            if (players.Any(p => p.SmsNumber.Contains(smsNumber)))
            {
                errorMessage = $"you are already registered for a game, to join a new game send command LEAVE";
                return (false, errorMessage);
            }

            var player = new Player(smsNumber, session.Id, IdGenerator.GetNext(players.Select(p => p.Id).ToList()));
            session.Join(player);
            return (true, player.Id);
        }

        public (bool, string) Leave(string smsNumber)
        {
            string errorMessage = null;

            if (string.IsNullOrEmpty(smsNumber)) throw new ArgumentNullException(nameof(smsNumber));

            var players = AllPlayers();
            var player = players.SingleOrDefault(p => p.SmsNumber == smsNumber);

            if (player == null)
            {
                errorMessage = $"I don't know you";
                return (false, errorMessage);
            }

            var session = gameSessions.Single(p => p.Id == player.SessionId);

            //possible race condition exception?
            //todo: add SessionExists?

            session.Leave(player);
            return (true, "You have left the game");
        }

        public IEnumerable<Player> AllPlayers()
        {
            return gameSessions.SelectMany(s => s.Players);
        }

        public IEnumerable<SessionInfo> GetSessionsInfo()
        {
            return gameSessions.Select(s => new SessionInfo()
            {
                Id = s.Id,
                PlayerCount = s.Players.Count()
            });
        }

        public SmsResponseMessage ProcessUserSmsMessage(string smsNumber, string body)
        {
            var response = new SmsResponseMessage();
            var parsedResult = SmsParser.Parse(body);

            if (parsedResult.UserCommand == UserCommandType.Join)
            {
                var joinResult = Join(smsNumber, parsedResult.Session);
                response.Body = joinResult.Item2;
            }
            else if (parsedResult.UserCommand == UserCommandType.Leave)
            {
                var leaveResult = Join(smsNumber, parsedResult.Session);
                response.Body = leaveResult.Item2;
            }

            return response;
        }
    }
}