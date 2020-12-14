using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2020
{
    public class Day8
    {
        public static string[] puzzleInput;

        public static void RunDay8Part1()
        {
            var acc = 0;
            var completedInstructions = new List<int>();

            for (int i = 0; i < puzzleInput.Length;)
            {
                if (completedInstructions.Contains(i))
                    break;
                
                completedInstructions.Add(i);
                var instruction = decodeInstruction(puzzleInput[i]);

                switch (instruction.instruction)
                {
                    case "nop":
                        i++;
                        break;
                    case "acc":
                        acc += instruction.value;
                        i++;
                        break;
                    case "jmp":
                        i += instruction.value;
                        break;
                }
            }
            
            Console.WriteLine(acc);
        }
        
        public static void RunDay8Part2()
        {
            var acc = 0;
            var lastSwap = 0;
            var finsihed = false;

            while(!finsihed)
            {
                acc = 0;
                var completedInstructions = new List<int>();
                var inputCopy = new List<string>(puzzleInput);

                for (int i = lastSwap; i < inputCopy.Count(); i++)
                {
                    if (inputCopy[i].Contains("nop"))
                    {
                        inputCopy[i] = Regex.Replace(inputCopy[i], @"\bnop\b", "jmp");
                        lastSwap = i+1;
                        break;
                    }
                    
                    if (inputCopy[i].Contains("jmp"))
                    {
                        inputCopy[i] = Regex.Replace(inputCopy[i], @"\bjmp\b", "nop");
                        lastSwap = i+1;
                        break;
                    }
                }
                
                
                for (int i = 0; i < inputCopy.Count();)
                {
                    if (completedInstructions.Contains(i))
                        break;

                    completedInstructions.Add(i);
                    var instruction = decodeInstruction(inputCopy[i]);

                    switch (instruction.instruction)
                    {
                        case "nop":
                            i++;
                            break;
                        case "acc":
                            acc += instruction.value;
                            i++;
                            break;
                        case "jmp":
                            i += instruction.value;
                            break;
                    }

                    finsihed = i == inputCopy.Count();
                }
            }
            
            Console.WriteLine(acc);
        }

        private static (string instruction, int value) decodeInstruction(string instruction)
        {
            var split = instruction.Split(" ");
            return (split[0], int.Parse(split[1]));
        }
    }
}