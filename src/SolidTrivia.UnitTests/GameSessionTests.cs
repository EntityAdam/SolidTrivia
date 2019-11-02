using SolidTrivia.Game;
using SolidTrivia.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SolidTrivia.UnitTests
{
    public class SessionTests
    {
        private SolidTriviaGame game;
        private GameSession session;

        public SessionTests()
        {
            game = new SolidTriviaGame();
            session = game.CreateNewSession();
            TestHelper.AddPlayers(game, session, 30);
        }

        [Fact]
        public void SelectAnswer()
        {
            Answer answer;

            Assert.Throws<ArgumentNullException>(() => answer = session.SelectAnswer(null, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => answer = session.SelectAnswer("LINQ", -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => answer = session.SelectAnswer("INVALID CATEGORY", 1));

            answer = session.SelectAnswer("Potporri", 3);
            Assert.Equal(3, answer.Weight);
            Assert.True(answer.IsAnswering);

            Assert.True(session.IsAnswerInProgress());
            Assert.Throws<InvalidOperationException>(() => answer = session.SelectAnswer("LINQ", 5));
            answer.MarkAsAnswered();
            Assert.False(session.IsAnswerInProgress());

            answer = session.SelectAnswer("LINQ", 1);
            Assert.Equal(1, answer.Weight);
            Assert.True(answer.IsAnswering);
        }

        [Fact]
        public void CurrentAnswer()
        {
            Answer answer;
            
            Assert.Null(session.CurrentAnswer());
            
            answer = session.SelectAnswer("Potporri", 3);
            Assert.Equal(3, answer.Weight);
            Assert.True(answer.IsAnswering);
           
            var currentAnswer = session.CurrentAnswer();

            Assert.Equal(currentAnswer, answer);

            session.MarkCurrentAnswerAsAnswered();
            Assert.Null(session.CurrentAnswer());
        }

        [Fact]
        public void Scenario1()
        {
            var firstChoicePlayer = session.SelectRandonPlayer();
            Assert.NotNull(firstChoicePlayer);

            SmsResponseMessage response;

            //player is invalid / not playing
            response = session.AddResponse("INVALID PLAYER", "INVALID RESPONSE");
            Assert.False(response.Success);


            //no answer is being answered
            response = session.AddResponse("1", "INVALID RESPONSE");
            Assert.False(response.Success);
            Assert.StartsWith("no answer to respond to", response.Body);

            var answer = session.SelectAnswer("LINQ", 2);
            Assert.True(session.IsAnswerInProgress());

            Assert.False(session.HasPlayerResponded("1", answer));
            response = session.AddResponse("1", "valid response");
            Assert.True(response.Success);
            Assert.StartsWith("your response has been accepted", response.Body);

            Assert.True(session.HasPlayerResponded("1", answer));
            response = session.AddResponse("1", "valid response");
            Assert.False(response.Success);
            Assert.StartsWith("you have already provided a response to this answer", response.Body);

        }
    }
}