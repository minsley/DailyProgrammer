using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DailyProg
{
    class Challenge162H
    {
        public static void MainMethod()
        {
            Console.Write("> ");
            var args = Console.ReadLine().Split(' ');
            if (args[0]=="debug") { Debug(); return; }
            if (args.Length != 3) throw new Exception();

            var command = args[0];
            var fileIn = args[1];
            var fileOut = args[2];
            if(!"-c-d".Contains(command)) throw new Exception();

            string input;
            string[] dict;

            if (command == "-c")
            {
                using (var sr = File.OpenText(fileIn))
                {
                    input = sr.ReadToEnd();
                    dict = BuildDictionary(input);
                }

                var output = Compress(input, dict);

                using (var sw = File.CreateText(fileOut))
                {
                    sw.WriteLine(dict.Length);
                    foreach (var definition in dict)
                    {
                        sw.WriteLine(definition);
                    }
                    sw.WriteLine(output);
                }
            }
            else // command == "-d"
            {
                using (var sr = File.OpenText(fileIn))
                {
                    var n = int.Parse(sr.ReadLine());
                    var dictList = new List<string>();
                    for (var i = 0; i < n; i++)
                    {
                        dictList.Add(sr.ReadLine());
                    }
                    input = sr.ReadToEnd();
                    dict = dictList.ToArray();
                }

                var output = Decompress(input, dict);

                using (var sw = File.CreateText(fileOut))
                {
                    sw.Write(output);
                }
            }
        }

        public static string[] BuildDictionary(string input)
        {
            var al = new List<string>();
            var matches = Regex.Matches(input, @"[A-z]+");
            foreach (Match match in matches)
            {
                var newWord = match.ToString().ToLower();
                if (!al.Contains(newWord)) al.Add(newWord);
            }
            return al.ToArray();
        }

        public static string Compress(string input, string[] dictionary)
        {
            var output = new StringBuilder();
            foreach (var line in input.Split(new string[]{"\r\n", "\n"}, StringSplitOptions.None))
            {
                foreach (var token in line.Split(' '))
                {
                    var tokenType = "";
                    if (token.Any(char.IsDigit)) throw new Exception("Error: digits are not supported: '" + token + "'");
                    else if (Regex.IsMatch(token, @"\s") || token == "") continue;
                    else if (Regex.IsMatch(token, @"[A-z]+(-[A-z]+)+")) tokenType = "hyphen";
                    else if (Regex.IsMatch(token, @"[A-z]+")) tokenType = "word";
                    else throw new Exception("Error: unrecognized token format: '" + token + "'");

                    switch (tokenType)
                    {
                        case "word":
                            var word = Regex.Match(token, @"[A-z]+").ToString();
                            if (word != "") output.Append(EncodeWord(word, dictionary));
                            goto case "punctuation";
                        case "hyphen":
                            var words = Regex.Matches(token, @"[A-z]+");
                            output.Append(EncodeWord(words[0].ToString(), dictionary));
                            for (var i = 1; i < words.Count; i++) output.Append("- " + EncodeWord(words[i].ToString(), dictionary));
                            goto case "punctuation";
                        case "punctuation":
                            var punctuation = Regex.Match(token, @"\p{P}", RegexOptions.RightToLeft).ToString();
                            if (punctuation != "")
                            {
                                if (!".,?!;:".Contains(punctuation)) throw new Exception("Error: unsupported symbol: '" + punctuation + "'");
                                output.Append(punctuation + " ");
                            }
                            break;
                    }
                }
                output.Append("R ");
            }
            output.Append("E");
            return output.ToString();
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

        public static string EncodeWord(string word, string[] dictionary)
        {
            var output = " ";
            if (char.IsUpper(word[0])) output = "^ ";
            else if (word == word.ToUpper()) output = "! ";
            else if (word != word.ToLower()) throw new Exception("Error: unsupported capitalization: '" + word + "'");
            var index = Array.IndexOf(dictionary, word.ToLower());
            return index + output;
        }

        public static string Capitalize(string word)
        {
            var result = word.ToLower().ToCharArray();
            result[0] = char.ToUpper(result[0]);
            return string.Concat(result);
        }

        public static void Debug()
        {
            string input;
            string[] dict;
            const string fileIn             = @"..\..\Challenge162H\SamIAm.txt";
            const string fileCompressed     = @"..\..\Challenge162H\compressed.txt";
            const string fileDecompressed   = @"..\..\Challenge162H\decompressed.txt";
            
            // Generate compressed file
            using (var sr = File.OpenText(fileIn))
            {
                input = sr.ReadToEnd();
                dict = BuildDictionary(input);
            }

            var compressed = Compress(input, dict);

            using (var sw = File.CreateText(fileCompressed))
            {
                sw.WriteLine(dict.Length);
                foreach (var definition in dict)
                {
                    sw.WriteLine(definition);
                }
                sw.WriteLine(compressed);
            }

            // Writeout decompressed file
            using (var sr = File.OpenText(fileCompressed))
            {
                var n = int.Parse(sr.ReadLine());
                var dictList = new List<string>();
                for (var i = 0; i < n; i++)
                {
                    dictList.Add(sr.ReadLine());
                }
                input = sr.ReadToEnd();
                dict = dictList.ToArray();
            }

            var decompressed = Decompress(input, dict);

            using (var sw = File.CreateText(fileDecompressed))
            {
                sw.Write(decompressed);
            }
        }
    }
}
