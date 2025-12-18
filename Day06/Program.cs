using System;
using System.IO;
using System.Linq;

namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025: Day 6");
            var puzzleInputRaw = File.ReadLines($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt").ToList();

            List<int[]> problemValues = [.. puzzleInputRaw
                .Where(i => !i.Contains('*'))
                .Select(i => i.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => int.Parse(i)).ToArray())];

            string[] problemOperators = puzzleInputRaw[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

			PartA(problemValues, problemOperators);
			PartB();
		}

		private static void PartA(List<int[]> problemValues, string[] problemOperators)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            ulong grandTotal = 0;

            for (int i = 0; i < problemOperators.Length; i++)
            {
                char problemOperator = problemOperators[i][0];
                ulong problemSolution = (ulong)((problemOperator == '+') ? 0 : 1);
                List<ulong> values = [.. problemValues.Select(v => (ulong)v[i])];

                for (int j = 0; j < values.Count; j++)
                {
                    if (problemOperator == '+')
                    {
                        problemSolution += values[j];
                    }
                    else
                    {
                        problemSolution *= values[j];
                    }
                }

                Console.WriteLine($"** Problem {i}'s {((problemOperator == '+') ? "sum" : "product")} is {problemSolution:N0}");
                grandTotal += problemSolution;
            }

            Console.WriteLine($"*** Grand total is {grandTotal:N0}");
        }

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
    }
}
