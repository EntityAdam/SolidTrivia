﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SolidTrivia.Common
{
    public class QuestionDeleteModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
