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
            var session = GetSessionById(sessionId);
            gameSessions.Remove(session);
        }

        public GameSession GetSessionById(string sessionId)
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

        private GameSession GetSessionIdBySms(string smsNumber)
        {
            var sessionId = AllPlayers().Single(p => p.SmsNumber == smsNumber).SessionId;
            return GetSessionById(sessionId);
        }

        //outcomes
        //true : playerId
        //false : errorMessage
        public (bool, string) Join(string smsNumber, string sessionId)
        {
            string message = null;

            if (string.IsNullOrEmpty(sessionId)) throw new ArgumentNullException(nameof(sessionId));
            if (smsNumber == null) throw new ArgumentNullException(nameof(smsNumber));

            var session = gameSessions.SingleOrDefault(s => s.Id == sessionId);

            if (session == null)
            {
                message = $"session does not exist. {sessionId}";
                return (false, message);
            }

            var players = AllPlayers();

            if (players.Any(p => p.SmsNumber.Contains(smsNumber)))
            {
                message = $"you are already registered for a game, to join a new game send command LEAVE";
                return (false, message);
            }

            var player = new Player(smsNumber, session.Id, IdGenerator.GetNext(players.Select(p => p.Id).ToList()));

            session.Join(player);
            message = $"you have joined the game {sessionId}, text LEAVE to quit at any time";
            return (true, message);
        }

        public (bool, string) Leave(string smsNumber)
        {
            string message = null;

            if (string.IsNullOrEmpty(smsNumber)) throw new ArgumentNullException(nameof(smsNumber));

            var players = AllPlayers();
            var player = players.SingleOrDefault(p => p.SmsNumber == smsNumber);

            if (player == null)
            {
                return (false, message);
            }

            var session = gameSessions.Single(p => p.Id == player.SessionId);

            //possible race condition exception?
            //todo: add SessionExists?

            session.Leave(player);
            message = "you have left the game, and I have forgotten everything about you";
            return (true, message);
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
                var joinResult = Join(smsNumber, parsedResult.SessionToJoin);
                response.Success = joinResult.Item1;
                response.Body = joinResult.Item2;
            }
            else if (parsedResult.UserCommand == UserCommandType.Leave)
            {
                var leaveResult = Leave(smsNumber);
                response.Success = leaveResult.Item1;
                response.Body = leaveResult.Item2;
            }
            else if (parsedResult.UserCommand == UserCommandType.Response)
            {
                var session = GetSessionIdBySms(smsNumber);
                var sessionResponse = session.AddResponse(smsNumber, parsedResult.FormattedString);
                //var sessionResponse = session.Response
                //determine if we want to send a message back to the user
            }
            return response;
        }

        public string GetPlayerRngIdBySms(string smsNumber)
        {
            if (smsNumber == null) throw new ArgumentNullException(nameof(smsNumber));
            return AllPlayers().Single(p=>p.SmsNumber == smsNumber).Id;
        }
    }
}