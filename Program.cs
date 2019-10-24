using LRN_CS.Data;
using LRN_CS.Worker;
using LRN_CS.Workers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LRN_CS
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private const string wordListFileName = "wordlist.txt";
        static void Main(string[] args)
        {
            bool continueWordUnscramble = true;
            do
            {
                Console.WriteLine("Please enter the option - F for File and M for Manual");
                var option = Console.ReadLine() ?? string.Empty;

                switch (option.ToUpper())
                {
                    case "F":
                        {
                            Console.Write("Enter scrambled words file name: ");
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        }
                    case "M":
                        {
                            Console.Write("Enter scrambled words manually");
                            ExecuteScrambledWordsManualEntryScenario();
                            break;
                        }
                    default:
                        {
                            Console.Write("Option was not recognized.");
                            break;
                        }
                }
                var continueDecision = string.Empty;
                do
                {
                    Console.WriteLine("Do you want to continue? Y/N");
                    continueDecision = (Console.ReadLine() ?? String.Empty);
                } while (!continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase) && 
                        !continueDecision.Equals("N", StringComparison.OrdinalIgnoreCase));

                continueWordUnscramble = continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase);

            } while (continueWordUnscramble);

        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var fileName = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(fileName);
        }
        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                foreach(var matchedWord in matchedWords)
                {
                    Console.Write("Match found for {0}: {1}", matchedWord.ScrambledWord, matchedWord.Word);
                }
            } 
            else
            {
                Console.WriteLine("No matches have been found.");
            }
        }


    }
}
