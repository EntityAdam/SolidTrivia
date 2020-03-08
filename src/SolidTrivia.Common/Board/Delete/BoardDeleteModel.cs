﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SolidTrivia.Common
{
    public class BoardDeleteModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}