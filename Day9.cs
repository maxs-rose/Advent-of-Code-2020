using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;
using Microsoft.VisualBasic;

namespace Advent_Of_Code_2020
{
    public class Day9
    {
        public static string[] puzzleInput;
        private static long[] convertedInput;
        const int pre = 25;

        public static void ConvertInput()
        {
            convertedInput = Array.ConvertAll(puzzleInput, long.Parse);
        }

        public static long RunDay9Part1()
        {
            ConvertInput();

            for (int i = pre; i < convertedInput.Length; i++)
            {
                var preAmble = convertedInput[(i - pre)..(i)];
                var perms = Array.ConvertAll(GetPermutations(preAmble, 2), x => x.Sum());

                if (!perms.Contains(convertedInput[i]))
                    return convertedInput[i];
            }

            return -1;
        }

        public static void RunDay9Part2()
        {
            var badNumber = RunDay9Part1();

            for (int i = 0; i < convertedInput.Length; i++)
            {
                var sum = convertedInput[i];
                var numbers = new List<long>() { convertedInput[i] };

                for (int j = i + 1; j < convertedInput.Length; j++)
                {
                    if (sum == badNumber && numbers.Count() > 2)
                    {
                        numbers.Sort();
                        Console.WriteLine(numbers[0] + numbers[^1]);
                        return;
                    }

                    if (sum > badNumber)
                        break;

                    sum += convertedInput[j];
                    numbers.Add(convertedInput[j]);
                }
            }
        }
        
        private static T[][] GetPermutations<T>(T[] input, int length)
        {
            if (length == 1)
                return input.Select(t => new[] { t }).ToArray();

            return GetPermutations(input, length - 1).SelectMany(t => input.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new[] {t2}).ToArray()).ToArray();
        }
    }
}