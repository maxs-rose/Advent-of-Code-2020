using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day5
    {
        public static string[] puzzleInput = {"BBFFBBFRLL"};

        public static void RunDay5Part1()
        {
            var max = -1;
            
            foreach (var pass in puzzleInput)
            {
                var binaryRow = pass[..^3].Replace('F', '0').Replace('B', '1');
                var binaryCol = pass[^3..].Replace('L', '0').Replace('R', '1');
                var numberRow = Convert.ToInt32(binaryRow, 2);
                var numberCol = Convert.ToInt32(binaryCol, 2);

                if(numberRow * 8 + numberCol > max)
                    max = numberRow * 8 + numberCol;
            }
            
            Console.WriteLine(max);
        }

        public static void RunDay5Part2()
        {
            var seatIds = new List<int>();
            
            foreach (var pass in puzzleInput)
            {
                var binaryRow = pass[..^3].Replace('F', '0').Replace('B', '1');
                var binaryCol = pass[^3..].Replace('L', '0').Replace('R', '1');
                var numberRow = Convert.ToInt32(binaryRow, 2);
                var numberCol = Convert.ToInt32(binaryCol, 2);
                
                seatIds.Add(numberRow * 8 + numberCol);
            }

            seatIds.Sort();
            var missingSeats = new List<int>();

            for (int row = 0; row < 127; row++)
                for(int col = 0; col < 8; col++)
                    missingSeats.Add((row*8)+col);

            // remove the magic seats
            missingSeats.RemoveRange(0, seatIds.Min());
            missingSeats.RemoveRange(seatIds.Max(), missingSeats.Count - 1 - seatIds.Max());
            missingSeats.TrimExcess();
            
            foreach (var id in seatIds)
                missingSeats.Remove(id);
            missingSeats.TrimExcess();
            
            Console.WriteLine(missingSeats[0]);
        }
    }
}