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
			var puzzleInputRaw = File.ReadAllText($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt");

			PartA();
			PartB();
		}

		private static void PartA()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");
		}

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
	}
}
