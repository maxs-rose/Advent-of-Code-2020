using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Advent_Of_Code_2020
{
    public class Day3
    {
        public static string[] puzzleInput = { "..##.......","#...#...#..",".#....#..#.","..#.#...#.#",".#...##..#.","..#.##.....",".#.#.#....#",".#........#","#.##...#...","#...##....#",".#..#...#.#" };
        
        public static void RunDay3Part1()
        {
            var width = puzzleInput[0].Length;
            var height = puzzleInput.Length;

            var x = 0;
            var y = 0;

            var trees = 0;

            while (y < height)
            {
                if (puzzleInput[y][x % width] == '#')
                    trees++;

                y++;
                x += 3;
            }
            
            Console.WriteLine(trees);
        }
        
        public static void RunDay3Part2()
        {
            var width = puzzleInput[0].Length;
            var height = puzzleInput.Length;

            List<int> slopeTotals = new List<int>();
            int[,] slopeParams = {{1, 1}, {3, 1}, {5, 1}, {7, 1}, {1, 2}};


            for (var i = 0; i < 5; i++)
            {
                var x = 0;
                var y = 0;

                var trees = 0;

                while (y < height)
                {
                    if (puzzleInput[y][x % width] == '#')
                        trees++;

                    x += slopeParams[i, 0];
                    y += slopeParams[i, 1];
                }

                slopeTotals.Add(trees);
            }

            Console.WriteLine(slopeTotals.Aggregate((a, x) => a * x));
        }
    }
}