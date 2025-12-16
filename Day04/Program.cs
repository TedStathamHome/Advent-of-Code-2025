using System;
using System.IO;
using System.Linq;

namespace Day04
{
    internal class RollCoordinate
    {
        int Row { get; set; }
        int Col { get; set; }
    }

    internal class Program
	{
        const char roll = '@';
        const int maxAdjacentRolls = 3;

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
			PartB(puzzleInputRaw);
		}

		private static void PartA(List<string> rollGrid)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            int rowLength = rollGrid[0].Length;
            int accessibleRolls = 0;

            for (int row = 1; row < (rollGrid.Count - 1); row++) 
            {
                for (int col = 1; col < (rowLength - 1); col++)
                {
                    // if we're not looking at a roll, skip the space
                    if (rollGrid[row][col] != roll)
                        continue;

                    accessibleRolls += RollIsRemovable(rollGrid, row, col) ? 1 : 0;
                }
            }

            Console.WriteLine($"** The number of accessible paper rolls is: {accessibleRolls:N0}");
        }

        private static bool RollIsRemovable(List<string> rollGrid, int row, int col)
        {
            int adjacentRolls =
                (rollGrid[row][col - 1] == roll ? 1 : 0) +        // to west
                (rollGrid[row][col + 1] == roll ? 1 : 0) +        // to east
                (rollGrid[row - 1][col] == roll ? 1 : 0) +        // to north
                (rollGrid[row + 1][col] == roll ? 1 : 0) +        // to south
                (rollGrid[row - 1][col - 1] == roll ? 1 : 0) +    // to north west
                (rollGrid[row - 1][col + 1] == roll ? 1 : 0) +    // to north east
                (rollGrid[row + 1][col - 1] == roll ? 1 : 0) +    // to south west
                (rollGrid[row + 1][col + 1] == roll ? 1 : 0);     // to south east

            return (adjacentRolls <= maxAdjacentRolls);
        }

        private static void PartB(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");

            List<RollCoordinate> removableRolls = [];
            List<RollCoordinate> removedRolls = [];
		}
	}
}
