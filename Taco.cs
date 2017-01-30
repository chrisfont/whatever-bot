using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTest
{
    class Taco : FoodItem
    {
        public Taco()
        {
            IsCrunchy = false;
        }

        public bool IsCrunchy { get; set; }
    }

}
