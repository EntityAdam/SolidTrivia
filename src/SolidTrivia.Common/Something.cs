using System;
using System.Collections.Generic;
using System.Text;

namespace SolidTrivia.Common
{
    class UserBoard
    {
        readonly string UserId;
        readonly List<ABoard> Boards;
    }

    class ABoard
    {
        readonly string Name;
        readonly List<ACategory> Categories;
    }

    class ACategory
    {
        readonly string Name;
        readonly IEnumerable<string> QuestionIds;
    }
}
