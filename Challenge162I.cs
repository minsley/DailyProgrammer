using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyProg
{
    class Challenge162I
    {
        /* 
         * Note: does not handle erroneous patterns (eg. "??", "middleCaps", "middl3nums", "4")
         * 
         * See this post for a cleaner regexy answer:
         * http://www.reddit.com/r/dailyprogrammer/comments/25hlo9/5142014_challenge_162_intermediate_novel/chhbtuk
         * 
         */

        public static void MainMethod()
        {
            var input1 =  @"The quick brown fox jumps over the lazy dog.
                            Or, did it?";

            var input2 =  @"I would not, could not, in the rain.
                            Not in the dark. Not on a train.
                            Not in a car. Not in a tree.
                            I do not like them, Sam, you see.
                            Not in a house. Not in a box.
                            Not with a mouse. Not with a fox.
                            I will not eat them here or there.
                            I do not like them anywhere!";

            var dict1 = BuildDictionary(input1);
            var output1 = Compress(input1, dict1);

            var dict2 = BuildDictionary(input2);
            var output2 = Compress(input2, dict2);

            Console.WriteLine(dict1.Length);
            foreach (var definition in dict1)
            {
                Console.WriteLine(definition);
            }
            Console.WriteLine(output1);

            Console.WriteLine(dict2.Length);
            foreach (var definition in dict2)
            {
                Console.WriteLine(definition);
            }
            Console.WriteLine(output2);
        }

        public static string[] BuildDictionary(string input)
        {
            var al = new List<string>();
            var matches = Regex.Matches(input, @"\w+");
            foreach (Match match in matches)
            {
                var newWord = match.ToString().ToLower();
                if(!al.Contains(newWord)) al.Add(newWord);
            }
            return al.ToArray();
        }

        public static string Compress(string input, string[] dictionary)
        {
            var output = new StringBuilder();
            foreach (var line in input.Split('\n'))
            {
                foreach (var word in line.Split(' '))
                {
                    if (Regex.IsMatch(word, @"\w+\p{P}\w+"))
                    {
                        var words = Regex.Matches(word, @"\w+");
                        output.Append(EncodeWord(words[0].ToString(), dictionary));
                        output.Append("- ");
                        output.Append(EncodeWord(words[1].ToString(), dictionary));
                    }
                    else
                    {
                        var wordPart = Regex.Match(word, @"\w+").ToString();
                        if(wordPart != "") output.Append(EncodeWord(wordPart, dictionary));

                        var punctuationPart = Regex.Match(word, @"\p{P}").ToString();
                        if (punctuationPart != "") output.Append(punctuationPart + " ");
                    }
                }
                output.Append("R ");
            }
            output.Append("E");
            return output.ToString();
        }

        public static string EncodeWord(string word, string[] dictionary)
        {
            var output = " ";
            if (char.IsUpper(word[0])) output = "^ ";
            else if (word == word.ToUpper()) output = "! ";
            var index = Array.IndexOf(dictionary, word.ToLower());
            return index + output;
        }
    }
}
