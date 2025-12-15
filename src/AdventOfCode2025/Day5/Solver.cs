using AdventOfCodeCore;

namespace AdventOfCode2025.Day5;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => true;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1(), SolvePart2());

    public object SolvePart1()
    {
        var answer = 0L;
        var freshIngredients = new List<(long start, long end)>();
        var availableIngredients = new List<long>();

        ParseInput1(freshIngredients, availableIngredients);
        freshIngredients = CollapseRanges(freshIngredients);
        answer += availableIngredients.Count(ingredient => freshIngredients.Any(s => s.start <= ingredient && ingredient <= s.end));
        return answer;
    }

    public void ParseInput1(List<(long start, long end)> freshIngredients, List<long> availableIngredients)
    {
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            var parts = line.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);
            freshIngredients.Add((start, end));
        }
        freshIngredients.Sort((a, b) => a.start.CompareTo(b.start));

        foreach (var line in Input.Skip(freshIngredients.Count + 1))
        {
            availableIngredients.Add(long.Parse(line));
        }
    }

    public object SolvePart2()
    {
        var answer = 0L;
        var freshIngredients = new List<(long start, long end)>();

        ParseInput1(freshIngredients, new List<long>());
        freshIngredients = CollapseRanges(freshIngredients);

        foreach (var ingredient in freshIngredients)
        {
            //Console.WriteLine($"Fresh ingredient range: {ingredient.start}-{ingredient.end}");
            if (freshIngredients.Any(other =>
                other != ingredient &&
                other.start <= ingredient.start &&
                ingredient.start <= other.end))
            {
                Console.WriteLine($"Ingredient {ingredient.start} is contained within another range.");
            }

            answer += (ingredient.end - ingredient.start + 1);
        }

        return answer;
    }

    List<(long start, long end)> CollapseRanges(List<(long start, long end)> freshIngredients)
    {
        var collapsedRanges = new List<(long start, long end)>();

        var previousIngredient = freshIngredients[0];
        collapsedRanges.Add(previousIngredient);
        //Console.WriteLine($"Adding new range {previousIngredient.start}-{previousIngredient.end}");
        foreach (var ingredient in freshIngredients.Skip(1))
        {
            if (ingredient.start <= previousIngredient.end)
            {
                // ranges overlap
                previousIngredient.end = Math.Max(previousIngredient.end, ingredient.end);
                //Console.WriteLine($"Merging overlapping ranges to {previousIngredient.start}-{previousIngredient.end}");
                collapsedRanges[collapsedRanges.Count - 1] = previousIngredient;
            }
            else
            {
                collapsedRanges.Add(ingredient);
                previousIngredient = ingredient;
                //Console.WriteLine($"Adding new range {previousIngredient.start}-{previousIngredient.end}");
            }
        }

        return collapsedRanges;
    }
}