using System;
using System.IO;
using System.Linq;

namespace DailyProg
{
    class Challenge163I
    {
        public static void MainMethod()
        {
            const int guesses = 4;
            const string dictionaryLocation = @"..\..\Utilities\enable1.txt";

            Console.Write("Enter difficulty level (1-5): ");
            var diff = int.Parse(Console.ReadLine()) - 1;

            var game = new Game(diff, dictionaryLocation);
            foreach (var word in game.WordList)
            {
                Console.WriteLine(word);
            }

            for (var i = 0; i < guesses; i++)
            {
                Console.Write("Guess ({0} left): ", guesses - i);
                var guess = Console.ReadLine();

                var numCorrect = game.EvaluateGuess(guess);

                Console.WriteLine("{0} / {1} correct", numCorrect, game.WordLength);

                if (numCorrect == game.WordLength)
                {
                    Console.WriteLine("You Win!");
                    break;
                }
            }
        }

        class Game
        {
            public readonly int WordLength;
            public readonly string[] WordList;

            private readonly string _answer;

            public Game(int difficulty, string dictionaryLocation)
            {
                var randomizer = new Random();
                int numWords;
                switch (difficulty)
                {
                    case 0:
                        WordLength = 4;
                        numWords = 5;
                        break;
                    case 1:
                        WordLength = 7;
                        numWords = 8;
                        break;
                    case 2:
                        WordLength = 10;
                        numWords = 10;
                        break;
                    case 3:
                        WordLength = 12;
                        numWords = 12;
                        break;
                    default:
                        WordLength = 15;
                        numWords = 15;
                        break;
                }

                WordList = new string[numWords];
                using (var tReader = File.OpenText(dictionaryLocation))
                {
                    var words = tReader.ReadToEnd().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    var validWords = words.Where(x => x.Length == WordLength).ToList();

                    for (var i = 0; i < numWords; i++)
                    {
                        var index = randomizer.Next(0, validWords.Count);
                        WordList[i] = validWords[index].ToUpper();
                        validWords.RemoveAt(index);
                    }
                }

                _answer = WordList[randomizer.Next(0, WordList.Length)];
            }

            public int EvaluateGuess(string guess)
            {
                var correctLetters = 0;
                for (var i = 0; i < WordLength; i++)
                {
                    if (char.ToUpper(guess[i]) == _answer[i]) correctLetters++;
                }
                return correctLetters;
            }
        }
    }
}
