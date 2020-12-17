using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Advent_Of_Code_2020
{
    public class Day16
    {
        public static string[] puzzleInput;
        private static List<Rule> fields = new();
        private static List<int[]> tickets = new();

        public static void FormatInput()
        {
            foreach (var line in puzzleInput)
            {
                if(line == "")
                    continue;

                var split = line.Split(',');
                if (split.Length == 1 && line.Split(' ').Length > 3)
                {
                    var name = line.Split(':')[0];
                    var range1 = Array.ConvertAll(line.Split(':')[1].Trim().Split(' ')[0].Split('-'), int.Parse);
                    var range2 = Array.ConvertAll(line.Split(':')[1].Trim().Split(' ')[2].Split('-'), int.Parse);
                    
                    fields.Add(new Rule() { name = name, range1 = range1, range2 = range2} );
                }
                else if(split.Length > 1)
                    tickets.Add(Array.ConvertAll(split, int.Parse));
            }
        }

        public static void RunDay16Part1()
        {
            var errorTotal = 0;

            foreach (var ticket in tickets)
            {
                foreach (var value in ticket)
                {
                    var passedOne = fields.Any(rule => (value >= rule.range1[0] && value <= rule.range1[1]) || (value >= rule.range2[0] && value <= rule.range2[1]));

                    if (!passedOne)
                        errorTotal += value;
                }
            }
            
            Console.WriteLine(errorTotal);
        }

        public static void RunDay16Part2()
        {
            // get only the valid tickets
            var validTickets = new List<long[]>();
            foreach (var ticket in tickets.ToArray()[1..])
            {
                var passed = ticket.Select(value => fields.Any(rule => (value >= rule.range1[0] && value <= rule.range1[1]) || (value >= rule.range2[0] && value <= rule.range2[1]))).All(passedOne => passedOne);

                if(passed)
                    validTickets.Add(Array.ConvertAll(ticket, t => (long)t));
            }

            // find the index for each rule
            foreach (var rule in fields)
            {
                for (var i = 0; i < validTickets[0].Length; i++)
                {
                    foreach (var ticket in validTickets)
                    {
                        if ((ticket[i] >= rule.range1[0] && ticket[i] <= rule.range1[1]) || (ticket[i] >= rule.range2[0] && ticket[i] <= rule.range2[1]))
                            continue;
                            
                        rule.possibleIndexes.Remove(i);
                        break;
                    }
                }
            }

            int fieldCount = fields.Count, count;

            do
            {
                count = 0;

                foreach (var field in fields)
                {
                    if (field.possibleIndexes.Count != 1) continue;
                    
                    foreach (var f in fields.Where(f => f != field && f.possibleIndexes.Count > 1))
                        f.possibleIndexes.Remove(field.possibleIndexes.First());

                    count += field.possibleIndexes.Count;
                }
                
            } while (count != fieldCount);

            var total = fields.Where(f => f.name.StartsWith("departure")).Select(f => f.possibleIndexes.First()).Aggregate(1L, (current, i) => current * tickets[0][i]);

            Console.WriteLine(total);
        }

        record Rule
        {
            public string name;
            public int[] range1;
            public int[] range2;
            public List<int> possibleIndexes;

            public Rule()
            {
                possibleIndexes = Enumerable.Range(0, 19).ToList();
            }
        }
    }
}