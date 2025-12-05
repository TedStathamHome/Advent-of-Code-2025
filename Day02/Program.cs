using System;
using System.IO;
using System.Linq;
using System.Text;

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
                var invalidRangeValues = FindInvalidValuesInRangeForSize(rangeValues, 2);

                foreach (var invalidId in invalidRangeValues)
                {
                    totalOfInvalidIDs += invalidId;
                }
            }

            Console.WriteLine($"*** Total of invalid IDs: {totalOfInvalidIDs}");
		}

        private static List<ulong> FindInvalidValuesInRangeForSize(string[] RangeValues, int RepeatLength)
        {
            List<ulong> invalidRangeValues = [];

            var rangeStartSize = RangeValues[0].Length;
            var rangeEndSize = RangeValues[1].Length;
            var minRange = ulong.Parse(RangeValues[0]);
            var maxRange = ulong.Parse(RangeValues[1]);
            var rangeStartAndEndSizesAreSame = rangeStartSize == rangeEndSize;
            var rangeStartFitsRepeatSize = rangeStartSize % RepeatLength == 0;
            var rangeEndFitsRepeatSize = rangeEndSize % RepeatLength == 0;

            if (!(rangeStartFitsRepeatSize || rangeEndFitsRepeatSize))
            {
                // we can't check for repeated values on values which have can't be split with no remainder
                // for example, if the start of the range is 7 digits long and we're looking for a repeat length of 3,
                // then it doesn't split such that we can repeat 3 digits multiple times and not exceed the range value length
                Console.WriteLine("**** Neither the range start or end sizes are a length which supports the repeat length, so skipping it.");
                return invalidRangeValues;
            }

            ulong rangeStart;
            ulong rangeEnd;

            if (rangeStartAndEndSizesAreSame)
            {
                // both start and end are the same size
                // take the first half of each to determine what values we have to iterate through
                // for example, for a range of 8800-9913, we want to pull 88 and 99 to iterate through
                rangeStart = ulong.Parse(RangeValues[0][..(rangeStartSize / RepeatLength)]);
                rangeEnd = ulong.Parse(RangeValues[1][..(rangeStartSize / RepeatLength)]);
                Console.WriteLine($"**** Start/end are same size; we'll iterate from {rangeStart} to {rangeEnd}");
            }
            else if (rangeStartSize % RepeatLength == 0)
            {
                // range start can be split up by the repeat length with no remainder, but range end cannot
                // can only check up to the last possible value to the length of the range start
                // for example, for a repeat length of 2, for range of 8800-10100, can only check from 8800 to 9999
                // from there, we'll pull 88 and 99 to iterate through
                rangeStart = ulong.Parse(RangeValues[0][..(rangeStartSize / RepeatLength)]);
                rangeEnd = ulong.Parse(new string('9', rangeStartSize / RepeatLength));
                Console.WriteLine($"**** Start/end are different sizes, and start has a number of digits that can be split up by the repeat length with no remainder; we'll iterate from {rangeStart} to {rangeEnd}");
            }
            else
            {
                // range end can be split up by the repeat length with no remainder, but range start cannot
                // can only check from the first possible value to the length of the range end
                // for example, for a repeat length of 2, for range of 880-1100, can only check from 1000 to 1100
                // from there, we'll want to pull 10 and 11 to iterate through
                rangeStart = ulong.Parse($"1{new string('0', (rangeEndSize / RepeatLength) - 1)}");
                rangeEnd = ulong.Parse(RangeValues[1][..(rangeEndSize / RepeatLength)]);
                Console.WriteLine($"**** Start/end are different sizes, and end has a number of digits that can be split up by the repeat length with no remainder; we'll iterate from {rangeStart} to {rangeEnd}");
            }

            for (var i = rangeStart; i <= rangeEnd; i++)
            {
                string idValue = "";
                for (var j = 0; j < RepeatLength; j++)
                {
                    idValue += $"{i}";
                }

                var idToCheck = ulong.Parse(idValue);
                

                if (idToCheck >= minRange)
                {
                    if (idToCheck <= maxRange)
                    {
                        Console.WriteLine($"**** Found invalid ID in range: {idToCheck}");
                        invalidRangeValues.Add(idToCheck);
                    }
                    else
                    {
                        // we've gone past the upper bound of the range, so exit early
                        break;
                    }
                }
            }

            return invalidRangeValues;
        }

        private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
    }
}
