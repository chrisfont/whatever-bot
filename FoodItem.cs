using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTest
{
    partial class FoodItem
    {
        public FoodItem()
        {
            Name = String.Empty;
            Price = 0;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public bool IsBreakfast { get; set; }
    }
}
