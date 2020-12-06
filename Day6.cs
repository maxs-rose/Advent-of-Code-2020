using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day6
    {
        public static string[] puzzleInput;
        private static List<List<string>> formattedPuzzleInput;

        private static void FormatInput()
        {
            var format = new List<List<string>>();

            var inputList = new List<string>();
            foreach (var input in puzzleInput)
            {
                if (input == "")
                {
                    format.Add(inputList);
                    inputList = new List<string>();
                    continue;
                }
                
                inputList.Add(input);
            }
            
            if(inputList.Count() > 0)
                format.Add(inputList);

            formattedPuzzleInput = format;
        }
        
        public static void RunDay6Part1()
        {
            FormatInput();

            var sum = formattedPuzzleInput.Sum(group => string.Join("", group).Distinct().Count());
            
            Console.WriteLine(sum);
        }

        public static void RunDay6Part2()
        {
            var sum = 0;

            foreach (var group in formattedPuzzleInput)
            {
                for (var i = 0; i < group[0].Length; i++)
                    sum += group.TrueForAll(s => s.Contains(group[0][i])) ? 1 : 0;
            }
            
            Console.WriteLine(sum);
        }
    }
}