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

                    if (commandDict.ContainsKey(cmd))
                    {
                        string message = commandDict[cmd](userCommands.Skip(1).ToArray());
                        await e.Channel.SendMessage(message);
                    }

                    //if (e.Message.RawText.ToLower().Contains("insultme"))
                    //    await e.Channel.SendMessage(GetInsult());
                    //if (e.Message.RawText.ToLower().Contains("help"))
                    //    await e.Channel.SendMessage(GetCommandList(userCommands));
                    //if (e.Message.RawText.Contains("taco") || e.Message.RawText.Contains("tacos"))
                    //{
                    //    string tacoOrder = MakeTacoOrder(e.Message.RawText.ToLower());
                    //    await e.Channel.SendMessage(tacoOrder);
                    //}
                    //if (e.Message.RawText.ToLower().Contains("addquote"))
                    //{
                    //    string greatsuccesss = AddQuote(e.Message.RawText);
                    //    await e.Channel.SendMessage(greatsuccesss);
                    //}
                    //if (e.Message.RawText.ToLower().Contains("sayquote"))
                    //{
                    //    await e.Channel.SendMessage(SelectQuoteFromFile());
                    //}
                }
            };

            _client.ExecuteAndWait(async () => {
            { await _client.Connect("", TokenType.Bot); Console.WriteLine("Connected."); }
            });

            
        }

        public static List<Taco> PopulateTacoList()
        {
            List<Taco> _tacoList = new List<Taco>();
            Taco _taco = new Taco();
            _taco.Name = "Crunchy Taco";
            _taco.Price = 1.19M;
            _taco.Calories = 170;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Crunchy Taco Supreme";
            _taco.Price = 1.59M;
            _taco.Calories = 190;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Doritos Locos Taco Supreme";
            _taco.Price = 1.89M;
            _taco.Calories = 190;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Doritos Locos Taco";
            _taco.Price = 1.49M;
            _taco.Calories = 170;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Soft Taco";
            _taco.Price = 1.49M;
            _taco.Calories = 180;
            _taco.IsCrunchy = false;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Soft Taco Supreme";
            _taco.Price = 1.59M;
            _taco.Calories = 210;
            _taco.IsCrunchy = false;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Grilled Steak Soft Taco";
            _taco.Price = 2.49M;
            _taco.Calories = 200;
            _taco.IsCrunchy = false;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);


            _taco = new Taco();
            _taco.Name = "Double Decker Taco";
            _taco.Price = 1.89M;
            _taco.Calories = 320;
            _taco.IsCrunchy = false;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Double Decker Taco Supreme";
            _taco.Price = 2.19M;
            _taco.Calories = 340;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Cheesy Gordita Crunch";
            _taco.Price = 2.89M;
            _taco.Calories = 500;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            _taco = new Taco();
            _taco.Name = "Cheesy Gordita Crunch Supreme";
            _taco.Price = 3.19M;
            _taco.Calories = 500;
            _taco.IsCrunchy = true;
            _taco.IsBreakfast = false;
            _tacoList.Add(_taco);

            return _tacoList;
        }

        public static string GetCommandList(string[] userMessage)
        {
            string _returnValue = String.Empty;
            _returnValue += "My list of commands are:" + Environment.NewLine;
            _returnValue += "Ask me for tacos and I'll you what tacos to buy." + Environment.NewLine;
            _returnValue += "Ask me to insult you and I will." + Environment.NewLine;

            return _returnValue;
        }

        public static string MakeTacoOrder(string[] userCommands)
        {
            string returnValue = String.Empty;
            
            TacoFilter filter = GenerateFilter(userCommands);
            if(filter.CalulateCalories)
            {
                List<Taco> _listTaco = PopulateTacoList();
                int minCals = _listTaco.Min(x => x.Calories);
                decimal minPrice = _listTaco.Min(x => x.Price);


                if (filter.Calories > minCals)
                {
                    int currentCalories = filter.Calories;
                    List<Taco> filteredList = FilterTacoList(_listTaco, currentCalories);


                    while (currentCalories > minCals && filteredList.Count > 0)
                    {
                        filteredList = FilterTacoList(_listTaco, currentCalories);
                        Random rand = new Random(Guid.NewGuid().GetHashCode());
                        int tacoIndex = rand.Next(filteredList.Count);
                        returnValue += filteredList[tacoIndex].Name + Environment.NewLine + "Calories: " + filteredList[tacoIndex].Calories + Environment.NewLine;
                        currentCalories -= filteredList[tacoIndex].Calories;
                    }
                }
                else
                    returnValue = "There are no tacos that low in calories to eat at Taco Bell.";

            }
            else if(filter.CalculatePrice != false)
            {
                returnValue = "Price calculation coming soon";
            }
            else
            {
                returnValue = "An error has occured in making your taco selections. Check !help for more information about this command.";
            }

            return returnValue;
        }

        public static string ChopCommandString(string userMessage)
        {
            return userMessage.Substring(COMMAND.Length);
        }

        public static TacoFilter GenerateFilter(string[] userCommands)
        {

            TacoFilter filter = new TacoFilter();
            try
            {
                if (userCommands[0] == "calories")
                {
                   filter.CalulateCalories = true;
                   filter.Calories = int.Parse(userCommands[1]);
                }
                else if (userCommands[0] == "price")
                {
                   filter.CalculatePrice = true;
                   filter.Price = decimal.Parse(userCommands[1]);
                }
                else
                {
                    filter.CalculatePrice = false;
                    filter.CalulateCalories = false;
                }

            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.ToString());
                filter.CalculatePrice = false;
                filter.CalulateCalories = false;
            }

            return filter;
        }

        public static List<Taco> FilterTacoList(List<Taco> originalList, int currentCals)
        {
            List<Taco> filteredList = originalList;
            filteredList = originalList.Where(x => x.Calories <= currentCals).ToList();
            return filteredList;
        }

        public static string GetInsult()
        {
            string returnValue = String.Empty;
            string opener = String.Empty;
            string openerFollow = String.Empty;
            string middle = String.Empty;
            string middleFollow = String.Empty;
            string closing = String.Empty;

            List<string> openers = new List<string> { "You", "Your Mother", "Your Dad", "Your Dog", "Your Kid", "Your Sister" };
            List<string> middles = new List<string> { " smells like feet ", " likes Nickleback ", " eats dirt ", " stepped in gum " };
            List<string> closings = new List<string> { " can't read", " smells thier own farts", " likes shitty memes", " is dumb" };

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int openIndex = rand.Next(openers.Count);
            int middleIndex = rand.Next(middles.Count);
            int closingIndex = rand.Next(closings.Count);

            returnValue = openers[openIndex] + middles[middleIndex] + " and" + closings[closingIndex] +"!";

            return returnValue;
        }

        public static string AddQuote(string[] userCommands)
        {
            string returnValue = "Not yet active.";
            string quote = String.Empty;
            foreach (var item in userCommands)
                quote += item + " ";
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            Directory.CreateDirectory(filePath + @"\quotes");
            filePath += @"\quotes\quotes.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(quote.Trim());
                }
                returnValue = "Quote written successfully.";
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.ToString());
                returnValue = "An error has occoured";
            }
            return returnValue;
        }

        public static string SelectQuoteFromFile()
        {
            string returnValue = "Not yet active.";
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\quotes\quotes.txt";
            try
            {
                List<string> quotes = new List<string>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    quotes = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                if (quotes.Count > 0)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int quoteIndex = rand.Next(quotes.Count);
                    returnValue = quotes.ElementAt(quoteIndex);
                }
                else
                    returnValue = "No quotes have been added.";
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.ToString());
                returnValue = "An error has occoured";
            }
            return returnValue;
        }

        private static void PopulateCommandDict()
        {
            commandDict.Add("help", GetCommandList);
            commandDict.Add("taco", MakeTacoOrder);
            commandDict.Add("addquote", AddQuote);
        }

    }
}
