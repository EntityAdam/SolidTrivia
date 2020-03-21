using System;
using System.ComponentModel.DataAnnotations;

namespace SolidTrivia.Common
{
    public class BoardEditModel
    {
        [Required]
        public Guid Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}