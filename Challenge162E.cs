using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DailyProg
{
    class Challenge162E
    {
        public static void MainMethod()
        {
            var dict = new[] {"i", "do", "house", "with", "mouse", "in", "not", "like", "them", "ham", "a", "anywhere", "green", "eggs", "and", "here", "or", "there", "sam", "am"};
            var input = "0^ 1 6 7 8 5 10 2 . R 0^ 1 6 7 8 3 10 4 . R 0^ 1 6 7 8 15 16 17 . R 0^ 1 6 7 8 11 . R 0^ 1 6 7 12 13 14 9 . R 0^ 1 6 7 8 , 18^ - 0^ - 19 . R E";
        
            Console.WriteLine(Decompress(input, dict));
        }

        public static string Decompress(string input, string[] dictionary)
        {
            var result = new StringBuilder();
            var chunks = input.Split(' ');

            foreach (var chunk in chunks)
            {
                int index;
                var indexFound = int.TryParse(Regex.Match(chunk, @"\d+").ToString(), out index);

                if (indexFound)
                {
                    var word = dictionary[index].ToLower();
                    if (chunk.EndsWith("^")) word = Capitalize(word);
                    else if (chunk.EndsWith("!")) word = word.ToUpper();
                    result.Append(word + " ");
                }
                else switch (chunk.ToLower())
                {
                    case "r":
                        result.Append("\n");
                        break;
                    case "e":
                        return result.ToString();
                    case "-":
                        result.Remove(result.Length - 1, 1);
                        result.Append(chunk);
                        break;
                    default:
                        result.Remove(result.Length - 1, 1);
                        result.Append(chunk + " ");
                        break;
                }
            }
            return result.ToString();
        }

        public static string Capitalize(string word)
        {
            var result = word.ToLower().ToCharArray();
            result[0] = char.ToUpper(result[0]);
            return string.Concat(result);
        }
    }
}
