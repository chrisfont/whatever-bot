using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTest
{
    class TacoFilter
    {
        public TacoFilter()
        {
            CalculatePrice = false;
            CalulateCalories = false;
        }

        public bool CalculatePrice { get; set; }
        public bool CalulateCalories { get; set; }
        public int Calories { get; set; }
        public decimal Price { get; set; }
    }
}
