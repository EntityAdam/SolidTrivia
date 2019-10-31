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
        public void CurrentRound()
        {
            var round1 = session.Rounds.First();
            var round2 = session.Rounds.Skip(1).First();
            var round3 = session.Rounds.Skip(2).First();
            Assert.Equal("Round One", round1.Title);
            Assert.Equal("Round Two", round2.Title);
            Assert.Equal("Final Round", round3.Title);

            Assert.Equal(session.CurrentBoard, round1);

            round1.IsComplete = true;
            Assert.Equal(session.CurrentBoard, round2);

            round2.IsComplete = true;
            Assert.Equal(session.CurrentBoard, round3);

            round3.IsComplete = true;
            //what now?
        }

        [Fact]
        public void SelectAnswer_Happy()
        {
            Answer answer;

            answer = session.SelectAnswer("letters", 3);
            Assert.Equal("letters", answer.Category);
            Assert.Equal(3, answer.Value);
            Assert.True(answer.IsAnswering);

            Assert.Throws<InvalidOperationException>(() => answer = session.SelectAnswer("numbers", 1));

            answer.MarkAsAnswered();

            answer = session.SelectAnswer("numbers", 1);
            Assert.Equal("numbers", answer.Category);
            Assert.Equal(1, answer.Value);
            Assert.True(answer.IsAnswering);
        }

        [Fact]
        public void SelectAnswer_Sad()
        {
            Answer answer;

            answer = session.SelectAnswer("letters", 4);
            Assert.Equal("letters", answer.Category);
            Assert.Equal(4, answer.Value);

            Assert.Throws<InvalidOperationException>(() => answer = session.SelectAnswer("letters", 4));
        }

        [Fact]
        public void SelectAnswer_Sad2()
        {
            Answer answer;

            Assert.Throws<ArgumentNullException>(() => answer = session.SelectAnswer("", 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => answer = session.SelectAnswer("invalid category", 0));
            Assert.Throws<KeyNotFoundException>(() => answer = session.SelectAnswer("letters", 6));
        }

        [Fact]
        public void Scenario1()
        {
            //var game = new SolidTriviaGame();
            //var session = game.CreateNewGameSession();

            ////temp;
            //var scores = new List<Response>();

            //game.OpenRegistration(session.Id);
            //game.Join("1", session.Id);
            //game.Join("2", session.Id);
            //game.Join("3", session.Id);
            //game.CloseRegistration(session.Id);

            //var firstChoicePlayer = session.SelectRandonPlayer();
            //Assert.NotNull(firstChoicePlayer);

            //var answer = session.SelectAnswer("fundamentals", 1);

            //game.Response("1", "blue"); //first answer correct
            //game.Response("1", "yellow"); //second answer discarded
            //game.Response("2", "yellow"); //first answer incorrect
            ////session.EndQuestion();
            //game.Response("3", "blue"); //late answer discarded

            //scores = game.Scores(session.Id);
            //Assert.Equal(1, scores.Where(r=>r.IsCorrect).Count());
            //Assert.Equal(1, scores.Where(r => r.IsCorrect).Count());

            //var answer2 = game.NextAnswer(session.Id);

            //Assert.NotEqual(answer1, answer2);

            //game.Response("1", "blue");
            //game.Response("1", "yellow");
            //game.Response("2", "yellow");
            //game.CloseAnswer(session.Id);
            //game.Response("3", "blue");

            ////todo: validate leaderboard
            ////var scores2 = game.Scores(session.Id);

            //var answer3 = game.NextAnswer(session.Id); //todo: error: no answer yet, must begin next round
        }
    }
}