using System;
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
			PartB();
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

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
	}
}
