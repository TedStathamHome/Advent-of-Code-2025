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

            ulong totalOfInvalidIDs = 0;

            foreach (var range in rangesToCheck)
            {
                Console.WriteLine($"*** Examining range of {range}...");

                var rangeValues = range.Split("-");
                var rangeStartSize = rangeValues[0].Length;
                var rangeEndSize = rangeValues[1].Length;
                var minRange = ulong.Parse(rangeValues[0]);
                var maxRange = ulong.Parse(rangeValues[1]);
                var rangeStartAndEndSizesAreSame = rangeStartSize == rangeEndSize;
                var rangeStartIsEvenSize = rangeStartSize % 2 == 0;
                var rangeEndIsEvenSize = rangeEndSize % 2 == 0;

                if (!(rangeStartIsEvenSize || rangeEndIsEvenSize))
                {
                    // we can't check for duplicate values on values which have an odd number of digits
                    Console.WriteLine("**** Neither the range start or end sizes are an even length, so skipping it.");
                    continue;
                }

                ulong rangeStart;
                ulong rangeEnd;

                if (rangeStartAndEndSizesAreSame)
                {
                    // both start and end are the same size
                    // take the first half of each to determine what values we have to iterate through
                    // for example, for a range of 8800-9913, we want to pull 88 and 99 to iterate through
                    rangeStart = ulong.Parse(rangeValues[0].Substring(0, rangeStartSize / 2));
                    rangeEnd = ulong.Parse(rangeValues[1].Substring(0, rangeStartSize / 2));
                    Console.WriteLine($"**** Start/end are same size; we'll iterate from {rangeStart} to {rangeEnd}");
                }
                else if (rangeStartSize % 2 == 0)
                {
                    // range start is even number of digits, but range end is odd
                    // can only check up to the last possible value to the length of the range start
                    // for example, for range of 8800-10100, can only check from 8800 to 9999
                    // from there, we'll pull 88 and 99 to iterate through
                    rangeStart = ulong.Parse(rangeValues[0][..(rangeStartSize / 2)]);
                    rangeEnd = ulong.Parse(new string('9', rangeStartSize / 2));
                    Console.WriteLine($"**** Start/end are different sizes, and start has an even number of digits; we'll iterate from {rangeStart} to {rangeEnd}");
                }
                else
                {
                    // range end is even number of digits, but range start is odd
                    // can only check from the first possible value to the length of the range end
                    // for example, for range of 880-1100, can only check from 1000 to 1100
                    // from there, we'll want to pull 10 and 11 to iterate through
                    rangeStart = ulong.Parse($"1{new string('0', (rangeEndSize / 2) - 1)}");
                    rangeEnd = ulong.Parse(rangeValues[1][..(rangeEndSize / 2)]);
                    Console.WriteLine($"**** Start/end are different sizes, and end has an even number of digits; we'll iterate from {rangeStart} to {rangeEnd}");
                }

                for (var i = rangeStart; i <= rangeEnd; i++)
                {
                    var idToCheck = ulong.Parse($"{i}{i}");

                    if (idToCheck >= minRange)
                    {
                        if (idToCheck <= maxRange)
                        {
                            Console.WriteLine($"**** Found invalid ID in range: {idToCheck}");
                            totalOfInvalidIDs += idToCheck;
                        }
                        else
                        {
                            // we've gone past the upper bound of the range, so exit early
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"*** Total of invalid IDs: {totalOfInvalidIDs}");
		}

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
    }
}
