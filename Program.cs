﻿using LRN_CS.Data;
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
        
        static void Main(string[] args)
        {

            try
            {
                bool continueWordUnscramble = true;
                do
                {
                    Console.WriteLine(Constants.OptionsOnHowToEnterScrambledWords);
                    var option = Console.ReadLine() ?? string.Empty;

                    switch (option.ToUpper())
                    {
                        case Constants.File:
                            {
                                Console.Write(Constants.EnterScrambledWordsViaFile);
                                ExecuteScrambledWordsInFileScenario();
                                break;
                            }
                        case Constants.Manual:
                            {
                                Console.Write(Constants.EnterScrambledWordsManually);
                                ExecuteScrambledWordsManualEntryScenario();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine(Constants.EnterScrambledWordsOptionUnrecognized);
                                break;
                            }
                    }
                    var continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(Constants.OptionsOnContinuingTheProgram);
                        continueDecision = (Console.ReadLine() ?? String.Empty);
                    } while (!continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                            !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscramble = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscramble);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTreminated + ex.Message);
            }

            

        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                var fileName = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(fileName);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);
            }

 
        }
        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                foreach(var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);
                }
            } 
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }


    }
}
