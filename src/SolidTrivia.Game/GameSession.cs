using SolidTrivia.Game.Data;
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
            Responses = new ObservableCollection<Response>();
            Players = new ObservableCollection<Player>();

            //TODO:
            this.Categories = TestData.Answers();
        }

        public string Id { get; }

        public ObservableCollection<Response> Responses { get; set; }

        public ObservableCollection<Player> Players { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public Player SelectRandonPlayer()
        {
            var rnd = new Random();
            var index = rnd.Next(Players.Count());
            return Players[index];
        }
        private IEnumerable<Answer> Answers() => Categories.SelectMany(c => c.Answers);

        public bool IsAnswerInProgress() => Answers().Any(a => a.IsAnswering);

        public Answer CurrentAnswer() => Answers().SingleOrDefault(a => a.IsAnswering == true);

        public void MarkCurrentAnswerAsAnswered() => CurrentAnswer().MarkAsAnswered();

        public void SelectAnswer(Answer answer)
        {
            if (answer == null) throw new ArgumentNullException(nameof(answer));
            if (IsAnswerInProgress()) throw new InvalidOperationException("An answer is in progress");
            if (answer.IsAnswered || answer.IsAnswering) throw new InvalidOperationException("That answer has already been selected");
            answer.IsAnswering = true;
        }

        public Answer SelectAnswer(string category, int weight)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));
            if (weight <= 0) throw new ArgumentOutOfRangeException(nameof(weight));

            if (!Categories.Any(c => c.Title == category)) throw new ArgumentOutOfRangeException(nameof(category));

            if (IsAnswerInProgress()) throw new InvalidOperationException("An answer is in progress");

            var cat = Categories.Single(c => c.Title == category);
            var answer = cat.Answers.FirstOrDefault(p => p.Weight == weight);

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
        public SmsResponseMessage AddResponse(string smsNumber, string text)
        {
            var player = Players.SingleOrDefault(p => p.SmsNumber == smsNumber);
            if (player == null)
            {
                return new SmsResponseMessage()
                {
                    Success = false
                };
            }

            var currentAnswer = CurrentAnswer();
            if (currentAnswer == null)
            {
                return new SmsResponseMessage()
                {
                    Success = false,
                    Body = "no answer to respond to"
                };
            }

            var hasPlayerResponded = HasPlayerResponded(smsNumber, currentAnswer);
            if (hasPlayerResponded)
            {
                return new SmsResponseMessage()
                {
                    Success = false,
                    Body = "you have already provided a response to this answer"
                };
            }

            var isCorrect = IsResponseCorrect(CurrentAnswer(), text);

            var response = new Response(player.Id, CurrentAnswer().Id, CurrentAnswer().Weight, text, isCorrect, DateTime.Now);
            Responses.Add(response);

            return new SmsResponseMessage()
            {
                Success = true,
                Body = "your response has been accepted"
            };

        }

        public bool HasPlayerResponded(string smsNumber, Answer answer)
        {
            var player = Players.SingleOrDefault(p => p.SmsNumber == smsNumber);
            return Responses.Any(r => r.PlayerId == player.Id && r.AnswerId == answer.Id);
        }

        //todo: account for possible spelling mistakes?
        private bool IsResponseCorrect(Answer answer, string text)
        {
            //todo: fix
            //case insensitive
            return (answer.AcceptableResponses.Contains(text));
        }

        public IEnumerable<Score> Leaderboard()
        {
            return Players.Select(GetScoreForPlayer).OrderBy(s=>s.TotalScore);
        }

        private Score GetScoreForPlayer(Player player)
        {
            return new Score()
            {
                PlayerId = player.Id,
                TotalScore = Responses.Where(r=>r.PlayerId == player.Id).Sum(r=>r.Score)
            };
        }
    }

    public class Score
    {
        public string PlayerId { get; set; }
        public int TotalScore { get; set; }

    }
}