using System;
using System.Collections.Generic;
using HangmanRenderer.Renderer;

namespace Hangman.Core.Game
{
    public class HangmanGame
    {
        private GallowsRenderer _renderer;

    public HangmanGame()
    {
        _renderer = new GallowsRenderer();
    }

    public void RenderGallows(int x, int y, int wrongAttempts)
    {
        
        for (int line = 0; line < 7; line++)
        {
            Console.SetCursorPosition(x, y + line);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        
        _renderer.Render(x, y, wrongAttempts);
    }

        
        public int PrintWord(List<char> guessedLetters, string randomWord)
        {
            Console.SetCursorPosition(0, 8);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, 8);

            int rightLetters = 0;
            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    rightLetters++;
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            return rightLetters;
        }

        
        public void PrintLines(string randomWord)
        {
            Console.SetCursorPosition(0, 9); 
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 9);
            Console.WriteLine(new string(' ', randomWord.Length *2));
      }
      
    }
}