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
            this.Categories = TestData.Prompts();
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

        private IEnumerable<IPrompt> Prompts() => Categories.SelectMany(c => c.Prompts);

        public bool IsPromptInProgress() => Prompts().Any(a => a.IsAnswering);

        public IPrompt CurrentPrompt() => Prompts().SingleOrDefault(a => a.IsAnswering == true);

        public void MarkCurrentPromptAsAnswered() => CurrentPrompt().MarkAsAnswered();

        public void SelectPrompt(IPrompt prompt)
        {
            if (prompt == null) throw new ArgumentNullException(nameof(prompt));
            if (IsPromptInProgress()) throw new InvalidOperationException("A prompt is in progress");
            if (prompt.IsAnswered || prompt.IsAnswering) throw new InvalidOperationException("That prompt has already been selected");
            prompt.IsAnswering = true;
        }

        public IPrompt SelectPrompt(string category, int weight)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));
            if (weight <= 0) throw new ArgumentOutOfRangeException(nameof(weight));

            if (!Categories.Any(c => c.Title == category)) throw new ArgumentOutOfRangeException(nameof(category));

            if (IsPromptInProgress()) throw new InvalidOperationException("A prompt is in progress");

            var cat = Categories.Single(c => c.Title == category);
            var prompt = cat.Prompts.FirstOrDefault(p => p.Weight == weight);

            if (prompt == null) throw new KeyNotFoundException(nameof(category));

            if (prompt.IsAnswered || prompt.IsAnswering) throw new InvalidOperationException("That prompt has already been selected");

            prompt.MarkAsCurrentPrompt();
            return prompt;
        }

        private Response GetResponseByUsername(string rngUsername)
        {
            var prompt = CurrentPrompt();
            return Responses.Single(r => r.PromptId == prompt.Id && r.PlayerId == rngUsername);
        }

        public void GradeCorrect(string rngUsername) => GetResponseByUsername(rngUsername).GradeCorrect();

        public void GradeIncorrect(string rngUsername) => GetResponseByUsername(rngUsername).GradeCorrect();

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

            var currentPrompt = CurrentPrompt();
            if (currentPrompt == null)
            {
                return new SmsResponseMessage()
                {
                    Success = false,
                    Body = "no prompt to respond to"
                };
            }

            var hasPlayerResponded = HasPlayerResponded(smsNumber, currentPrompt);
            if (hasPlayerResponded)
            {
                return new SmsResponseMessage()
                {
                    Success = false,
                    Body = "you have already provided a response to this prompt"
                };
            }

            var isCorrect = IsResponseCorrect(CurrentPrompt(), text);

            var response = new Response(player.Id, CurrentPrompt().Id, CurrentPrompt().Weight, text, isCorrect, DateTime.Now);
            Responses.Add(response);

            return new SmsResponseMessage()
            {
                Success = true,
                Body = "your response has been accepted"
            };
        }

        public bool HasPlayerResponded(string smsNumber, IPrompt prompt)
        {
            var player = Players.SingleOrDefault(p => p.SmsNumber == smsNumber);
            return Responses.Any(r => r.PlayerId == player.Id && r.PromptId == prompt.Id);
        }

        //todo: account for possible spelling mistakes?
        private static bool IsResponseCorrect(IPrompt prompt, string text)
        {
            var acceptableResponses = prompt.AcceptableResponses.Select(a => a.ToLower());
            return (acceptableResponses.Contains(text.ToLower()));
        }

        public IEnumerable<Score> Leaderboard()
        {
            return Players.Select(GetScoreForPlayer).OrderBy(s => s.TotalScore);
        }

        private Score GetScoreForPlayer(Player player)
        {
            return new Score()
            {
                PlayerId = player.Id,
                TotalScore = Responses.Where(r => r.PlayerId == player.Id).Sum(r => r.Score)
            };
        }
    }
}