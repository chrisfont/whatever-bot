using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTest
{
    class UsefulCommands
    {
        public static string GetCommandList(string[] userMessage)
        {
            string _returnValue = String.Empty;
            _returnValue += "My list of commands are:" + Environment.NewLine;
            _returnValue += "!taco <calories/price> <# of calories/price in $> - Whatever bot will tell you what tacos to eat." + Environment.NewLine;
            _returnValue += "!addquote <quote> - Add a quote to the database." + Environment.NewLine;
            _returnValue += "!insultme - Whatever bot will insult you.";

            return _returnValue;
        }
    }

}
