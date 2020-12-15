using System;
using System.Collections.Generic;

namespace Advent_Of_Code_2020
{
    public class Day15
    {
        private static int[] puzzleInput = {0,13,16,17,1,10,6};
        private static readonly int term = 2020;
        private static readonly int term2 = 30000000;

        public static void RunDay15Part1()
        {
            var spoken = new List<int>(puzzleInput);

            for (var i = spoken.Count; i < term; i++)
            {
                var first = findFirst(spoken);
                spoken.Add(i - first);
            }

            Console.WriteLine(spoken[^1]);

            int findFirst(List<int> nList)
            {
                for (var i = nList.Count - 2; i >= 0; i--)
                    if (nList[i] == nList[^1])
                        return i + 1;

                return nList.Count;
            }
        }

        public static void RunDay15Part2()
        {
            var spoken = new Dictionary<int, int>();
            
            for (var i = 0; i < puzzleInput.Length; i++)
                spoken.Add(puzzleInput[i], i+1);
            
            var lastAdded = puzzleInput[^1];
            
            for (var i = puzzleInput.Length; i < term2; i++)
            {
                var prev = findFirst(spoken, lastAdded, i);
                spoken[lastAdded] = i;
                lastAdded = i - prev;
            }
            
            Console.WriteLine(lastAdded);

            int findFirst(Dictionary<int, int> spoke, int last, int t)
            {
                if (spoke.ContainsKey(last))
                    return spoke[last];

                return t;
            }
        }
    }
}