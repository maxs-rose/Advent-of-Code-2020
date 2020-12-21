using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2020
{
    public class Day21
    {
        public static string[] puzzleInput;
        private static List<(List<string> ingredients, string[] allergens)> formattedList = new();
        private static Dictionary<string, List<string>> foodsWithAllergens = new();

        private static void FormatInput()
        {
            formattedList = puzzleInput.Select(l => (ingredients: l.Split(" (contains ")[0].Split(" ").ToList(), allergens: l.Split(" (contains ")[1][..^1].Replace(",", "").Split(" "))).ToList();
        }
        
        public static void RunDay21Part1()
        {
            FormatInput();
            
            var allergens = formattedList.SelectMany(x => x.allergens).Distinct();

            foreach (var allergen in allergens)
            {
                var matchingAllergens = formattedList.Where(x => x.allergens.Contains(allergen))
                    .Select(x => x.ingredients).ToList();
                
                foodsWithAllergens.Add(allergen, matchingAllergens.Aggregate((x, y) => x.Intersect(y).ToList()));
            }
            
            Console.WriteLine(formattedList.SelectMany(x => x.ingredients).Count(i => !foodsWithAllergens.SelectMany(fwa => fwa.Value).Distinct().Contains(i)));
        }

        public static void RunDay21Part2()
        {
            while (foodsWithAllergens.Values.Any(x => x.Count != 1))
            {
                var singles = foodsWithAllergens.Where(x => x.Value.Count == 1).SelectMany(x => x.Value).Distinct().ToList();
                var nonSingles = foodsWithAllergens.Where(x => x.Value.Count > 1).Select(x => x.Key).ToList();
                nonSingles.ForEach(k => foodsWithAllergens[k].RemoveAll(x => singles.Contains(x)));
            }
            
            Console.WriteLine(string.Join("," ,foodsWithAllergens.Select(l => (l.Key, l.Value)).OrderBy(x => x.Key).SelectMany(x => x.Value).Distinct().ToArray()));
        }
    }
}