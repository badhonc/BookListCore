using System;
using System.Collections.Generic;

#nullable disable

namespace BookListM.Models
{
    public partial class Maincategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubcategoryId { get; set; }
    }
}
