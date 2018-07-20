using System;
using System.Diagnostics;
using System.Net;

namespace Searching
{

    class Program
    {
        static string[] words;

        static void Main(string[] args)
        {

            Console.WriteLine("Downloading Please Wait");
            words = GetWords();
            Console.WriteLine("Done.");

            Start();
        }

        private static void Start()
        {
            bool isFound = false;
            Console.WriteLine("Would you like a Linear search or a Binary search?");
            string answer = Console.ReadLine().ToLower();

            switch (answer)
            {
                case "linear":
                    isFound = Linear(isFound);
                    break;
                case "binary":
                    isFound = Binary(isFound);
                    break;
                default:
                    Console.WriteLine($"Sorry {answer} is not an option.");
                    Start();
                    break;
            }
        }

        private static bool Binary(bool isFound)
        {
            string searchEntry = Program.GetInput();
            isFound = BinarySearch(words, isFound, searchEntry);
            EndOrContinue();
            return isFound;
        }

        private static bool BinarySearch(string[] words, bool isFound, string searchEntry)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int first = 0;
            int last = words.Length - 1;
            int position = -1;
            bool found = false;
            int compCount = 0;


            while (found != true && first <= last)
            {
                int middle = (first + last) / 2;

                if (string.Compare(words[middle], searchEntry, true) == 0)
                {
                    found = true;
                    position = middle;
                    compCount++;
                    Console.WriteLine($"Your search {searchEntry} has been found after {compCount} comparisons.");
                }
                else if (string.Compare(words[middle], searchEntry, true) > 0)
                {
                    last = middle;
                    compCount++;
                }
                else
                {
                    first = middle;
                    compCount++;
                }
                if (compCount > 1000)
                {
                    Console.WriteLine($"Sorry but the {searchEntry} couldn't be found");
                    break;
                }
            }

            stopwatch.Stop();
            Console.WriteLine($" Search took {stopwatch.ElapsedTicks} ticks");
            return isFound;
        }


        private static bool Linear(bool isFound)
        {
            string searchEntry = GetInput();
            isFound = LinearSearch(words, isFound, searchEntry);
            EndOrContinue();
            return isFound;
        }

        private static void EndOrContinue()
        {
            Console.WriteLine("Would you like to search again?:  Yes or No?");
            string confirmation = Console.ReadLine().ToLower();
            if (confirmation == "yes" || confirmation == "y")
            {
                Start();
            }
            else
            { }
        }

        private static bool LinearSearch(string[] words, bool isFound, string searchEntry)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var item in words)
            {
                if (searchEntry == item)
                {
                    Console.WriteLine($"Found {searchEntry}");
                    isFound = true;
                    break;
                }
            }
            if (isFound == false)
            {
                Console.WriteLine($"Couldn't find {searchEntry}");
            }
            stopwatch.Stop();
            Console.WriteLine($" Search took {stopwatch.ElapsedTicks} ticks");
            return isFound;
        }

        public static string GetInput()
        {
            Console.WriteLine("Please enter your search");
            string searchEntry = Console.ReadLine();
            return searchEntry;
        }

        private static string[] GetWords()
        {
            var url = "https://raw.githubusercontent.com/dwyl/english-words/master/words.txt";
            var textFromFile = (new WebClient()).DownloadString(url);
            var words = textFromFile.Split('\n');
            return words;
        }
    }
}
