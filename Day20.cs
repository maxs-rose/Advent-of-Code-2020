using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Advent_Of_Code_2020
{
    public class Day20
    {
        public static string[] puzzleInput;
        private static List<Tile> allTiles = new();
        private static List<List<Tile>> tileGrid = new();
        private static Dictionary<int, Tile> tileDict;

        private static void FormatInput()
        {
            var id = -1;
            var grid = new List<string>();
            
            foreach (var line in puzzleInput)
            {
                if (line == "")
                {
                    allTiles.Add(new Tile(id, grid));
                    
                    id = -1;
                    grid = new List<string>();
                    continue;
                }

                if (id == -1)
                    id = int.Parse(line.Split(" ")[1][..^1].Trim());
                else
                    grid.Add(line);
            }
            
            if(id != -1)
                allTiles.Add(new Tile(id, grid));
        }
        
        public static void RunDay20Part1()
        {
            FormatInput();

            tileDict = allTiles.ToDictionary(tile => tile.id);

            var tileStack = new Stack<Tile>();
            var remainingTiles = new List<Tile>(allTiles);

            tileStack.Push(remainingTiles.First());

            while (tileStack.Count > 0)
            {
                var currentTile = tileStack.Pop();
                remainingTiles.Remove(currentTile);
                var currentBorders = currentTile.GetSides();

                remainingTiles.ForEach(tile =>
                {
                    foreach (var border in tile.GetSides())
                    {
                        var iOther = tile.GetSides().IndexOf(border);
                        var reversedBorder = new string(border.ToCharArray().Reverse().ToArray());
                        
                        if (currentBorders.Contains(border))
                        {
                            var iCurrent = currentBorders.IndexOf(border);
                            var rot = ((iCurrent + 4) - Tile.Opposite(iOther)) % 4;

                            if (currentTile.GetConnection(iCurrent) == null)
                            {
                                tileDict[tile.id].Rotate(rot);

                                if (iCurrent == 0 || iCurrent == 2)
                                    tileDict[tile.id].FlipVertical();
                                else
                                    tileDict[tile.id].FlipHorizontal();

                                currentTile.SetNeibor(iCurrent, tile.id);
                                tile.SetNeibor(Tile.Opposite(iCurrent), currentTile.id);

                                tileStack.Push(tile);
                            }
                        }
                        else if (currentBorders.Contains(reversedBorder))
                        {
                            var iCurrent = currentBorders.IndexOf(reversedBorder);
                            var rot = ((iCurrent + 4) - Tile.Opposite(iOther)) % 4;

                            if (currentTile.GetConnection(iCurrent) == null)
                            {
                                tileDict[tile.id].Rotate(rot);

                                currentTile.SetNeibor(iCurrent, tile.id);
                                tile.SetNeibor(Tile.Opposite(iCurrent), currentTile.id);

                                tileStack.Push(tile);
                            }
                        }
                    }
                });
            }

            var image = new Tile[(int) Math.Round(Math.Sqrt(allTiles.Count))][];
            var left = allTiles.First(t => t.up == -1 && t.left == -1);

            for (var i = 0; i < image.Length; i++)
                image[i] = new Tile[(int) Math.Round(Math.Sqrt(allTiles.Count))];

            for (var y = 0; y < image.Length; y++)
            {
                var right = left;
                for (var x = 0; x < image[y].Length; x++)
                {
                    image[y][x] = right;
                    right = tileDict[right.right];
                }

                left = tileDict[left.down];
            }

        }

        class Tile
        {
            public readonly int id;
            public List<string> grid;

            public int up = -1;
            public int right = -1;
            public int down = -1;
            public int left = -1;

            public Tile(int id, List<string> grid)
            {
                this.id = id;
                this.grid = grid;
            }

            public void SetNeibor(int pos, int t)
            {
                switch (pos)
                {
                    case 0:
                        up = t;
                        break;
                    case 1:
                        right = t;
                        break;
                    case 2:
                        down = t;
                        break;
                    case 3:
                        left = t;
                        break;
                }
            }

            public Tile GetConnection(int side)
            {
                switch (side)
                {
                    case 0 when up != -1:
                        return tileDict[up];
                    case 1 when right != -1:
                        return tileDict[right];
                    case 2 when down != -1:
                        return tileDict[down];
                    case 3 when left != -1:
                        return tileDict[left];
                }

                return null;
            }

            public static int Opposite(int side)
            {
                return side switch
                {
                    0 => 2,
                    1 => 3,
                    2 => 0,
                    3 => 1,
                    _ => -1
                };
            }
            
            public List<string> GetSides()
            {
                var result = new List<string>
                {
                    grid[0],
                    string.Join("", grid.Select(g => g[^1]).ToArray()),
                    grid[^1],
                    string.Join("", grid.Select(g => g[0]).ToArray())
                };

                return result;
            }

            public void Rotate(int times = 1)
            {
                for (var i = 0; i < times; i++)
                {
                    var res = new char[10, 10];
                    
                    for(var row = 0; row < grid.Count; row++)
                        for(var col = 0; col < grid.Count; col++)
                        {
                            var newRow = col;
                            var newCol = grid.Count - (row + 1);
                            res[newCol, newRow] = grid[row][col];
                        }

                    var stringRes = new List<string>();

                    for (var row = 0; row < 10; row++)
                    {
                        var l = "";
                        for (var col = 0; col < 10; col++)
                            l += res[col, row];
                        stringRes.Add(l);
                    }
                    
                    grid = stringRes;
                }
            }

            public void FlipVertical()
            {
                grid.Reverse();
            }
            
            public void FlipHorizontal()
            {
                grid = grid.Select(g => new string(g.Reverse().ToArray())).ToList();
            }
        }
    }
}