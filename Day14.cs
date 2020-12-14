using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020
{
    public class Day14
    {
        public static string[] puzzleInput;
        
        public static void RunDay14Part1()
        {
            var maskString = "";
            var memory = new Dictionary<long, long>();
            
            foreach (var input in puzzleInput)
            {
                if (input.Contains("mask"))
                {
                    maskString = input.Split("=").Last().Trim();
                    continue;
                }

                var split = input.Split(new[] {'[', ']', '='});
                var address = long.Parse(split[1]);
                var data = long.Parse(split[^1].Trim());
                
                // modify the data
                for (int i = maskString.Length - 1, j = 0; i >= 0; i--, j++)
                {
                    if (maskString[j] == '1')
                        data |= (1L << i);
                    else if (maskString[j] == '0')
                        data &= ~(1L << i);
                }

                if (memory.ContainsKey(address))
                    memory[address] = data;
                else
                    memory.Add(address, data);
            }

            Console.WriteLine(memory.Sum(x => x.Value));
        }

        public static void RunDay14Part2()
        {
            var maskString = "";
            var memory = new Dictionary<string, long>();
            
            foreach (var input in puzzleInput)
            {
                if (input.Contains("mask"))
                {
                    maskString = input.Split("=").Last().Trim();
                    continue;
                }

                var split = input.Split(new[] {'[', ']', '='});
                var address = Convert.ToString(long.Parse(split[1]), 2).PadLeft(36, '0').ToArray();

                for (var i = 0; i < address.Length; i++)
                    if (maskString[i] == 'X' || maskString[i] == '1')
                        address[i] = maskString[i];

                var addresses = GenerateAddresses(string.Join("", address));
                
                foreach(var adr in addresses)
                    if (memory.ContainsKey(adr))
                        memory[adr] = long.Parse(split[^1].Trim());
                    else
                        memory.Add(adr, long.Parse(split[^1].Trim()));
            }

            Console.WriteLine(memory.Sum(x => x.Value));
        }
        
        private static IEnumerable<string> GenerateAddresses(string address)
        {
            var addresses = new List<string>();
            var rx = new Regex(Regex.Escape("X"));
            
            if(!address.Contains('X'))
                addresses.Add(address);
            else
                for(var i = 0; i <= 1; i++)
                    addresses.AddRange(GenerateAddresses(rx.Replace(address, i.ToString(), 1)));

            return addresses;
        }
    }
}