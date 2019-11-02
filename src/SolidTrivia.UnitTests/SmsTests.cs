using SolidTrivia.Game;
using SolidTrivia.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SolidTrivia.UnitTests
{
    public class SmsTests
    {

        private SolidTriviaGame game;
        private GameSession session;

        public SmsTests()
        {
            game = new SolidTriviaGame();
            session = game.CreateNewSession();
        }

        [Fact]
        public void Test()
        {
            Assert.Empty(game.AllPlayers());
            
            SmsResponseMessage response = new SmsResponseMessage();

            //successful join
            response = game.ProcessUserSmsMessage("1", $"join {session.Id}");
            Assert.True(response.Success);
            Assert.True(response.HasMessage);
            Assert.StartsWith("you have joined the game", response.Body);
            Assert.Single(game.AllPlayers());

            //failed join, already playing
            response = game.ProcessUserSmsMessage("1", $"join {session.Id}");
            Assert.False(response.Success);
            Assert.StartsWith("you are already registered for a game", response.Body);
            Assert.Single(game.AllPlayers());

            //successful join
            response = game.ProcessUserSmsMessage("2", $"JOIN {session.Id}");
            Assert.StartsWith("you have joined the game", response.Body);
            Assert.Equal(2, game.AllPlayers().Count());

            //failed leave, user is not playing, no response
            response = game.ProcessUserSmsMessage("3", $"LEAVE");
            Assert.False(response.HasMessage);
            Assert.Equal(2, game.AllPlayers().Count());

            //successful leave
            response = game.ProcessUserSmsMessage("2", $"leave");
            Assert.StartsWith("you have left the game", response.Body);
            Assert.Single(game.AllPlayers());
        }
    }
}
