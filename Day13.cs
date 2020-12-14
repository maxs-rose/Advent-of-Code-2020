using System;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day13
    {
        public static string[] puzzleInput;
        
        public static void RunDay13Part1()
        {
            var time = long.Parse(puzzleInput[0]);
            var busses = Array.ConvertAll(puzzleInput[1].Replace("x", "").Split(",").ToList().Where(x => x != "").ToArray(), long.Parse);
            var bustimes = busses.Select(bus => (long) Math.Ceiling((decimal) time / (decimal) bus) * bus).ToList();

            Console.WriteLine((bustimes.Min() - time) * busses[bustimes.IndexOf(bustimes.Min())]);
        }

        public static void RunDay13Part2()
        {
            var busses = puzzleInput[1].Split(",")
                .Select(x => x switch {"x" => -1, _ => long.Parse(x)}).Select((s, ix) => (s, ix)).Where(s => s.s != -1)
                .ToList();

            var increment = busses[0].s;
            var busIndex = 1;
            long i;
            for (i = busses[0].s; busIndex < busses.Count(); i += increment)
            {
                if ((i + busses[busIndex].ix) % busses[busIndex].s == 0)
                {
                    increment *= busses[busIndex].s;
                    busIndex++;
                }
            }

            Console.WriteLine(i - increment);
        }
    }
}