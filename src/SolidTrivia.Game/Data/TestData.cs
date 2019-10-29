using System.Collections.Generic;

namespace SolidTrivia.Game.Data
{
    public static class TestData
    {
        public static List<Answer> Answers()
        {
            return new List<Answer>()
            {
                new Answer("This is the first letter", "letters", 1, new[] { "A" }),
                new Answer("This is the second letter", "letters", 2, new[] { "B" }),
                new Answer("This is the third letter", "letters", 3, new[] { "C" }),
                new Answer("This is the fourth letter", "letters", 4, new[] { "D" }),
                new Answer("This is the fifth letter", "letters", 5, new[] { "E" }),

                new Answer("This is the first number", "numbers", 1, new[] { "1" }),
                new Answer("This is the second number", "numbers", 2, new[] { "2" }),
                new Answer("This is the third number", "numbers", 3, new[] { "3" }),
                new Answer("This is the fourth number", "numbers", 4, new[] { "4" }),
                new Answer("This is the fifth number", "numbers", 5, new[] { "5" }),

                new Answer("This is the first letter", "letters2", 1, new[] { "A" }),
                new Answer("This is the second letter", "letters2", 2, new[] { "B" }),
                new Answer("This is the third letter", "letters2", 3, new[] { "C" }),
                new Answer("This is the fourth letter", "letters2", 4, new[] { "D" }),
                new Answer("This is the fifth letter", "letters2", 5, new[] { "E" }),

                new Answer("This is the first number", "numbers2", 1, new[] { "1" }),
                new Answer("This is the second number", "numbers2", 2, new[] { "2" }),
                new Answer("This is the third number", "numbers2", 3, new[] { "3" }),
                new Answer("This is the fourth number", "numbers2", 4, new[] { "4" }),
                new Answer("This is the fifth number", "numbers2", 5, new[] { "5" }),

                new Answer("This is the first letter", "letters3", 1, new[] { "A" }),
                new Answer("This is the second letter", "letters3", 2, new[] { "B" }),
                new Answer("This is the third letter", "letters3", 3, new[] { "C" }),
                new Answer("This is the fourth letter", "letters3", 4, new[] { "D" }),
                new Answer("This is the fifth letter", "letters3", 5, new[] { "E" }),

                new Answer("This is the first number", "numbers3", 1, new[] { "1" }),
                new Answer("This is the second number", "numbers3", 2, new[] { "2" }),
                new Answer("This is the third number", "numbers3", 3, new[] { "3" }),
                new Answer("This is the fourth number", "numbers3", 4, new[] { "4" }),
                new Answer("This is the fifth number", "numbers3", 5, new[] { "5" }),
            };
        }
    }
}
