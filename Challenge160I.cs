using System;
using System.Linq;
using System.Collections.Generic;

namespace DailyProg
{
    class Challenge160I
    {
        public static void MainMethod()
        {
            // Get parameters
            Console.Write("Enter N P >> ");
            var input1 = Console.ReadLine().Split(' ');
            int n; int.TryParse(input1[0], out n);
            int p; int.TryParse(input1[1], out p);

            Console.Write("Enter B >> ");
            var b = Array.ConvertAll(Console.ReadLine().Split(' '), s => int.Parse(s));

            // Find all permutations of b
            var sequences = new List<List<int>>();
            permutation(sequences, new List<int>(), b.ToList());


            // Try each termite infestation vector, looking for least kits needed
            var leastKits = 0;
            var bestSequence = new List<int>();
            foreach(var sequence in sequences)
            {
                Console.WriteLine("\n*** Infestation! ***\n");

                // Calculate kits needed for each wave of infestations
                var kitsNeeded = 0;
                var buildings = new string('s', n).ToCharArray();
                foreach (var target in sequence)
                {
                    kitsNeeded += CalculateKitsNeeded(ref buildings, target - 1);
                }
                Console.WriteLine("\nKits needed for sequence [{0}]\": {1}", String.Join(", ",sequence), kitsNeeded);

                if (kitsNeeded < leastKits || leastKits == 0)
                {
                    leastKits = kitsNeeded;
                    bestSequence = sequence;
                }
            }
            Console.WriteLine("\n\nBEST SEQUENCE: [{0}]\nKITS NEEDED: {1}", String.Join(", ", bestSequence), leastKits);
        }

        /// <summary>
        /// Calculates the number of kits needed to protect all houses in this infestation wave.
        /// </summary>
        /// <remarks>
        /// buildings[] legend:
        /// s: house is safe
        /// t: house is infested
        /// d: house is destroyed
        /// k: house is protected
        /// </remarks>
        public static int CalculateKitsNeeded(ref char[] buildings, int target)
        {
            buildings[target] = 't';
            Console.WriteLine(buildings);

            for (var i = target - 1; i >= 0; i--)
            {
                if (buildings[i + 1] == 't' || buildings[i + 1] == 'k' && buildings[i] != 'd') buildings[i] = 'k';
            }
            for (var i = target + 1; i < buildings.Length; i++)
            {
                if (buildings[i - 1] == 't' || buildings[i - 1] == 'k' && buildings[i] != 'd') buildings[i] = 'k';
            }
            var kits = buildings.Count(x => x.Equals('k'));
            Console.WriteLine(buildings);

            for (var i = 0; i < buildings.Count(); i++)
            {
                if (buildings[i] == 't') buildings[i] = 'd';
                if (buildings[i] == 'k') buildings[i] = 's';
            }
            Console.WriteLine(buildings);
            Console.WriteLine("\nKits used this wave: {0}\n", kits);

            return kits;
        }

        /// <summary>
        /// Finds all permutations for a List of int
        /// </summary>
        /// <param name="result">List of permutations</param>
        /// <param name="prefix">Static prefixed elements</param>
        /// <param name="remaining">The initial list of elements</param>
        private static void permutation(List<List<int>> result, List<int> prefix, List<int> remaining)
        {
            int n = remaining.Count;
            if (n == 0) result.Add(prefix);
            else
            {
                for (int i = 0; i < n; i++)
                {
                    var newPrefix = new List<int>(prefix);
                    newPrefix.Add(remaining[i]);
                    var newRemaining = new List<int>(remaining);
                    newRemaining.Remove(remaining[i]);
                    permutation(result, newPrefix, newRemaining);
                }
            }
        }
    }
}
