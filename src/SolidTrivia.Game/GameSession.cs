using SolidTrivia.Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidTrivia.Game
{
    public class GameSession : BindableBase
    {
        public GameSession(string id)
        {
            Id = id;
            Rounds = new List<AnswerBoard>()
            {
                new AnswerBoard("Round One", 1),
                new AnswerBoard("Round Two", 2),
                new AnswerBoard("Final Round", 3)
            };
            Responses = new List<Response>();
            Players = new ObservableCollection<Player>();
        }

        public string Id { get; }

        public List<Response> Responses { get; set; }

        public ObservableCollection<Player> Players { get; set; }

        public List<AnswerBoard> Rounds { get; set; }

        public AnswerBoard CurrentBoard
        {
            get
            {
                return Rounds.First(r => !r.IsComplete);
            }
        }

        public int RoundMultiplier
        {
            get
            {
                switch (Rounds.IndexOf(CurrentBoard))
                {
                    case 0:
                        return 100;

                    case 1:
                        return 200;

                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public Player SelectRandonPlayer()
        {
            var rnd = new Random();
            var index = rnd.Next(Players.Count());
            return Players[index];
        }

        public Answer SelectAnswer(string category, int value)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));

            if (!CurrentBoard.Categories.Contains(category)) throw new ArgumentOutOfRangeException(nameof(category));

            if (CurrentBoard.IsAnswerInProgress) throw new InvalidOperationException("An answer has already been selected");

            var answer = CurrentBoard.Answers.FirstOrDefault(p => p.Category == category && p.Value == value);

            if (answer == null) throw new KeyNotFoundException(nameof(category));

            if (answer.IsAnswered || answer.IsAnswering) throw new InvalidOperationException("That answer has already been selected");

            answer.IsAnswering = true;
            return answer;
        }

        internal void Join(Player player)
        {
            Players.Add(player);
            OnPropertyChanged(nameof(Players));
        }

        internal void Leave(Player player)
        {
            Players.Remove(player);
        }

        public void EndQuestion(string sessionId)
        {
            throw new NotImplementedException();
        }

        //public void Response(string smsNumber, string text)
        //{
        //    var player = Players.Single(p => p.SmsNumber == smsNumber); //todo: don't error, ignore answers from unknown players

        //    if (player == null) { return; }

        //    //var session = gameSessions.Single(s => s.Id == player.SessionId); //todo: don't error, ignore answers with no body

        //    if (session == null) { return; }

        //    session.Response(smsNumber, text);
        //}

        public void Scores(string sessionId)
        {
            throw new NotImplementedException();
        }

        //todo: account for possible spelling mistakes?
        private bool IsResponseCorrect(Answer answer, string text)
        {
            return (answer.AcceptableResponses.Contains(text));
        }
    }
}