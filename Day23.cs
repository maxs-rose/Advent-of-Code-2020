using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day23
    {
        public static string puzzleInput;
        private static LinkedList<int> formattedInput;

        public static void RunDay23Part1()
        {
            formattedInput = new LinkedList<int>(puzzleInput.Select(x => int.Parse(x.ToString())).ToList());

            var cups = new LinkedList<int>(formattedInput);

            var cupIndex = new Dictionary<int, LinkedListNode<int>>();
            var s = cups.First;
            while (s != null)
            {
                cupIndex.Add(s.Value, s);
                s = s.Next;
            }

            var currentCup = cups.First;
            for (var round = 0; round < 100; round++)
            {
                var pickup = new List<LinkedListNode<int>>
                {
                    currentCup.NextOrFirst(),
                    currentCup.NextOrFirst().NextOrFirst(),
                    currentCup.NextOrFirst().NextOrFirst().NextOrFirst(),
                };

                foreach (var picked in pickup)
                    cups.Remove(picked);

                var destCup = currentCup.Value - 1;
                while (destCup < 1 || pickup.Any(x => x.Value == destCup))
                {
                    destCup--;

                    if (destCup < 1)
                        destCup = cupIndex.Count;
                }

                currentCup = currentCup.NextOrFirst();
                var target = cupIndex[destCup];

                foreach (var picked in pickup)
                {
                    cups.AddAfter(target, picked);
                    target = picked;
                }
            }

            var result = "";

            var node = cupIndex[1].NextOrFirst();

            while (node.Value != 1)
            {
                result += node.Value;
                node = node.NextOrFirst();
            }
            
            Console.WriteLine(result);
        }

        public static void RunDay23Part2()
        {
            var input = puzzleInput.Select(x => int.Parse(x.ToString())).ToList();
            input.AddRange(Enumerable.Range(10, 1000001 - 10));
            
            var cups = new LinkedList<int>(input);

            var cupIndex = new Dictionary<int, LinkedListNode<int>>();
            var s = cups.First;
            while (s != null)
            {
                cupIndex.Add(s.Value, s);
                s = s.Next;
            }

            var currentCup = cups.First;
            for (var round = 0; round < 10000000; round++)
            {
                var pickup = new List<LinkedListNode<int>>
                {
                    currentCup.NextOrFirst(),
                    currentCup.NextOrFirst().NextOrFirst(),
                    currentCup.NextOrFirst().NextOrFirst().NextOrFirst(),
                };

                foreach (var picked in pickup)
                    cups.Remove(picked);

                var destCup = currentCup.Value - 1;
                while (destCup < 1 || pickup.Any(x => x.Value == destCup))
                {
                    destCup--;

                    if (destCup < 1)
                        destCup = cupIndex.Count;
                }

                currentCup = currentCup.NextOrFirst();
                var target = cupIndex[destCup];

                foreach (var picked in pickup)
                {
                    cups.AddAfter(target, picked);
                    target = picked;
                }
            }
            
            Console.WriteLine($"{1UL * (ulong)cupIndex[1].NextOrFirst().Value * (ulong)cupIndex[1].NextOrFirst().NextOrFirst().Value}");
        }
    }

    static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
            => current.Next ?? current!.List!.First;
    }
}