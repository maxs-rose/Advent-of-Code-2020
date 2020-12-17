using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace Advent_Of_Code_2020
{
    public class Day17
    {
        public static string[] puzzleInput;
        private static List<List<string>> board;
        private static List<List<List<string>>> board4d;

        public static void RunDay17Part1()
        {
            FormatInput();

            var iterations = 0;

            while (iterations != 6)
            {
                iterations++;
                ExpandGrid();

                var tempBoard = new List<List<string>>();

                for (var z = 0; z < board.Count; z++)
                {
                    tempBoard.Add(new List<string>());

                    for (var y = 0; y < board[0].Count; y++)
                    {
                        var line = "";
                        for (var x = 0; x < board[0][0].Length; x++)
                        {
                            var friends = GetCells((x, y, z)).Count;

                            if (board[z][y][x] == '#')
                                friends--; // remove myself

                            if (board[z][y][x] == '#')
                            {
                                if (friends == 2 || friends == 3)
                                    line += "#";
                                else
                                    line += ".";
                            }
                            else
                            {
                                if (friends == 3)
                                    line += "#";
                                else
                                    line += ".";
                            }
                        }

                        tempBoard[z].Add(line);
                    }
                }

                board = tempBoard;
            }

            Console.WriteLine(board.Sum(layer => layer.Sum(line => line.Count(x => x == '#'))));

            static void FormatInput()
            {
                board = new List<List<string>> {new(), new(), new()};


                for (var i = 0; i < puzzleInput.Length; i++)
                    board[0].Add("".PadLeft(puzzleInput[0].Length, '.'));

                for (var i = 0; i < puzzleInput.Length; i++)
                    board[2].Add("".PadLeft(puzzleInput[0].Length, '.'));

                for (var i = 0; i < puzzleInput.Length; i++)
                    board[1].Add(puzzleInput[i]);
            }

            static List<char> GetCells((int x, int y, int z) coords)
            {
                var result = new List<char>();

                // cardinal directions
                for (var z = -1; z <= 1; z++)
                    for (var y = -1; y <= 1; y++)
                        for (var x = -1; x <= 1; x++)
                        {
                            // too high or low
                            if (coords.z + z < 0 || coords.z + z >= board.Count)
                                continue;

                            // to up down
                            if (coords.y + y < 0 || coords.y + y >= board[0].Count)
                                continue;

                            // to right or left
                            if (coords.x + x < 0 || coords.x + x >= board[0][0].Length)
                                continue;

                            if (board[coords.z + z][coords.y + y][coords.x + x] == '#')
                                result.Add('#');
                        }

                return result;
            }

            static void ExpandGrid()
            {
                // add a bottom layer
                if (board[0].Any(line => line.Any(c => c == '#')))
                {
                    board.Insert(0, new List<string>());

                    for (var i = 0; i < board[1].Count; i++)
                        board[0].Add("".PadLeft(board[1][0].Length, '.'));
                }

                // add a top layer
                if (board[^1].Any(line => line.Any(c => c == '#')))
                {
                    board.Add(new List<string>());

                    for (var i = 0; i < board[1].Count; i++)
                        board[^1].Add("".PadLeft(board[1][0].Length, '.'));
                }

                // widen the grid
                for (var i = 0; i < board.Count; i++)
                {
                    var found = board[i].Any(line => line.StartsWith('#') || line.EndsWith('#'));

                    if (!found) continue;

                    for (var j = 0; j < board.Count; j++)
                    {
                        for (var k = 0; k < board[j].Count; k++)
                        {
                            board[j][k] = board[j][k].PadLeft(board[j][k].Length + 1, '.');
                            board[j][k] = board[j][k].PadRight(board[j][k].Length + 1, '.');
                        }
                    }

                    break;
                }

                // add an empty col to the left
                for (int z = 0; z < board.Count; z++)
                {
                    if (board[z][0].Contains('#'))
                    {
                        for (var i = 0; i < board.Count; i++)
                            board[i].Insert(0, "".PadLeft(board[i][0].Length, '.'));
                        break;
                    }
                }

                // add an empty col to the right
                for (int z = 0; z < board.Count; z++)
                {
                    if (board[z][^1].Contains('#'))
                    {
                        for (var i = 0; i < board.Count; i++)
                            board[i].Add("".PadLeft(board[i][0].Length, '.'));
                        break;
                    }
                }
            }
        }

        public static void RunDay17Part2()
        {
            FormatInput();

            var iterations = 0;
            
            while (iterations != 6)
            {
                iterations++;
                ExpandBoard();

                var tempBoard = new List<List<List<string>>>();

                for (var w = 0; w < board4d.Count; w++)
                {
                    tempBoard.Add(new List<List<string>>());

                    for (var z = 0; z < board4d[0].Count; z++)
                    {
                        tempBoard[w].Add(new List<string>());

                        for (var y = 0; y < board4d[0][0].Count; y++)
                        {
                            var line = "";

                            for (var x = 0; x < board4d[0][0][0].Length; x++)
                            {
                                var friends = GetCells((x, y, z, w)).Count;

                                if (board4d[w][z][y][x] == '#')
                                    friends--;
                                
                                if (board4d[w][z][y][x] == '#')
                                {
                                    if (friends == 2 || friends == 3)
                                        line += "#";
                                    else
                                        line += ".";
                                }
                                else
                                {
                                    if (friends == 3)
                                        line += "#";
                                    else
                                        line += ".";
                                }
                            }

                            tempBoard[w][z].Add(line);
                        }
                    }
                }
                
                
                board4d = tempBoard;
            }
            
            Console.WriteLine(board4d.Sum(z => z.Sum( y => y.Sum( x => x.Count( c => c == '#')))));

            List<char> GetCells((int x, int y, int z, int w) coords)
            {
                var result = new List<char>();

                for (var w = -1; w <= 1; w++)
                for (var z = -1; z <= 1; z++)
                for (var y = -1; y <= 1; y++)
                for (var x = -1; x <= 1; x++)
                {
                    if (coords.w + w < 0 || coords.w + w >= board4d.Count)
                        continue;

                    if (coords.z + z < 0 || coords.z + z >= board4d[0].Count)
                        continue;

                    if (coords.y + y < 0 || coords.y + y >= board4d[0][0].Count)
                        continue;

                    if (coords.x + x < 0 || coords.x + x >= board4d[0][0][0].Length)
                        continue;

                    if (board4d[coords.w + w][coords.z + z][coords.y + y][coords.x + x] == '#')
                        result.Add('#');
                }

                return result;
            }

            void ExpandBoard()
            {
                // check if we need to add more w's
                if(board4d[0].Any(z => z.Any( y => y.Contains('#'))))
                {
                    board4d.Insert(0, new List<List<string>>());

                    for (var z = 0; z < board4d[1].Count; z++)
                    {
                        board4d[0].Add(new List<string>());
                        for (var y = 0; y < board4d[1][0].Count; y++)
                            board4d[0][z].Add("".PadLeft(board4d[1][0][0].Length, '.'));
                    }
                }
                
                if(board4d[^1].Any(z => z.Any( y => y.Contains('#'))))
                {
                    board4d.Add( new List<List<string>>());

                    for (var z = 0; z < board4d[0].Count; z++)
                    {
                        board4d[^1].Add(new List<string>());
                        for (var y = 0; y < board4d[0][0].Count; y++)
                            board4d[^1][z].Add("".PadLeft(board4d[0][0][0].Length, '.'));
                    }
                }
                
                // add more z's if needed
                for (var w = 0; w < board4d.Count; w++)
                {
                    // add at the start
                    if (board4d[w][0].Any(y => y.Any(c => c == '#')))
                    {
                        for (var w1 = 0; w1 < board4d.Count; w1++)
                        {
                            board4d[w1].Insert(0, new List<string>());

                            for (var i = 0; i < board4d[0][1].Count; i++)
                                board4d[w1][0].Add("".PadLeft(board4d[w1][1][0].Length, '.'));
                        }
                    }
                    
                    if (board4d[w][^1].Any(y => y.Any(c => c == '#')))
                    {
                        for (var w1 = 0; w1 < board4d.Count; w1++)
                        {
                            board4d[w1].Add(new List<string>());

                            for (var i = 0; i < board4d[w1][0].Count; i++)
                                board4d[w1][^1].Add("".PadLeft(board4d[w1][0][0].Length, '.'));
                        }
                    }
                }
                
                // add more y's if needed
                for (var w = 0; w < board4d.Count; w++)
                {
                    for (var z = 0; z < board4d[0].Count; z++)
                    {
                        if (board4d[w][z][0].Any(y => y == '#'))
                        {
                            for (var i = 0; i < board4d.Count; i++)
                            for (var j = 0; j < board4d[0].Count; j++)
                                board4d[i][j].Insert(0, "".PadLeft(board4d[i][j][1].Length, '.'));
                        }
                        
                        if (board4d[w][z][^1].Any(y => y == '#'))
                        {
                            for (var i = 0; i < board4d.Count; i++)
                            for (var j = 0; j < board4d[0].Count; j++)
                                board4d[i][j].Add("".PadLeft(board4d[i][j][0].Length, '.'));
                        }
                    }
                }
                
                // add more x's
                for (var w = 0; w < board4d.Count; w++)
                {
                    for (var z = 0; z < board4d[0].Count; z++)
                    {
                        for (var y = 0; y < board4d[0][0].Count; y++)
                        {
                            if (board4d[w][z][y][0] == '#')
                            {
                                for (var i = 0; i < board4d.Count; i++)
                                    for (var j = 0; j < board4d[0].Count; j++)
                                    for (var k = 0; k < board4d[0][0].Count; k++)
                                        board4d[i][j][k] = "." + board4d[i][j][k];
                            }
                            
                            if (board4d[w][z][y][^1] == '#')
                            {
                                for (var i = 0; i < board4d.Count; i++)
                                    for (var j = 0; j < board4d[0].Count; j++)
                                    for (var k = 0; k < board4d[0][0].Count; k++)
                                        board4d[i][j][k] += ".";
                            }
                        }
                    }
                }
            }

            void FormatInput()
            {
                board4d = new List<List<List<string>>>();
                board4d.Add(new List<List<string>>());
                board4d[0].Add(new List<string>());

                for (var i = 0; i < puzzleInput.Length; i++)
                    board4d[0][0].Add(puzzleInput[i]);
            }
        }
    }
}