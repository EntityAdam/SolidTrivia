using System;

namespace SolidTrivia.Common
{
    public class TagEditModel
    {
        public Guid Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
