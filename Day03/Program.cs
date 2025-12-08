using System;
using System.IO;
using System.Linq;

namespace Day03
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2025: Day 3");
			var puzzleInputRaw = File.ReadLines($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt").ToList();

			PartA(puzzleInputRaw);
			PartB(puzzleInputRaw);
		}

		private static void PartA(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            var joltages = new List<int>();

            foreach (var batteryBank in puzzleInputRaw)
            {
                var batteryJoltagesInBank = batteryBank.ToCharArray();
                int maxJoltageInBank = 0;

                for (int i = 0; i < (batteryJoltagesInBank.Length - 1); i++)
                {
                    int currentJoltage = int.Parse($"{batteryJoltagesInBank[i]}{batteryJoltagesInBank[(i + 1)..].Max()}");

                    if (currentJoltage > maxJoltageInBank)
                        maxJoltageInBank = currentJoltage;

                    if (maxJoltageInBank == 99)
                        break;
                }

                joltages.Add(maxJoltageInBank);
                Console.WriteLine($"** Max joltage in bank {batteryBank} is {maxJoltageInBank:N0}");
            }

            Console.WriteLine($"* Joltage total is: {joltages.Sum():N0}");
        }

		private static void PartB(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");

            var joltages = new List<ulong>();

            foreach (var batteryBank in puzzleInputRaw)
            {
                var batteryJoltagesInBank = batteryBank.ToCharArray().Select(b => byte.Parse($"{b}")).ToArray();
                ulong maxJoltageInBank = 0;

                for(int i = 0; i < ( batteryJoltagesInBank.Length - 12); i++)
                {
                    // take the character at i
                    // if the next character is > current character, add 1 to i and continue
                    // if the next character is <= current character, add current character to string, increment i, and continue
                }
            }
		}
	}
}
