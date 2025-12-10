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
                const int maxDigits = 12;
                var batteryJoltagesInBank = batteryBank.ToCharArray().Select(b => byte.Parse($"{b}")).ToArray();
                var bankLength = batteryBank.Length;
                ulong maxJoltageInBank = 0;
                Console.WriteLine($"\r\n** Processing bank {batteryBank}");

                for(int i = 0; i < (bankLength - maxDigits); i++)
                {
                    var joltageValue = (new string('0', maxDigits)).ToCharArray();
                    byte currentDigit = 0;
                    byte digitsTaken = 0;

                    for (int j = i; j < bankLength; j++)
                    {
                        if ((bankLength - j) < (maxDigits - digitsTaken + 1))
                        {
                            // if the number of batteries left in the bank is less than the number of digits we need to populate
                            // in joltageValue, take the digit
                            currentDigit = batteryJoltagesInBank[j];
                            joltageValue[digitsTaken] = ($"{currentDigit}").ToCharArray()[0];
                            digitsTaken++;
                        }
                        // take the digit at j
                        // if the next digit is > current digit, add 1 to j and continue
                        // if the next digit is <= current digit, add current digit to string, increment j, and continue
                        else if (digitsTaken < (maxDigits - 1))
                        {
                            // if we haven't reached the last digit we can take, move forward
                            currentDigit = batteryJoltagesInBank[j];

                            // if the next digit is larger than the current digit, move forward to it
                            // we want to take the largest digit we can before adding it to the joltage value
                            if (batteryJoltagesInBank[j + 1] > currentDigit)
                            {
                                continue;
                            }
                            else
                            {
                                // if the next digit is <= the current digit, take the current digit
                                joltageValue[digitsTaken] = ($"{currentDigit}").ToCharArray()[0];
                                digitsTaken++;
                            }
                        }
                        else
                        {
                            // once we've reached the last digit we can take, just take the max digit of the remainder of the string
                            currentDigit = byte.Parse($"{batteryJoltagesInBank[(j)..].Max()}");
                            joltageValue[digitsTaken] = ($"{currentDigit}").ToCharArray()[0];
                            digitsTaken++;
                            break;
                        }
                    }

                    ulong joltage = ulong.Parse(new string(joltageValue));
                    if (joltage > maxJoltageInBank)
                    {
                        maxJoltageInBank = joltage;
                        Console.WriteLine($"*** Current max joltage is {maxJoltageInBank}");
                    }
                }

                joltages.Add(maxJoltageInBank);
                Console.WriteLine($"** Max joltage in bank {batteryBank} is {maxJoltageInBank:N0}");
            }

            ulong totalJoltages = 0;
            foreach (var joltage in joltages)
            {
                totalJoltages += joltage;
            }
            Console.WriteLine($"* Joltage total is: {totalJoltages:N0}");
        }
    }
}
