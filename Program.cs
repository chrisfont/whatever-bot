using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DiscordTest
{
    class Program
    {
        const string COMMAND = "!";

        static void Main(string[] args)
        {
            DiscordClient _client = new DiscordClient();

            _client.MessageReceived += async (s, e) =>
            {
                if (e.Message.RawText.ToLower().StartsWith(COMMAND))
                {
                    if (e.Message.RawText.ToLower().Contains("insultme"))
                        await e.Channel.SendMessage("You are a sack of crap!");
                    if (e.Message.RawText.ToLower().Contains("help"))
                        await e.Channel.SendMessage(GetCommandList());
                    if (e.Message.RawText.Contains("taco") || e.Message.RawText.Contains("tacos"))
                    {
                        string tacoOrder = MakeTacoOrder(e.Message.RawText.ToLower());
                        await e.Channel.SendMessage(tacoOrder);
                    }
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

        public static string GetCommandList()
        {
            string _returnValue = String.Empty;
            _returnValue += "My list of commands are:" + Environment.NewLine;
            _returnValue += "Ask me for tacos and I'll you what tacos to buy." + Environment.NewLine;
            _returnValue += "Ask me to insult you and I will." + Environment.NewLine;

            return _returnValue;
        }

        public static string MakeTacoOrder(string userMessage)
        {
            string returnValue = String.Empty;
            
            userMessage = ChopCommandString(userMessage);
            TacoFilter filter = GenerateFilter(userMessage);
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

        public static TacoFilter GenerateFilter(string userMessage)
        {

            TacoFilter filter = new TacoFilter();
            string[] userCommands = userMessage.Split(' ');
            try
            {
                    if (userCommands[1] == "calories")
                    {
                        filter.CalulateCalories = true;
                        filter.Calories = int.Parse(userCommands[2]);
                    }
                    else
                    {
                        filter.CalculatePrice = true;
                        filter.Price = decimal.Parse(userCommands[2]);
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
    }
}
