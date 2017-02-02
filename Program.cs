using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using System.IO;
using System.Reflection;

namespace DiscordTest
{
    class Program
    {
        const string COMMAND = "!";
        public delegate string CommandFuction(string[] userCommands);
        public static Dictionary<string, CommandFuction> commandDict = new Dictionary<string, CommandFuction>();
       

        static void Main(string[] args)
        {
            DiscordClient _client = new DiscordClient();
            PopulateCommandDict();

            _client.MessageReceived += async (s, e) =>
            {
                if (e.Message.RawText.ToLower().StartsWith(COMMAND))
                {

                    string[] userCommands = ChopCommandString(e.Message.RawText).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    string cmd = userCommands[0];
                    cmd = cmd.ToLower();

                    if(cmd == "goaway")
                    {
                        await _client.Disconnect();
                    }
                        

                    if (commandDict.ContainsKey(cmd))
                    {
                        string message = commandDict[cmd](userCommands.Skip(1).ToArray());
                        await e.Channel.SendMessage(message);
                    }
                }
            };

            _client.ExecuteAndWait(async () => {
            { await _client.Connect("", TokenType.Bot); Console.WriteLine("Connected."); }
            });

            
        }

        public static string ChopCommandString(string userMessage)
        {
            return userMessage.Substring(COMMAND.Length);
        }


        private static void PopulateCommandDict()
        {
            commandDict.Add("help", UsefulCommands.GetCommandList);
            commandDict.Add("taco", UselessCommands.MakeTacoOrder);
            commandDict.Add("addquote", UselessCommands.AddQuote);
            commandDict.Add("insultme", UselessCommands.GetInsult);
            commandDict.Add("quote", UselessCommands.SelectQuoteFromFile);
            commandDict.Add("roll", UselessCommands.RollDice);
        }



    }
}
