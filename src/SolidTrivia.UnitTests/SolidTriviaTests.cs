using SolidTrivia.Game;
using System;
using System.Linq;
using Xunit;

namespace SolidTrivia.UnitTests
{
    public class SolidTriviaTests
    {
        private readonly SolidTriviaGame game;

        public SolidTriviaTests()
        {
            game = new SolidTriviaGame();
        }

        [Fact]
        public void CreateSession()
        {
            var session1 = game.CreateNewSession();
            var session2 = game.CreateNewSession();

            Assert.NotNull(session1.Id);
            Assert.NotNull(session2.Id);

            Assert.NotNull(session1.Players);
            Assert.NotNull(session2.Players);

            Assert.Equal(2, game.ActiveSessions());
        }

        [Fact]
        public void EndSession()
        {
            var session1 = game.CreateNewSession();
            var session2 = game.CreateNewSession();

            Assert.Throws<ArgumentNullException>(() => game.EndGameSession(null));
            Assert.Throws<ArgumentNullException>(() => game.EndGameSession(""));
            Assert.Throws<ArgumentOutOfRangeException>(() => game.EndGameSession("NOT A SESSION"));

            game.EndGameSession(session1.Id);
            Assert.Equal(1, game.ActiveSessions());

            game.EndGameSession(session2.Id);
            Assert.Equal(0, game.ActiveSessions());
        }

        [Fact]
        public void JoinSession()
        {
            var session1 = game.CreateNewSession();
            var session2 = game.CreateNewSession();
            Assert.Equal(2, game.ActiveSessions());

            Assert.Throws<ArgumentNullException>(() => game.Join(null, session1.Id));
            Assert.Throws<ArgumentNullException>(() => game.Join("1", null));

            var joinResult = (false, "");

            joinResult = game.Join("1", "INVALID SESSION");
            Assert.False(joinResult.Item1);
            Assert.StartsWith("session does not exist", joinResult.Item2);

            joinResult = game.Join("1", session1.Id);
            Assert.True(joinResult.Item1);

            joinResult = game.Join("1", session1.Id);
            Assert.False(game.Join("1", session1.Id).Item1);
            Assert.StartsWith("you are already registered", joinResult.Item2);
        }

        [Fact]
        public void LeaveSession()
        {
            var session1 = game.CreateNewSession();
            var session2 = game.CreateNewSession();
            var sessionInfo = game.GetSessionsInfo();

            TestHelper.AddPlayers(game, session1, 10);
            TestHelper.AddPlayers(game, session2, 10);

            Assert.Equal(10, session1.Players.Count());
            Assert.Equal(10, session2.Players.Count());

            game.Leave("1");
            game.Leave("2");

            Assert.Equal(8, session1.Players.Count());
            Assert.Equal(18, game.AllPlayers().Count());

            Assert.Equal(8, sessionInfo.First().PlayerCount);
            Assert.Equal(10, sessionInfo.Skip(1).Take(1).First().PlayerCount);

            var leaveResult = (false, "");
            leaveResult = game.Leave("invalid player");
            Assert.False(leaveResult.Item1);
            Assert.Null(leaveResult.Item2);
        }

        [Fact]
        public void EndSession_PlayerCount()
        {
            var session1 = game.CreateNewSession();
            var session2 = game.CreateNewSession();
            var sessionInfo = game.GetSessionsInfo();

            TestHelper.AddPlayers(game, session1, 10);
            TestHelper.AddPlayers(game, session2, 10);

            Assert.Equal(10, sessionInfo.First().PlayerCount);
            Assert.Equal(10, sessionInfo.Skip(1).Take(1).First().PlayerCount);

            Assert.Equal(10, session1.Players.Count());
            Assert.Equal(10, session2.Players.Count());

            game.EndGameSession(session1.Id);

            Assert.Equal(1, game.ActiveSessions());
            Assert.Equal(10, game.AllPlayers().Count());

            var leaveResult = (false, "");
            leaveResult = game.Leave("invalid player");
            Assert.False(leaveResult.Item1);
            Assert.Null(leaveResult.Item2);
        }

        [Fact]
        public void LeaveSessionEndSession_RaceCondition()
        {
            //TestHelper.CreateSessions(game, 100);
            //create players for each session
            //then async player leave and async end session
            //see if race condition happens
        }

        [Fact]
        public void NewSessionState()
        {
            var session = game.CreateNewSession();
            Assert.NotNull(session.Id);
            Assert.NotNull(session.Responses);
            Assert.NotNull(session.Players);
        }
    }
}