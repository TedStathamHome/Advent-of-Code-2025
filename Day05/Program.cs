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

            IngredientRanges = [.. IngredientRanges.OrderBy(r => r.Start)];

            Console.WriteLine($"** Initial number of ingredient ranges is {IngredientRanges.Count:N0}");

            int i = 0;
            while (i < IngredientRanges.Count - 1)
            {
                if (IngredientRanges[i].End < IngredientRanges[i + 1].Start)
                {
                    // current range falls before the start of the next range, proceed to checking the next range
                    i++;
                    continue;
                }

                if (IngredientRanges[i].Start <= IngredientRanges[i + 1].Start && IngredientRanges[i].End >= IngredientRanges[i + 1].End)
                {
                    // current range completely encloses the next range, so remove the next range
                    IngredientRanges.RemoveAt(i + 1);
                    continue;
                }

                if (IngredientRanges[i].Start <= IngredientRanges[i + 1].Start && IngredientRanges[i].End >= IngredientRanges[i + 1].Start && IngredientRanges[i].End <= IngredientRanges[i + 1].End)
                {
                    // current range overlaps the start of the next range, so set the end of the current range to the end of the next range, then remove the next range
                    IngredientRanges[i].End = IngredientRanges[i + 1].End;
                    IngredientRanges.RemoveAt(i + 1);
                    continue;
                }

                if (IngredientRanges[i].Start >= IngredientRanges[i + 1].Start && IngredientRanges[i].Start <= IngredientRanges[i + 1].End && IngredientRanges[i].End >= IngredientRanges[i + 1].End)
                {
                    // current range overlaps the end of the next range, so set the start of the current range to the start of the next range, then remove the next range
                    IngredientRanges[i].Start = IngredientRanges[i + 1].Start;
                    IngredientRanges.RemoveAt(i + 1);
                    continue;
                }
            }

            ulong freshIngredientCount = 0;
            Console.WriteLine($"** Final list of ingredient ranges, which has {IngredientRanges.Count:N0} entries");

            foreach (var ingredientRange in IngredientRanges)
            {
                Console.WriteLine($"*** {ingredientRange.Start:N0} -> {ingredientRange.End:N0}");
                freshIngredientCount += (ingredientRange.End - ingredientRange.Start + 1);
            }

            Console.WriteLine($"** Count of fresh ingredients for all ranges: {freshIngredientCount:N0}");
        }
	}
}
