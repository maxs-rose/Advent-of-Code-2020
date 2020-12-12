using System;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day12
    {
        public static string[] puzzleInput;

        public static void RunDay12Part1()
        {
            var movement = new int[4];
            var currentDir = 1;

            foreach (var instruct in puzzleInput)
            {
                var dist = int.Parse(instruct[1..]);

                switch (instruct[0])
                {
                    case 'N':
                        totalMovement(ref movement, dist, 0, 2);
                        break;
                    case 'E':
                        totalMovement(ref movement, dist, 1, 3);
                        break;
                    case 'S':
                        totalMovement(ref movement, dist, 2, 0);
                        break;
                    case 'W':
                        totalMovement(ref movement, dist, 3, 1);
                        break;
                    case 'L':
                        while(dist != 0)
                        {
                            currentDir = currentDir - 1 >= 0 ? (currentDir - 1) % 4 : 3;
                            dist -= 90;
                        }
                        
                        break;
                    case 'R':
                        while(dist != 0)
                        {
                            currentDir = (currentDir + 1) % 4;
                            dist -= 90;
                        }
                        
                        break;
                    case 'F':
                        totalMovement(ref movement, dist, currentDir, (currentDir + 2) % 4);
                        break;
                }
            }

            Console.WriteLine(movement.Sum());
        }
        
        public static void RunDay12Part2()
        {
            var waypoint = new [] { 1, 10, 0, 0 };
            var ship = new int[4];

            foreach (var instruct in puzzleInput)
            {
                var dist = int.Parse(instruct[1..]);

                switch (instruct[0])
                {
                    case 'N':
                        totalMovement(ref waypoint, dist, 0, 2);
                        break;
                    case 'E':
                        totalMovement(ref waypoint, dist, 1, 3);
                        break;
                    case 'S':
                        totalMovement(ref waypoint, dist, 2, 0);
                        break;
                    case 'W':
                        totalMovement(ref waypoint, dist, 3, 1);
                        break;
                    case 'L':
                        while(dist != 0)
                        {
                            rotateWaypointLeft(ref waypoint);
                            dist -= 90;
                        }
                        
                        break;
                    case 'R':
                        while(dist != 0)
                        {
                            rotateWaypointRight(ref waypoint);
                            dist -= 90;
                        }
                        
                        break;
                    case 'F':
                        moveShip(ref ship, ref waypoint, dist);
                        break;
                }
            }

            Console.WriteLine(ship.Sum());
        }

        private static void rotateWaypointRight(ref int[] waypoint)
        {
            var p = waypoint[3];

            for (var i = waypoint.Length-1; i > 0; i--)
                waypoint[i] = waypoint[i - 1];

            waypoint[0] = p;
        }
        
        private static void rotateWaypointLeft(ref int[] waypoint)
        {
            var p = waypoint[0];

            for (var i = 0; i < waypoint.Length-1; i++)
                waypoint[i] = waypoint[i + 1];

            waypoint[3] = p;
        }

        private static void moveShip(ref int[] ship, ref int[] waypoint, int movements)
        {
            // move ship north
            var north = waypoint[0] * movements;
            if (ship[2] > north)
                ship[2] -= north;
            else
            {
                var d = north - ship[2];
                ship[2] = 0;
                ship[0] += d;
            }
            
            // move ship south
            var south = waypoint[2] * movements;
            if (ship[0] > south)
                ship[0] -= south;
            else
            {
                var d = south - ship[0];
                ship[0] = 0;
                ship[2] += d;
            }
            
            // move ship east
            var east = waypoint[1] * movements;
            if (ship[3] > east)
                ship[3] -= east;
            else
            {
                var d = east - ship[3];
                ship[3] = 0;
                ship[1] += d;
            }
            
            // move ship west
            var west = waypoint[3] * movements;
            if (ship[1] > west)
                ship[1] -= west;
            else
            {
                var d = west - ship[1];
                ship[1] = 0;
                ship[3] += d;
            }
        }

        private static void totalMovement(ref int[] movement, int dist, int current, int opp)
        {
            if (movement[opp] > dist)
            {
                movement[opp] -= dist;
                return;
            }

            var d = dist - movement[opp];
            movement[opp] = 0;
            movement[current] += d;
        }
    }
}