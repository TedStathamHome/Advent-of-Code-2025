using System;
using System.IO;
using System.Linq;

namespace Day02
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2025: Day 2");
			var puzzleInputRaw = File.ReadLines($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt").ToList();

            PartA(puzzleInputRaw[0]);
			PartB();
		}

		private static void PartA(string rawRangesToCheck)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            string[] rangesToCheck = rawRangesToCheck.Split(',');
            Console.WriteLine($"** Ranges to check: {rangesToCheck.Count():N0}");

            foreach (var range in rangesToCheck)
            {
                Console.WriteLine($"*** Examining range of {range}...");

                var rangeValues = range.Split("-");
                var rangeStartSize = rangeValues[0].Length;
                var rangeEndSize = rangeValues[1].Length;
                var rangeStartAndEndSizesAreSame = rangeStartSize == rangeEndSize;
                var rangeStartIsEvenSize = rangeStartSize % 2 == 0;
                var rangeEndIsEvenSize = rangeEndSize % 2 == 0;

                if (!(rangeStartIsEvenSize || rangeEndIsEvenSize))
                {
                    Console.WriteLine("**** Neither the range start or end sizes are an even length, so skipping it.");
                    continue;
                }

                ulong rangeStart;
                ulong rangeEnd;

                if (rangeStartAndEndSizesAreSame)
                {
                    rangeStart = ulong.Parse(rangeValues[0].Substring(0, rangeStartSize / 2));
                    rangeEnd = ulong.Parse(rangeValues[1].Substring(0, rangeStartSize / 2));
                }
                else if (rangeStartSize % 2 == 0)       // range start is even number of digits, but range end is odd
                {

                }
                else                                    // range end is even number of digits, but range start is odd
                {

                }
            }
		}

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
    }
}
