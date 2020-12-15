using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020
{
    public class Day4
    {
        public static string[] puzzleInput;
        public static string[] fields = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid"};
        private static List<Passport> passports = new List<Passport>();

        public static void GeneratePassports()
        {
            var currentLines = new List<string>();

            foreach (var line in puzzleInput)
            {
                if(line != "")
                    currentLines.Add(line);
                else
                {
                    passports.Add(new Passport(currentLines.ToArray()));
                    currentLines.Clear();
                }
            }

            if (!currentLines.Any()) return;
            
            passports.Add(new Passport(currentLines.ToArray()));
            currentLines.Clear();
        }
        
        public static void RunDay4Part1()
        {
            var validPassports = 0;

            foreach (var passport in passports)
            {
                switch (passport.values.Keys.Count())
                {
                    case 8:
                        validPassports++;
                        continue;
                    case 7 when !passport.values.ContainsKey("cid"):
                        validPassports++;
                        break;
                }
            }
            
            Console.WriteLine(validPassports);
        }

        public static void RunDay4Part2()
        {
            var valid = 0;
            
            foreach (var passport in passports)
            {
                if(passport.values.Keys.Count() != 8)
                    if(passport.values.Keys.Count() < 7)
                        continue; // skip passports that are missing required fields
                    else if(passport.values.Keys.Count() == 7 && passport.values.ContainsKey("cid"))
                        continue;

                foreach (var field in fields)
                {
                    if (passport.values.ContainsKey(field))
                    {
                        var val = passport.values[field];
                        switch (field)
                        {
                            case "byr":
                                if (val.Length != 4 || (int.Parse(val) < 1920 || int.Parse(val) > 2002))
                                    goto NextPassport;
                                break;
                            case "iyr":
                                if (val.Length != 4 || (int.Parse(val) < 2010 || int.Parse(val) > 2020))
                                    goto NextPassport;
                                break;
                            case "eyr":
                                if (val.Length != 4 || (int.Parse(val) < 2020 || int.Parse(val) > 2030))
                                    goto NextPassport;
                                break;
                            case "hgt":
                                var numbers = float.Parse(new Regex(@"[^\d]").Replace(val, ""));
                                var unit = new Regex(@"[^\D]").Replace(val, "");

                                if (unit == "cm")
                                {
                                    if (numbers < 150 || numbers > 193)
                                        goto NextPassport;
                                }
                                else if (unit == "in")
                                {
                                    if(numbers < 59 || numbers > 76)
                                        goto NextPassport;
                                }
                                else
                                    goto NextPassport;

                                break;
                            case "hcl":
                                if(!Regex.Match(val, @"^#[0-9a-f]{6}$").Success)
                                    goto NextPassport;
                                break;
                            case "ecl":
                                string[] colours = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                                
                                if(!colours.Contains(val))
                                    goto NextPassport;
                                
                                break;
                            case "pid":
                                if(val.Length != 9)
                                    goto NextPassport;
                                break;
                        }
                    }
                }

                valid++;
                
                NextPassport:;
            }
            
            Console.WriteLine(valid);
        }

        private class Passport
        {
            public Dictionary<string, string> values;

            public Passport(string[] lines)
            {
                values = new Dictionary<string, string>();
            
                foreach (var dataLine in lines)
                {
                    var data = dataLine.Split(' ');
                    foreach (var kv in data)
                    {
                        var split = kv.Split(':');
                        values.Add(split[0], split[1]);
                    }
                }
            }
        }
    }
    
}