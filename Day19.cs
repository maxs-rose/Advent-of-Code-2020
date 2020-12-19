using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020
{
    public class Day19
    {
        public static string[] puzzleInput;
        private static Dictionary<int, string> rules;
        private static List<string> data;

        private static void FormatInput()
        {
            rules = new();
            data = new();

            var r = true;
            foreach (var line in puzzleInput)
            {
                if (line == "")
                {
                    r = false;
                    continue;
                }

                if (r)
                {
                    var split = line.Split(":");
                    var info = split[1].Trim().Replace("\"", "");
                    rules.Add(int.Parse(split[0]), info);
                }
                else
                    data.Add(line);
            }
        }
        
        public static void RunDay19Part1()
        {
            FormatInput();

            var s = rules[0];

            while (Regex.IsMatch(s, @"\d+"))
                s = Regex.Replace(s, @"\d+", m => $"({rules[int.Parse(m.Value)]})");
            
            var matches = new Regex($"^{s.Replace(" ", "").Replace("\\", "")}$");
            
            Console.WriteLine(data.Count(m => matches.IsMatch(m)));
        }

        public static void RunDay19Part2()
        {
            FormatInput();
            
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            while (true)
            {
                var simple = rules.FirstOrDefault(r => !Regex.IsMatch(r.Value, @"\d+") && r.Key != 42 && r.Key != 31);

                if (simple.Equals(default(KeyValuePair<int, string>)))
                    break;

                foreach (var iKey in rules.Keys.ToArray())
                    rules[iKey] = Regex.Replace(rules[iKey], $"\\b{simple.Key}\\b", $"({simple.Value})");

                rules.Remove(simple.Key);
            }
            
            rules.TrimExcess();

            rules[31] = rules[31].Replace(" ", "").Replace("\"", "");
            rules[42] = rules[42].Replace(" ", "").Replace("\"", "");

            var match = new Regex(
                $"^{rules[0].Replace("8", $"({rules[42]})+").Replace("11", $"(?<A>{rules[42]})+(?<-A>{rules[31]})+").Replace(" ", "")}$");
            
            Console.WriteLine(data.Count(m => match.IsMatch(m)));
        }
    }
}