using System;
using System.IO;
using System.Linq;

namespace Day04
{
    internal class RollCoordinate
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }

    internal class Program
	{
        const char roll = '@';
        const char emptySpace = '.';
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

        private static void PartB(List<string> rollGrid)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");

            List<RollCoordinate> removableRolls = [];
            List<RollCoordinate> removedRolls = [];

            int rowLength = rollGrid[0].Length;

            // make initial pass through grid to build the list of removable rolls
            for (int row = 1; row < (rollGrid.Count - 1); row++)
            {
                for (int col = 1; col < (rowLength - 1); col++)
                {
                    // if we're not looking at a roll, skip the space
                    if (rollGrid[row][col] != roll)
                        continue;

                    if (RollIsRemovable(rollGrid, row, col))
                    {
                        removableRolls.Add(new RollCoordinate { Row = row, Col = col });
                    }
                }
            }

            while (removableRolls.Count > 0)
            {
                int rollsToRemove = removableRolls.Count;

                // mark all of the removable rolls as empty space
                for (int roll = 0; roll < rollsToRemove; roll++)
                {
                    RollCoordinate rollToRemove = removableRolls[0];

                    // remove the roll from the grid
                    char[] rollRow = rollGrid[rollToRemove.Row].ToCharArray();
                    rollRow[rollToRemove.Col] = emptySpace;
                    rollGrid[rollToRemove.Row] = new string(rollRow);

                    // add it to the list of removed rolls
                    removedRolls.Add(rollToRemove);
                    removableRolls.RemoveAt(0);
                }

                // rescan the grid to figure out new set of removable rolls
                // this is brute force, but my trickier attempt of just scanning
                // rolls immediately around the one just removed didn't work.
                for (int row = 1; row < (rollGrid.Count - 1); row++)
                {
                    for (int col = 1; col < (rowLength - 1); col++)
                    {
                        // if we're not looking at a roll, skip the space
                        if (rollGrid[row][col] != roll)
                            continue;

                        if (RollIsRemovable(rollGrid, row, col))
                        {
                            removableRolls.Add(new RollCoordinate { Row = row, Col = col });
                        }
                    }
                }

                Console.WriteLine($"*** Removed {rollsToRemove:N0} rolls.");

                foreach (var line in rollGrid)
                {
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine($"** Removed a total of {removedRolls.Count:N0} rolls.");
        }
    }
}
