using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace Advent_Of_Code_2020
{
    public class Day11
    {
        public static string[] puzzleInput;
        
        public static void RunDay11Part1()
        {
            var inputCopy = new string[puzzleInput.Length];
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                inputCopy[i] = new string(puzzleInput[i]);
            }

            var rounds = 0;
            bool change;
            do
            {
                rounds++;
                change = false;
                var nextRound = new string[puzzleInput.Length];
                
                for (var y = 0; y < puzzleInput.Length; y++)
                {
                    for (var x = 0; x < puzzleInput[0].Length; x++)
                    {
                        var surroundings = getSurroundings(x, y, inputCopy);
                        switch (inputCopy[y][x])
                        {
                            case 'L':
                                if (surroundings.Count(c => c == '#') == 0)
                                {
                                    change = true;
                                    nextRound[y] += '#';
                                }
                                else
                                    nextRound[y] += 'L';
                                break;
                            case '#':
                                if (surroundings.Count(c => c == '#') >= 4)
                                {
                                    change = true;
                                    nextRound[y] += 'L';
                                }
                                else
                                    nextRound[y] += '#';
                                
                                break;
                            default:
                                nextRound[y] += inputCopy[y][x];
                                break;
                        }
                    }
                }
                
                // copy the changes to the output
                for (var i = 0; i < nextRound.Length; i++)
                    inputCopy[i] = new string(nextRound[i]);
                
            } while (change);
            
            // Console.WriteLine(rounds);
            Console.WriteLine(inputCopy.Sum(x => x.Count(y => y == '#')));
        }
        
        public static void RunDay11Part2()
        {
            var inputCopy = new string[puzzleInput.Length];
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                inputCopy[i] = new string(puzzleInput[i]);
            }

            var rounds = 0;
            bool change;
            do
            {
                rounds++;
                change = false;
                var nextRound = new string[puzzleInput.Length];
                
                for (var y = 0; y < puzzleInput.Length; y++)
                {
                    for (var x = 0; x < puzzleInput[0].Length; x++)
                    {
                        var surroundings = getSeenSeats(x, y, inputCopy);
                        switch (inputCopy[y][x])
                        {
                            case 'L':
                                if (surroundings.Count(c => c == '#') == 0)
                                {
                                    change = true;
                                    nextRound[y] += '#';
                                }
                                else
                                    nextRound[y] += 'L';
                                break;
                            case '#':
                                if (surroundings.Count(c => c == '#') >= 5)
                                {
                                    change = true;
                                    nextRound[y] += 'L';
                                }
                                else
                                    nextRound[y] += '#';
                                
                                break;
                            default:
                                nextRound[y] += inputCopy[y][x];
                                break;
                        }
                    }
                }
                
                // copy the changes to the output
                for (var i = 0; i < nextRound.Length; i++)
                    inputCopy[i] = new string(nextRound[i]);
                
            } while (change);
            
            // Console.WriteLine(rounds);
            Console.WriteLine(inputCopy.Sum(x => x.Count(y => y == '#')));
        }

        private static char[] getSeenSeats(int x, int y, string[] board)
        {
            var returnList = new List<char>();

            // right
            for(var i = x+1; i < board[0].Length; i++)
                if (board[y][i] != '.')
                {
                    returnList.Add(board[y][i]);
                    break;
                }
            
            // left
            for (var i = x-1; i >= 0; i--)
                if (board[y][i] != '.')
                {
                    returnList.Add(board[y][i]);
                    break;
                }
            
            // up
            for (var i = y-1; i >= 0; i--)
                if (board[i][x] != '.')
                {
                    returnList.Add(board[i][x]);
                    break;
                }
                
            // down
            for (var i = y+1; i < board.Length; i++)
                if (board[i][x] != '.')
                {
                    returnList.Add(board[i][x]);
                    break;
                }
            
            // up left
            for(int i = x-1, j = y-1; i >= 0 && j >= 0; i--, j--)
                if (board[j][i] != '.')
                {
                    returnList.Add(board[j][i]);
                    break;
                }
            
            // up right
            for(int i = x+1, j = y-1; i < board[0].Length && j >= 0; i++, j--)
                if (board[j][i] != '.')
                {
                    returnList.Add(board[j][i]);
                    break;
                }
            
            // down right
            for(int i = x+1, j = y+1; i < board[0].Length && j < board.Length; i++, j++)
                if (board[j][i] != '.')
                {
                    returnList.Add(board[j][i]);
                    break;
                }
            
            // down left
            for(int i = x-1, j = y+1; i >= 0 && j < board.Length; i--, j++)
                if (board[j][i] != '.')
                {
                    returnList.Add(board[j][i]);
                    break;
                }
            
            return returnList.ToArray();
        }

        private static char[] getSurroundings(int x, int y, string[] board)
        {
            var returnList = new List<char>();
            for (var i = -1; i <= 1; i++)
                for(var j = -1; j <= 1; j++)
                    if ((x + i >= 0 && x + i < board[0].Length) && (y+j >= 0 && y+j < board.Length))
                        if(!(x+i==x && y+j== y))
                            returnList.Add(board[y+j][x+i]);
                
            return returnList.ToArray();
        }
    }
}