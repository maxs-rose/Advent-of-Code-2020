using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public static class Day22
    {
        public static string[] puzzleInput;

        private static Queue<int> player1;
        private static Queue<int> player2;

        private static void FormatInput()
        {
            player1 = new();
            player2 = new();
            
            var p2 = false;
            
            foreach (var line in puzzleInput)
            {
                if(line.Contains("Player"))
                    continue;

                if (line == "")
                {
                    p2 = true;
                    continue;
                }

                if (!p2)
                    player1.Enqueue(int.Parse(line));
                else
                    player2.Enqueue(int.Parse(line));
            }
        }
        
        public static void RunDay22Part1()
        {
            FormatInput();

            while (player1.Count != 0 && player2.Count != 0)
            {
                var (p1, p2) = (player1.Dequeue(), player2.Dequeue());

                if (p1 > p2)
                {
                    player1.Enqueue(p1);
                    player1.Enqueue(p2);
                }
                else
                {
                    player2.Enqueue(p2);
                    player2.Enqueue(p1);
                }
            }

            Console.WriteLine(player1.Select((v, i) => (v, player1.Count - i)).Sum(x => x.v * x.Item2) + player2.Select((v, i) => (v, player2.Count - i)).Sum(x => x.v * x.Item2));
        }

        public static void RunDay22Part2()
        {
            FormatInput();

            RecursiveGame(player1, player2);

            Console.WriteLine(player1.Select((v, i) => (v, player1.Count - i)).Sum(x => x.v * x.Item2) +
                              player2.Select((v, i) => (v, player2.Count - i)).Sum(x => x.v * x.Item2));

            bool RecursiveGame(Queue<int> player1, Queue<int> player2)
            {
                var hands = new HashSet<(int, int)>();

                while (player1.Count > 0 && player2.Count > 0)
                {
                    var hash = (player1.ToList().GetSequenceHashCode(), player2.ToList().GetSequenceHashCode());

                    if (hands.Contains(hash))
                    {
                        player2.Clear();
                        continue;
                    }

                    hands.Add(hash);

                    var (p1c, p2c) = (player1.Dequeue(), player2.Dequeue());

                    bool p1w;

                    if (player1.Count >= p1c && player2.Count >= p2c)
                        p1w = RecursiveGame(new Queue<int>(player1.Take(p1c)), new Queue<int>(player2.Take(p2c)));
                    else
                        p1w = p1c > p2c;

                    if (p1w)
                    {
                        player1.Enqueue(p1c);
                        player1.Enqueue(p2c);
                    }
                    else
                    {
                        player2.Enqueue(p2c);
                        player2.Enqueue(p1c);
                    }
                }

                return player1.Count > 0;
            }
        }

        public static int GetSequenceHashCode<T>(this IList<T> sequence)
        {
            const int seed = 487;
            const int modifier = 31;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) =>
                    (current*modifier) + item.GetHashCode());
            }            
        }
    }
}