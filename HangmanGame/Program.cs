using System;
using System.Collections.Generic;
using Hangman.Core.Game;

namespace HangmanGameConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear(); // Clear the console for fresh start

            var hangman = new HangmanGame();

            // Header position
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("Welcome to Hangman  :)");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("-----------------------------------");

            Random random = new Random();
            List<string> wordDictionary = new List<string> { "Planet", "Mystery", "Crimson", "Voyage", "Galaxy", "Jungle", "Knight", "Whisper", "Echoes", "Puzzle", "Shadow", "Quartz", "Mirage", "Phantom", "Canyon", "Falcon", "Icicle", "Zephyr", "Ember", "Breeze", };
            int index = random.Next(wordDictionary.Count);
            string randomWord = wordDictionary[index];

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            // Initial render
            hangman.RenderGallows(0, 0, amountOfTimesWrong);
            currentLettersRight = hangman.PrintWord(currentLettersGuessed, randomWord);
            hangman.PrintLines(randomWord);

            while (amountOfTimesWrong < 6 && currentLettersRight < lengthOfWordToGuess)
            {
                // Guessed letters display (line 10)
                Console.SetCursorPosition(0, 10);
                Console.Write("Letters guessed: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.Write(letter + " ");
                }
                Console.Write(new string(' ', Console.WindowWidth - (17 + currentLettersGuessed.Count * 2))); // Clear remaining space

                // Input prompt (line 12)
                Console.SetCursorPosition(0, 12);
                Console.Write("Guess a letter: ");
                Console.Write(new string(' ', Console.WindowWidth - 16)); // Clear line
                Console.SetCursorPosition(16, 12); // After "Guess a letter: "
                char letterGuessed = Console.ReadLine().ToUpper()[0];

                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    // Error message (line 14)
                    Console.SetCursorPosition(0, 14);
                    Console.Write("You already used that letter!");
                    Console.ReadKey();
                    Console.SetCursorPosition(0, 14);
                    Console.Write(new string(' ', Console.WindowWidth)); // Clear error message
                }
                else
                {
                    currentLettersGuessed.Add(letterGuessed);
                    bool correctGuess = randomWord.ToUpper().Contains(char.ToUpper(letterGuessed));

                    if (correctGuess)
                    {
                        currentLettersRight = hangman.PrintWord(currentLettersGuessed, randomWord);
                        hangman.PrintLines(randomWord);
                    }
                    else
                    {
                        amountOfTimesWrong++;
                        hangman.RenderGallows(0, 0, amountOfTimesWrong);
                    }
                }
            }

            // Game end message
            Console.SetCursorPosition(0, 14);
            Console.ForegroundColor = oldColor;
            Console.WriteLine(amountOfTimesWrong == 6 
                ? "\r\nGame over! The word was: " + randomWord 
                : "\r\nCongratulations! You won!");
            Console.WriteLine("Thank you for playing!");
        }
    }
}
