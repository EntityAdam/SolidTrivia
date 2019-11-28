using SolidTrivia.Game.Models;
using System.Collections.Generic;

namespace SolidTrivia.Game.Data
{
    public static class TestData
    {
        public static List<Category> Prompts()
        {

            var cat1 = new List<Prompt>()
            {
                new Prompt("This is the first letter", 1, 
@"public async Task<string> CanDoStuff()
{
    Console.WriteLine(""This is a test"");
}", new[] { "A" }),
                new Prompt("This is the second letter", 2, new[] { "B" }),
                new Prompt("This is the third letter", 3, new[] { "C" }),
                new Prompt("This is the fourth letter", 4, new[] { "D" }),
                new Prompt("This is the fifth letter", 5, new[] { "E" }),
            };
            var cat2 = new List<Prompt>()
            {
                new Prompt("This is the first number", 1, new[] { "1" }),
                new Prompt("This is the second number", 2, new[] { "2" }),
                new Prompt("This is the third number", 3, new[] { "3" }),
                new Prompt("This is the fourth number", 4, new[] { "4" }),
                new Prompt("This is the fifth number", 5, new[] { "5" }),
            };
            var cat3 = new List<Prompt>()
            {
                new Prompt("This is the first letter", 1, new[] { "A" }),
                new Prompt("This is the second letter", 2, new[] { "B" }),
                new Prompt("This is the third letter", 3, new[] { "C" }),
                new Prompt("This is the fourth letter", 4, new[] { "D" }),
                new Prompt("This is the fifth letter", 5, new[] { "E" }),
            };
            var cat4 = new List<Prompt>()
            {
                new Prompt("This is the first number", 1, new[] { "1" }),
                new Prompt("This is the second number", 2, new[] { "2" }),
                new Prompt("This is the third number", 3, new[] { "3" }),
                new Prompt("This is the fourth number", 4, new[] { "4" }),
                new Prompt("This is the fifth number", 5, new[] { "5" }),
            };
            var cat5 = new List<Prompt>()
            {
                new Prompt("This is the first number", 1, new[] { "1" }),
                new Prompt("This is the second number", 2, new[] { "2" }),
                new Prompt("This is the third number", 3, new[] { "3" }),
                new Prompt("This is the fourth number", 4, new[] { "4" }),
                new Prompt("This is the fifth number", 5, new[] { "5" }),
            };
            var cat6 = new List<Prompt>()
            {
                new Prompt("This is the first number", 1, new[] { "1" }),
                new Prompt("This is the second number", 2, new[] { "2" }),
                new Prompt("This is the third number", 3, new[] { "3" }),
                new Prompt("This is the fourth number", 4, new[] { "4" }),
                new Prompt("This is the fifth number", 5, new[] { "5" }),
            };

            return new List<Category>()
            {
                new Category("Design Patterns", cat1),
                new Category("SOLID", cat2),
                new Category("Testing", cat3),
                new Category("GIT", cat4),
                new Category("LINQ", cat5),
                new Category("Potporri", cat6)
            };
        }
    }
}