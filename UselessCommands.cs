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
    class UselessCommands
    {

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
            _returnValue += "!taco <calories/price> <# of calories/price in $> - Whatever bot will tell you what tacos to eat." + Environment.NewLine;
            _returnValue += "!addquote <quote> - Add a quote to the database." + Environment.NewLine;
            _returnValue += "!insultme - Whatever bot will insult you.";

            return _returnValue;
        }

        public static string MakeTacoOrder(string[] userCommands)
        {
            string returnValue = String.Empty;

            TacoFilter filter = GenerateFilter(userCommands);
            if (filter.CalulateCalories)
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
            else if (filter.CalculatePrice != false)
            {
                returnValue = "Price calculation coming soon";
            }
            else
            {
                returnValue = "An error has occured in making your taco selections. Check !help for more information about this command.";
            }

            return returnValue;
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
            catch (Exception exc)
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

        public static string GetInsult(string[] userCommands)
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

            returnValue = openers[openIndex] + middles[middleIndex] + " and" + closings[closingIndex] + "!";

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

        public static string SelectQuoteFromFile(string[] userCommands)
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

        public static string RollDice(string[] userCommands)
        {
            string returnValue = String.Empty;
            List<int> result = new List<int>();
            int numDice = 0;
            int numFaces = 6;

            try
            {
                if (userCommands.Length == 0)
                { numDice = 1; numFaces = 6; }
                else if (userCommands.Length == 1)
                { numDice = int.Parse(userCommands[0]); numFaces = 6; }
                else
                { numDice = int.Parse(userCommands[0]); numFaces = int.Parse(userCommands[1]); }

                result = GetDiceResult(numDice, numFaces);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.ToString());
                returnValue = "An error has occoured.";
            }

            string diceResult = String.Empty;

            foreach (var item in result) diceResult += item + " ";

            returnValue = "You rolled " + numDice + " " + numFaces + "-sided dice. Your result was: " + diceResult.Trim();

            return returnValue;
        }

        public static List<int> GetDiceResult(int numberOfDice, int numberOfFaces)
        {
            List<int> rollResult = new List<int>();
            int rolls = 0;
            while (rolls < numberOfDice)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                rollResult.Add(rand.Next(numberOfFaces + 1));
                rolls++;
            }

            return rollResult;
        }
    }
}
