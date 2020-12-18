using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Advent_Of_Code_2020
{
    public class Day18
    {
        public static string[] puzzleInput;
        
        public static void RunDay18Part1()
        {
            var sum = 0L;

            foreach (var equation in puzzleInput)
                sum += Eval(equation);

            Console.WriteLine(sum);
            
            long Eval(string equation)
            {
                var result = 0L;

                var split = equation.Split(' ');

                int pos = 0;

                var mult = false;
                
                for (;pos < split.Length;)
                {
                    if (split[pos].Contains("("))
                    {
                        var openings = split[pos].Count(x => x == '(');
                        var innerExpression = split[pos++] + " ";
                        
                        for(;openings != 0;)
                        {
                            if (split[pos].Contains(")"))
                            {
                                openings -= split[pos].Count(x => x == ')');
                                innerExpression += split[pos++];
                            }
                            else if (split[pos].Contains("("))
                            {
                                openings += split[pos].Count(x => x == '(');
                                innerExpression += split[pos++];
                            }
                            else
                                innerExpression += split[pos++];

                            innerExpression += " ";
                        }
                        
                        if (mult)
                            result *= Eval(innerExpression[1..^2]);
                        else
                            result += Eval(innerExpression[1..^2]);
                    }
                    else
                    {
                        if (split[pos] == "*")
                        {
                            mult = true;
                            pos++;
                        } else if (split[pos] == "+")
                        {
                            mult = false;
                            pos++;
                        }
                        else
                        {
                            if (mult)
                                result *= long.Parse(split[pos++]);
                            else
                                result += long.Parse(split[pos++]);
                        }
                    }
                }

                return result;
            }
        }

        public static void RunDay18Part2()
        {
            
            var sum = 0L;

            foreach (var equation in puzzleInput)
                sum += Eval(equation);

            Console.WriteLine(sum);
            
            long Eval(string equation)
            {
                var first = 1L;
                var second = 0L;

                var split = equation.Split(' ');

                int pos = 0;

                var mult = false;
                
                for (;pos < split.Length;)
                {
                    if (split[pos].Contains("("))
                    {
                        var openings = split[pos].Count(x => x == '(');
                        var innerExpression = split[pos++] + " ";
                        
                        for(;openings != 0;)
                        {
                            if (split[pos].Contains(")"))
                            {
                                openings -= split[pos].Count(x => x == ')');
                                innerExpression += split[pos++];
                            }
                            else if (split[pos].Contains("("))
                            {
                                openings += split[pos].Count(x => x == '(');
                                innerExpression += split[pos++];
                            }
                            else
                                innerExpression += split[pos++];

                            innerExpression += " ";
                        }
                        
                        if (mult)
                            second = Eval(innerExpression[1..^2]);
                        else
                            second += Eval(innerExpression[1..^2]);
                    }
                    else
                    {
                        if (split[pos] == "*")
                        {
                            mult = true;
                            first *= second;
                            second = 0;
                        } 
                        else if (split[pos] == "+")
                        {
                            mult = false;
                        }
                        else
                        {
                            if (mult)
                                second = long.Parse(split[pos]);
                            else
                                second += long.Parse(split[pos]);
                        }

                        pos++;
                    }
                }

                return second * first;
            }
        }
    }
}