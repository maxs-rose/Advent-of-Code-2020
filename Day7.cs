using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;

namespace Advent_Of_Code_2020
{
    public class Day7
    {
        public static string[] puzzleInput;
        
        public static void RunDay7Part1()
        {
            var containers = new List<string>() { "shiny gold" };

            for(int i = 0; i < containers.Count; i++)
            {
                foreach (var rule in puzzleInput)
                {
                    var split = rule.Split(' ');
                    var bag = $"{split[0]} {split[1]}";
                    if (!containers.Contains(bag) && string.Join(" ", split[2..]).Contains(containers[i]))
                        containers.Add(bag);
                }
            }
            
            Console.WriteLine(containers.Count() - 1);
        }

        public static void RunDay7Part2()
        {
            var rules = new Dictionary<string, (int count, string colour)[]>();
            foreach (var rule in puzzleInput)
            {
                var split = rule.Split(' ', '.');
                var colour = $"{split[0]} {split[1]}";
                
                var dicAdd = new List<(int, string)>();
                
                var toColours = string.Join(" ", split[4..]).Trim().Split(",");
                foreach (var toC in toColours)
                {
                    var toCsplit = toC.Trim().Split(" ");
                    
                    if(toC.Any(char.IsDigit))
                    {
                        var number = int.Parse(toCsplit[0]);
                        var toColour = $"{toCsplit[1].Trim()} {toCsplit[2].Trim()}";
                        dicAdd.Add((number, toColour));
                    }
                    else
                    {
                        dicAdd.Add((0, ""));
                    }
                }
                
                rules.Add(colour, dicAdd.ToArray());
            }

            Console.WriteLine(RecursiveCount("shiny gold") - 1);
            
            int RecursiveCount(string colour)
                => 1 + (rules.ContainsKey(colour) ? rules[colour].Sum(x => x.count * RecursiveCount(x.colour)) : 0);
        }
    }
}