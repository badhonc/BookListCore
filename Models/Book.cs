﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BookListM.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
    }
}
