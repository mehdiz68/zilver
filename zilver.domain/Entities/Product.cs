using System;
using System.Collections.Generic;
using System.Text;

namespace zilver.domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
