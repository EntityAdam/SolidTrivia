using EntityAdam.IdGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityAdam.IdGenerator
{
    public class IdGeneratorService : IIdGeneratorService
    {
        private Random Random = new Random();

        private List<string> Ids { get; set; }

        public IdGeneratorService()
        {
            Ids = GetAll();
        }

        public string GetNext()
        {
            var next = Next();

            var id = Ids[next];

            Ids.RemoveAt(next);

            return id;
        }

        private int Next() => Random.Next(0, Ids.Count());

        private static List<string> GetAll()
        {
            return IdGeneratorData.Verbs
                .SelectMany(verbs =>
                    IdGeneratorData.Nouns.Select(noun => $"{verbs}-{noun}")).ToList();
        }
    }
}