using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyProg
{
    /* 
    Description:

    The Thue-Morse sequence is a binary sequence (of 0s and 1s) that never repeats. It is obtained by starting with 0 and successively calculating the Boolean complement of the sequence so far. It turns out that doing this yields an infinite, non-repeating sequence. This procedure yields 0 then 01, 0110, 01101001, 0110100110010110, and so on.

    Thue-Morse Wikipedia Article[1] for more information.
    Input:

    Nothing.
    Output:

    Output the 0 to 6th order Thue-Morse Sequences.
    Example:

    nth     Sequence
    ===========================================================================
    0       0
    1       01
    2       0110
    3       01101001
    4       0110100110010110
    5       01101001100101101001011001101001
    6       0110100110010110100101100110100110010110011010010110100110010110

    Extra Challenge:

    Be able to output any nth order sequence. Display the Thue-Morse Sequences for 100, 1000, 10000.
    Credit:

    challenge idea from /u/jnazario [2] from our /r/dailyprogrammer_ideas[3] subreddit.
    */
    class Challenge174E
    {
        public static void MainMethod()
        {
            const int iter = 6;

            BitArray series = new BitArray(1);
            PrintBitArray(series);
            for (var i = 0; i < iter; i++)
            {
                var inverted = new BitArray(series).Not();
                series = Append(series, inverted);
                PrintBitArray(series);
            }
        }

        public static BitArray Append(BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }

        public static void PrintBitArray(BitArray bitArray)
        {
            foreach (var bit in bitArray)
            {
                Console.Write((bool)bit ? 1 : 0);
            }
            Console.WriteLine();
        }
    }
}
