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

            List<ulong> joltages = [];

            foreach (string batteryBank in puzzleInputRaw)
            {
                Console.WriteLine($"\r\n** Processing bank {batteryBank}");

                const int maxDigits = 12;
                byte[] batteryJoltagesInBank = [.. batteryBank.ToCharArray().Select(b => byte.Parse($"{b}"))];
                int bankLength = batteryBank.Length;
                ulong maxJoltageInBank = 0;

                // Fastest approach is to:
                // 1. Determine the max individual battery joltage across the
                //    batteries in the bank, excluding the last (maxDigits - 1)
                //    batteries. Any other joltageValues we calculate which
                //    start with a lower number would just be excluded, so we
                //    won't even bother calculating them.
                // 2. Loop through each occurrance of the value from step 1,
                //    and calculate the joltageValue, taking the highest one.
                byte maxBatteryJoltage = batteryJoltagesInBank[..(bankLength - maxDigits)].Max();
                char maxJoltageChar = $"{maxBatteryJoltage}"[0];
                Console.WriteLine($"*** Max individual battery joltage before final 12 characters: {maxJoltageChar}");

                // if the 12th-last joltage in the battery bank is the same or higher than
                // the maxBatteryJoltage, take the last 12 battery joltages and set it
                // as the maxJoltageInBank.
                if (batteryJoltagesInBank[bankLength - maxDigits] >= maxBatteryJoltage)
                {
                    maxJoltageInBank = ulong.Parse(batteryBank[(bankLength - maxDigits)..]);
                    Console.WriteLine($"*** Initial maxJoltageInBank: {maxJoltageInBank:N0}");
                }

                List<int> maxJoltageIndexes = [.. batteryBank.Select((c, i) => new { Char = c, Index = i})
                    .Where(tuple => tuple.Char == maxJoltageChar)
                    .Select(tuple => tuple.Index)];

                /// find the first occurrance of the highest digit prior to the last (maxDigits - 1) digits,
                /// as this will be the start of the first potential max joltage
                /// after finishing a joltage value, skip forward to the next occurrence of the determined highest digit,
                /// as everything else will be lower valued and not worth calculating

                foreach (int index in maxJoltageIndexes)
                {
                    char[] joltageValue = (new string('0', maxDigits)).ToCharArray();
                    joltageValue[0] = maxJoltageChar;
                    int currentDigit = 1;
                    int currentBattery = index + 1;

                    // if we get to the point where we have run out of batteries
                    // to inspect, we'll want to take the remaining batteries in the bank
                    // also stop if we've reached the need to calculate the final digit
                    while ((currentBattery < (bankLength - (maxDigits - currentDigit))) && (currentDigit < (maxDigits - 1)))
                    {
                        // loop until we find the highest digit before the value drops down
                        if (batteryJoltagesInBank[currentBattery] <= batteryJoltagesInBank[currentBattery + 1])
                        {
                            currentBattery++;
                            continue;
                        }

                        joltageValue[currentDigit] = $"{batteryJoltagesInBank[currentBattery]}"[0];
                        currentDigit++;
                        currentBattery++;
                    }

                    // we're on the last digit, so take the max of the remaining digits
                    if (currentDigit >= (maxDigits - 1))
                    {
                        byte lastDigit = batteryJoltagesInBank[(currentBattery + 1)..].Max();
                        joltageValue[currentDigit] = $"{lastDigit}"[0];
                    }
                    else
                    {
                        // we're not on the last digit, so the remaining digits need to be added to the end of joltValue
                        for (int j = currentBattery; j < bankLength; j++)
                        {
                            joltageValue[currentDigit] = $"{batteryJoltagesInBank[j]}"[0];
                            currentDigit++;
                        }
                    }

                    ulong joltage = ulong.Parse(new string(joltageValue));
                    if (joltage > maxJoltageInBank)
                    {
                        maxJoltageInBank = joltage;
                        Console.WriteLine($"**** Current max joltage is {maxJoltageInBank:N0}");
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
