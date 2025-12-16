using System;
using System.IO;
using System.Linq;

namespace Day04
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2025: Day 4");
			var puzzleInputRaw = File.ReadLines($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt").ToList();

            // add a border of periods around the input to make it easier when working against the borders
            puzzleInputRaw = [.. puzzleInputRaw.Select(r => $".{r}.")];
            puzzleInputRaw.Insert(0, new string('.', puzzleInputRaw[0].Length));
            puzzleInputRaw.Add(new string('.', puzzleInputRaw[0].Length));

            foreach (var line in puzzleInputRaw)
            {
                Console.WriteLine(line);
            }

            PartA(puzzleInputRaw);
			PartB();
		}

		private static void PartA(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            const char roll = '@';
            const int maxAdjacentRolls = 3;

            int rowLength = puzzleInputRaw[0].Length;
            int accessibleRolls = 0;

            for (int r = 1; r < (puzzleInputRaw.Count - 1); r++) 
            {
                for (int c = 1; c < (rowLength - 1); c++)
                {
                    // if we're not looking at a roll, skip the space
                    if (puzzleInputRaw[r][c] != roll)
                        continue;

                    int adjacentRolls =
                        (puzzleInputRaw[r][c - 1] == roll ? 1 : 0) +        // to west
                        (puzzleInputRaw[r][c + 1] == roll ? 1 : 0) +        // to east
                        (puzzleInputRaw[r - 1][c] == roll ? 1 : 0) +        // to north
                        (puzzleInputRaw[r + 1][c] == roll ? 1 : 0) +        // to south
                        (puzzleInputRaw[r - 1][c - 1] == roll ? 1 : 0) +    // to north west
                        (puzzleInputRaw[r - 1][c + 1] == roll ? 1 : 0) +    // to north east
                        (puzzleInputRaw[r + 1][c - 1] == roll ? 1 : 0) +    // to south west
                        (puzzleInputRaw[r + 1][c + 1] == roll ? 1 : 0);     // to south east

                    accessibleRolls += (adjacentRolls <= maxAdjacentRolls) ? 1 : 0;
                }
            }

            Console.WriteLine($"** The number of accessible paper rolls is: {accessibleRolls:N0}");
        }

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
	}
}
