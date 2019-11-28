using SolidTrivia.Game;
using SolidTrivia.Game.Models;
using System;
using System.Linq;
using Xunit;
using static SolidTrivia.Game.Models.Response;

namespace SolidTrivia.UnitTests
{
    public class SessionTests
    {
        private readonly SolidTriviaGame game;
        private readonly GameSession session;

        public SessionTests()
        {
            game = new SolidTriviaGame();
            session = game.CreateNewSession();
            TestHelper.AddPlayers(game, session, 30);
        }

        [Fact]
        public void SelectAnswer()
        {
            IPrompt prompt;

            Assert.Throws<ArgumentNullException>(() => prompt = session.SelectPrompt(null, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => prompt = session.SelectPrompt("LINQ", -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => prompt = session.SelectPrompt("INVALID CATEGORY", 1));

            prompt = session.SelectPrompt("Potporri", 3);
            Assert.Equal(3, prompt.Weight);
            Assert.True(prompt.IsAnswering);

            Assert.True(session.IsPromptInProgress());
            Assert.Throws<InvalidOperationException>(() => prompt = session.SelectPrompt("LINQ", 5));
            prompt.MarkAsAnswered();
            Assert.False(session.IsPromptInProgress());

            prompt = session.SelectPrompt("LINQ", 1);
            Assert.Equal(1, prompt.Weight);
            Assert.True(prompt.IsAnswering);
        }

        [Fact]
        public void CurrentAnswer()
        {
            IPrompt prompt;

            Assert.Null(session.CurrentPrompt());

            prompt = session.SelectPrompt("Potporri", 3);
            Assert.Equal(3, prompt.Weight);
            Assert.True(prompt.IsAnswering);

            var currentAnswer = session.CurrentPrompt();

            Assert.Equal(currentAnswer, prompt);

            session.MarkCurrentPromptAsAnswered();
            Assert.Null(session.CurrentPrompt());
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
            Assert.StartsWith("no prompt to respond to", response.Body);

            var answer = session.SelectPrompt("LINQ", 2);
            Assert.True(session.IsPromptInProgress());

            Assert.False(session.HasPlayerResponded("1", answer));
            response = session.AddResponse("1", "valid response");
            Assert.True(response.Success);
            Assert.StartsWith("your response has been accepted", response.Body);

            Assert.True(session.HasPlayerResponded("1", answer));
            response = session.AddResponse("1", "valid response");
            Assert.False(response.Success);
            Assert.StartsWith("you have already provided a response to this prompt", response.Body);
        }

        [Fact]
        public void Scenario2()
        {
            var answer = session.SelectPrompt("LINQ", 2);

            //correct response
            game.ProcessUserSmsMessage("1", "2");
            var responses = session.Responses.Where(r => r.PromptId == answer.Id);
            Assert.Single(responses);
            Assert.True(responses.First().IsCorrect);
            Assert.Equal("2", responses.First().Text);

            //incorrect response
            game.ProcessUserSmsMessage("2", "wrong answer");
            var responses1 = session.Responses.Where(r => r.PromptId == answer.Id);
            Assert.Equal(2, responses1.Count());
            Assert.False(responses1.Skip(1).Take(1).First().IsCorrect);
        }

        [Fact]
        public void Scores()
        {
            session.SelectPrompt("Design Patterns", 1);
            game.ProcessUserSmsMessage("1", "A");
            game.ProcessUserSmsMessage("2", "A");
            game.ProcessUserSmsMessage("3", "A");
            game.ProcessUserSmsMessage("4", "A");
            game.ProcessUserSmsMessage("5", "A");
            session.MarkCurrentPromptAsAnswered();

            session.SelectPrompt("Design Patterns", 2);
            game.ProcessUserSmsMessage("1", "B");
            game.ProcessUserSmsMessage("2", "B");
            game.ProcessUserSmsMessage("3", "B");
            game.ProcessUserSmsMessage("4", "B");
            game.ProcessUserSmsMessage("5", "WRONG ANSWER");
            session.MarkCurrentPromptAsAnswered();

            session.SelectPrompt("Design Patterns", 3);
            game.ProcessUserSmsMessage("1", "C");
            game.ProcessUserSmsMessage("2", "C");
            game.ProcessUserSmsMessage("3", "C");
            game.ProcessUserSmsMessage("4", "WRONG ANSWER");
            game.ProcessUserSmsMessage("5", "WRONG ANSWER");
            session.MarkCurrentPromptAsAnswered();

            session.SelectPrompt("Design Patterns", 4);
            game.ProcessUserSmsMessage("1", "D");
            game.ProcessUserSmsMessage("2", "D");
            game.ProcessUserSmsMessage("3", "WRONG ANSWER");
            game.ProcessUserSmsMessage("4", "WRONG ANSWER");
            game.ProcessUserSmsMessage("5", "WRONG ANSWER");
            session.MarkCurrentPromptAsAnswered();

            session.SelectPrompt("Design Patterns", 5);
            game.ProcessUserSmsMessage("1", "E");
            game.ProcessUserSmsMessage("2", "WRONG ANSWER");
            game.ProcessUserSmsMessage("3", "WRONG ANSWER");
            game.ProcessUserSmsMessage("4", "WRONG ANSWER");
            game.ProcessUserSmsMessage("5", "WRONG ANSWER");
            session.MarkCurrentPromptAsAnswered();

            var x = session.Leaderboard();
            //todo: finish testing

            //todo: user leaves game, all responses are removed
        }

        [Fact]
        public void Test()
        {
            session.SelectPrompt("Design Patterns", 1);
            game.ProcessUserSmsMessage("1", "A");
            game.ProcessUserSmsMessage("2", "A");
            game.ProcessUserSmsMessage("3", "A");
            game.ProcessUserSmsMessage("4", "A");
            game.ProcessUserSmsMessage("5", "A");

            ////todo: response relies on the CurrentAnswer AKA the one answer that has a flag of IsAnswering = true
            //session.MarkCurrentAnswerAsAnswered();

            string playerName = game.GetPlayerRngIdBySms("1");

            var response = session.Responses.Single(r => r.PromptId == session.CurrentPrompt().Id && r.PlayerId == playerName);
            Assert.Equal(GradeType.NotGraded, response.Grade);


            session.GradeCorrect(playerName);
            Assert.Equal(GradeType.Correct, response.Grade);
        }
    }
}