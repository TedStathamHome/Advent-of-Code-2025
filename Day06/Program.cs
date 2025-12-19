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

			PartA(puzzleInputRaw);
			PartB(puzzleInputRaw);
		}

		private static void PartA(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            List<int[]> problemValues = [.. puzzleInputRaw
                .Where(i => !i.Contains('*'))
                .Select(i => i.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => int.Parse(i)).ToArray())];

            string[] problemOperators = puzzleInputRaw[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

		private static void PartB(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");

            // Grab the operators and their indexes within the string.
            // I've added a space and colon to the end of the string so
            // that it's easy to tell where the last set of numbers is.
            // With these, we know where each character range in each
            // value line the digits appear within.
            var problemOperators = (puzzleInputRaw[^1] + " :")
                .Select((op, index) => new { Op = op, Index = index })
                .ToList()
                .Where(o => "+*:".Contains(o.Op))
                .ToList();

            List<string> problemValuesRaw = puzzleInputRaw[..(puzzleInputRaw.Count - 1)];
            ulong grandTotal = 0;

            for (int o = 0; o < (problemOperators.Count - 1); o++)
            {
                int start = problemOperators[o + 1].Index - 2;
                int end = problemOperators[o].Index;
                char op = problemOperators[o].Op;
                List<ulong> values = [];
                ulong problemSolution = (ulong)((op == '+') ? 0 : 1);

                // loop through the columns in the range, from right to left
                for (int v = start; v >= end; v--)
                {
                    string value = "";

                    foreach (var valueLine in problemValuesRaw)
                    {
                        value += valueLine[v];
                    }
                    Console.WriteLine($"*** Problem {o} - found value of '{value}'");
                    values.Add(ulong.Parse(value.Trim()));
                }

                for (int j = 0; j < values.Count; j++)
                {
                    if (op == '+')
                    {
                        problemSolution += values[j];
                    }
                    else
                    {
                        problemSolution *= values[j];
                    }
                }

                Console.WriteLine($"** Problem {o}'s {((op == '+') ? "sum" : "product")} is {problemSolution:N0}");
                grandTotal += problemSolution;
            }

            Console.WriteLine($"*** Grand total is {grandTotal:N0}");
        }
    }
}
