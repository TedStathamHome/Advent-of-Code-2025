using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
	internal class Program
	{
        internal class IngredientRange
        {
            public ulong Start { get; set; }
            public ulong End { get; set; }
        }

        static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2025: Day 5");
			var puzzleInputRaw = File.ReadLines($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt").ToList();

            List<IngredientRange> ingredientRanges = [.. puzzleInputRaw
                .Where(i => i.Contains('-'))
                .Select(i => i.Split('-')).ToList()
                .Select(a => new IngredientRange { Start = ulong.Parse(a[0]), End = ulong.Parse(a[1]) })];

            List<ulong> ingredients = [.. puzzleInputRaw
                .Where(i => !i.Contains('-') && i.Length > 0)
                .Select(i => ulong.Parse(i))];

			PartA(ingredientRanges, ingredients);
			PartB(ingredientRanges);
		}

		private static void PartA(List<IngredientRange> IngredientRanges, List<ulong> Ingredients)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            int freshIngredients = 0;

            foreach (var ingredient in Ingredients)
            {
                if (IngredientRanges.Any(r => ingredient >= r.Start && ingredient <= r.End))
                {
                    Console.WriteLine($"*** Ingredient {ingredient} is fresh");
                    freshIngredients++;
                }
            }

            Console.WriteLine($"** There are {freshIngredients} fresh ingredients.");
		}

		private static void PartB(List<IngredientRange> IngredientRanges)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");

            // combine overlapping ingredient ranges until there are none with overlaps

            List<IngredientRange> newRanges = [.. IngredientRanges];
            List<IngredientRange> rangesToRemove = [];
            int rangeListChangesMade = 0;

            foreach (var range in IngredientRanges)
            {
                // find ranges that exactly match this range in their size
                var exactMatches = newRanges
                    .Where(r => range.Start == r.Start && range.End == r.End)
                    .Select((newRange, index) => new {NewRange = newRange, Index = index})
                    .ToList();

                // if there are more than 1, remove everything but the first one
                // generally we should only get a single match, but who knows?
                if (exactMatches.Count > 1)
                {
                    for (int i = 1; i < exactMatches.Count; i++)
                    {
                        newRanges.RemoveAt(exactMatches[i].Index);
                        rangeListChangesMade++;
                    }
                }

                // find matches where the start is within another range,
                // but is not exactly the same as the found range
                var rangeStartInOtherRanges = newRanges
                    .Where(r => range.Start >= r.Start && range.Start <= r.End
                        && !(range.Start == r.Start && range.End == r.End))
                    .Select((newRange, index) => new { NewRange = newRange, Index = index })
                    .ToList();

                if (rangeStartInOtherRanges.Count > 0)
                {
                    foreach (var foundRange in rangeStartInOtherRanges)
                    {
                        // if the end of the range is past the end of the found range,
                        // then the found range encapsulates the range being inspected

                        if (range.End > foundRange.NewRange.End)
                        {
                            // extend the found range out to the end of the range being
                            // inspected to merge them
                            foundRange.NewRange.End = range.End;
                            newRanges[foundRange.Index] = foundRange.NewRange;
                        }

                        // add the range being inspected to the list of ranges to be removed
                        rangesToRemove.Add(range);      // check if it exists already
                    }
                }

                // if the range being checked encloses other entries in the new ranges,
                // remove the other entries

                // if the range being checked overlaps other entries in the new ranges,
                // where the checked range's start precedes another range and the checked
                // range's end is within that same range, merge them together

                // if the range being checked overlaps other entries in the new ranges,
                // where the checked range's end follows another range and the checked
                // range's start is within that same range, merge them together


                // if the range being checked doesn't overlap any other ranges yet,
                // add it to the list of new ranges
                //if (newRanges.Any(r => range.Start < r.Start && range.End))
            }
        }
	}
}
