using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SolidTrivia.Common
{

    //TODO: for the view models, or should I just rely on the Data Annotations RegexAttribute?
    public static class InputValidator
    {
        private const string TagPattern = @"^[A-Za-z0-9-#\+]+$";
        private static Regex Tag => new Regex(TagPattern, RegexOptions.Compiled);
        public static bool IsValidTagName(string tagName) => Tag.IsMatch(tagName);

        private const string CategoryPattern = @"/^[\w\s-&]+$/";
        private static Regex Category => new Regex(CategoryPattern, RegexOptions.Compiled);
        public static bool IsValidCategory(string categoryName) => Category.IsMatch(categoryName);
    }
}
