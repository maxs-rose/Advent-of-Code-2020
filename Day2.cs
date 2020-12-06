using System;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day2
    {
        public static string[] puzzleInput = { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" };

        public static void RunDay2Part1()
        {
            var valid = 0;
            
            foreach (var input in puzzleInput)
            {
                var broken = input.Split(' ');
                var minMax = broken[0].Split('-');
                var min = int.Parse(minMax[0]);
                var max = int.Parse(minMax[1]);
                var looking = broken[1][0];

                var count = broken[2].Count(x => x == looking);

                if (count >= min && count <= max)
                    valid++;
            }
            
            Console.WriteLine(valid);
        }

        public static void RunDay2Part2()
        {
            var valid = 0;
            
            foreach (var input in puzzleInput)
            {
                var broken = input.Split(' ');
                var positions = broken[0].Split('-');
                var pos1 = int.Parse(positions[0]) - 1;
                var pos2 = int.Parse(positions[1]) - 1;
                var looking = broken[1][0];

                if (broken[2].Length > pos1 && broken[2].Length > pos2 &&
                    (broken[2][pos1] == looking ^ 
                    broken[2][pos2] == looking))
                    valid++;
            }
            
            Console.WriteLine(valid);
        }
    }
}