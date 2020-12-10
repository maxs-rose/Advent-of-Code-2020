using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

namespace Advent_Of_Code_2020
{
    public class Day10
    {
        public static string[] puzzleInput;
        private static int[] convertedInput;

        public static void ConvertInput()
        {
            convertedInput = new int[puzzleInput.Length];
            convertedInput = Array.ConvertAll(puzzleInput, int.Parse);
            Array.Sort(convertedInput);
        }
        
        public static void RunDay10Part1()
        {
            ConvertInput();
            
            var chargers = new List<int>() { 0 };
            var diffs = new List<int>() { 3 };

            for (int i = 0; i < convertedInput.Length; i++)
            {
                var diff = convertedInput[i] - chargers.Last();
                if (diff >= 1 && diff <= 3)
                {
                    chargers.Add(convertedInput[i]);
                    diffs.Add(diff);
                }
            }
            
            Console.WriteLine( $"{diffs.Count(x => x == 1)} * {diffs.Count(x => x == 3)} = {diffs.Count(x => x == 1) * diffs.Count(x => x == 3)}");
        }

        public static void RunDay10Part2()
        {
            var ci = convertedInput.ToList();
            ci.Add(convertedInput[^1]+3);
            ci.Add(0);
            ci.Sort();
            convertedInput = ci.ToArray();
            var max = convertedInput[^1];

            var alreadyExplored = new Dictionary<long, long>();

            Console.WriteLine(Combinations(0));
            
            long Combinations(long value)
            {
                if (alreadyExplored.ContainsKey(value))
                    return alreadyExplored[value];
                
                if (value == max)
                {
                    alreadyExplored.Add(value, 1);
                    return 1;
                }

                if (!convertedInput.Contains((int)value))
                    return 0;

                var sum = Combinations(value + 1) + Combinations(value + 2) + Combinations(value + 3);
                alreadyExplored.Add(value, sum);
                
                return sum;
            }
        }
    }
}