using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolidTrivia.Web.Data
{
    public class DummyLobbyService
    {
        private static readonly string[] PlayerNames = new[]
        {
            "cold-apple", "cool-banana", "chilly-grape", "wishful-corn", "forgettable-chipmunk"
        };

        public List<string> Players { get; set; }

        public Task<string> AddPlayer()
        {
            var rng = new Random();
            var player = PlayerNames[rng.Next(PlayerNames.Length)];
            return Task.FromResult(player);
        }
    }
}
