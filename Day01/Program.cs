using System;
using System.IO;
using System.Linq;

namespace Day01
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2025: Day 1");
			var puzzleInputRaw = File.ReadLines($"./PuzzleInput-{((args.Length > 0 && args[0].Trim().ToLower() == "test") ? "test" : "full")}.txt").ToList();

			PartA(puzzleInputRaw);
			PartB();
		}

		private static void PartA(List<string> puzzleInputRaw)
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part A");

            int dialPosition = 50;
            int timesStoppedAtZero = 0;

            Console.WriteLine($"** Starting dial position: {dialPosition:N0}");

            foreach ( var rotation in puzzleInputRaw )
            {
                var startingDialPosition = dialPosition;
                var direction = rotation[0];
                var distance = int.Parse(rotation[1..]);
                var extraZeroes = 0;

                // some distances are in the hundreds, so they
                // can be brought down to the last two digits,
                // as rotating the dial 100 spaces just puts it
                // back where it started
                if (distance > 99)
                {
                    extraZeroes = distance / 100;
                    distance %= 100;
                }

                if (direction == 'L')
                {
                    dialPosition -= distance;
                    if (dialPosition < 0)
                    {
                        dialPosition += 100;

                        // if our starting position was at zero, don't count it as an extra click to zero
                        if (startingDialPosition != 0)
                        {
                            extraZeroes++;
                        }
                    }
                }
                else
                {
                    dialPosition += distance;
                    if (dialPosition > 99)
                    {
                        dialPosition -= 100;

                        // if our ending position was at zero, don't count it as an extra click to zero
                        if (dialPosition != 0)
                            extraZeroes++;
                    }
                }

                Console.WriteLine($"*** Dial rotated {rotation} to {dialPosition:N0}, clicking to zero {extraZeroes:N0} extra time(s)");

                timesStoppedAtZero += extraZeroes;

                if (dialPosition == 0)
                    timesStoppedAtZero++;
            }

            Console.WriteLine($"* Dial reached zero {timesStoppedAtZero:N0} times");
		}

		private static void PartB()
		{
			Console.WriteLine("\r\n**********");
			Console.WriteLine("* Part B");
		}
    }
}
