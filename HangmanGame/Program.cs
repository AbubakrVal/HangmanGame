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
            Console.Clear(); 

            var hangman = new HangmanGame();

            
            Console.SetCursorPosition(10, 1);
            Console.WriteLine("Welcome to Hangman  :)");
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("-----------------------------------");

            Random random = new Random();
            List<string> wordDictionary = new List<string> { "Planet", "Mystery", "Crimson", "Voyage", "Galaxy", "Jungle", "Knight", "Whisper", "Echoes", "Puzzle", "Shadow", "Quartz", "Mirage", "Phantom", "Canyon", "Falcon", "Icicle", "Zephyr", "Ember", "Breeze", };
            int index = random.Next(wordDictionary.Count);
            string randomWord = wordDictionary[index];

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 6;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;



            char[] playerGuessWord = new char[randomWord.Length];
            Array.Fill(playerGuessWord, '_');

/*            string playerGuessWord = string.Empty;

            for (int index1 = 0; index1 < randomWord.Length; index1++)
            {
                playerGuessWord += "_";
            }
*/


           hangman.RenderGallows(0, 0, amountOfTimesWrong);
            currentLettersRight = hangman.PrintWord(currentLettersGuessed, randomWord);
            hangman.PrintLines(randomWord);

            while (amountOfTimesWrong > 0 && currentLettersRight < lengthOfWordToGuess)
            {
                
                Console.SetCursorPosition(0, 10);
                Console.Write("Letters guessed: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.Write(letter + " ");
                }
                Console.Write(new string(' ', Console.WindowWidth - (17 + currentLettersGuessed.Count * 2)));

                // Input prompt (line 12)
                Console.SetCursorPosition(0, 12);
                Console.Write("Guess a letter: ");
                Console.Write(new string(' ', Console.WindowWidth - 16)); 
                Console.SetCursorPosition(16, 12); 
                char letterGuessed = Console.ReadLine().ToUpper()[0];

                if (randomWord.Contains(letterGuessed))
                {
                    for (int index2 = 0; index2 < randomWord.Length; index2++)
                    {
                        if (randomWord[index2] == letterGuessed)
                        {
                            playerGuessWord[index2] = letterGuessed; 
                        }
                    }
                }

                if (currentLettersGuessed.Contains(letterGuessed))
                    {

                        Console.SetCursorPosition(0, 14);
                        Console.Write("You already used that letter!");
                        Console.ReadKey();
                        Console.SetCursorPosition(0, 14);
                        Console.Write(new string(' ', Console.WindowWidth));
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
                            amountOfTimesWrong--;
                            hangman.RenderGallows(0, 0, amountOfTimesWrong);
                        }
                    }
            }

           
            Console.SetCursorPosition(0, 10);
            Console.ForegroundColor = oldColor;
            Console.WriteLine("\r\nThe word was: " + randomWord);
            Console.WriteLine(currentLettersRight == lengthOfWordToGuess 
                ? "\r\nCongratulations! You won!" 
                : "\r\nGame over! Better luck next time!");

            Console.WriteLine("Thank you for playing!");
        }
    }
}
