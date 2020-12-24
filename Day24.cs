using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day24
    {
        public static string[] puzzleInput;

        private static List<List<string>> coordLines;
        
        private static Dictionary<string, (int x, int y)> coords = new()
        {
            {"e", (2, 0)},
            {"se", (1, 1)},
            {"sw", (-1, 1)},
            {"w", (-2, 0)},
            {"nw", (-1, -1)},
            {"ne", (1, -1)}
        };

        private static void FormatInput()
        {
            coordLines = new();

            foreach (var line in puzzleInput)
            {
                var split = new List<string>();
                
                for (var i = 0; i < line.Length; i++)
                {
                    var c = line[i].ToString();

                    if (i < line.Length - 1)
                        c += line[i + 1];

                    if (coords.Keys.Where(x => x.Length > 1).Contains(c))
                    {
                        split.Add(c);
                        i++;
                    }
                    else
                        split.Add(c[0].ToString());
                }
                
                coordLines.Add(split);
            }
        }

        public static void RunDay24Part1()
        {
            FormatInput();

            var lineCoords = new List<(int x, int y)>();

            foreach (var coordLine in coordLines)
            {
                (int x, int y) coord = (0, 0);

                coordLine.Select(x => coords[x]).ToList().ForEach(v => coord = (coord.x + v.x, coord.y + v.y));

                lineCoords.Add(coord);
            }

            Console.WriteLine(lineCoords.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count() % 2 == 0)
                .Count(x => x.Value == false));
        }

        public static void RunDay24Part2()
        {
            FormatInput();

            var tileCoords = new List<(int x, int y)>();

            foreach (var coordLine in coordLines)
            {
                (int x, int y) coord = (0, 0);

                coordLine.Select(x => coords[x]).ToList().ForEach(v => coord = (coord.x + v.x, coord.y + v.y));

                tileCoords.Add(coord);
            }

            var tileDict = tileCoords.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count() % 2 == 0);

            for (var i = 0; i < 100; i++)
            {
                var tileDictTemp = new Dictionary<(int x, int y), bool>(tileDict);
                var tiles = tileDict.Keys;
                
                foreach (var tile in tiles)
                {
                    var friends = coords.Values.Select(v => (tile.x + v.x, tile.y + v.y));

                    foreach (var t in friends)
                        tileDictTemp[t] = UpdateTile(t, tileDict);

                    tileDictTemp[tile] = UpdateTile(tile, tileDict);
                }

                tileDict = tileDictTemp;
            }
            
            Console.WriteLine(tileDict.Count(x => x.Value == false));
        }

        static bool UpdateTile((int x, int y) tile, Dictionary<(int x, int y), bool> tileDict)
        {
            var exists = tileDict.Keys.Contains(tile);
            var inactiveFriends = coords.Values.Select(v => (tile.x + v.x, tile.y + v.y))
                .Count(v => tileDict.Keys.Contains(v) && !tileDict[v]);
            var inactive = exists && !tileDict[tile];

            if (inactive && (inactiveFriends == 0 || inactiveFriends > 2))
                return true;
            if (!inactive && inactiveFriends == 2)
                return false;
            return !inactive || tileDict[tile];
        }
    }
}