using SolidTrivia.Game.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Game
{
    public static class IdGenerator
    {
        public static List<string> Get(int players)
        {
            var duplicates = 0;
            var set = new HashSet<string>();
            while (set.Count < players)
            {
                if (!set.Add(Get()))
                {
                    duplicates++;
                    if (duplicates > 100)
                    {
                        throw new Exception("Unable to generate unique id");
                    }
                }
            }
            return new List<string>(set);
        }

        public static string GetNext(List<string> ids)
        {
            var duplicates = 0;
            string result = null;
            var set = new HashSet<string>(ids);
            while (set.Count < ids.Count+1)
            {
                result = Get();
                if (!set.Add(result))
                {
                    duplicates++;
                    if (duplicates > 100)
                    {
                        throw new Exception("Unable to generate unique id");
                    }
                }
            }
            return result;
        }

        private static string Get()
        {
            string result;
            var random = new Random();
            result = IdGeneratorData.Verbs[random.Next(IdGeneratorData.Verbs.Length)];
            result += "-";
            result += IdGeneratorData.Nouns[random.Next(IdGeneratorData.Nouns.Length)];
            return result;
        }
    }
}
