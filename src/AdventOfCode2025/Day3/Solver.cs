using AdventOfCodeCore;

using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    public Solver(int day) : base(day)
    {
    }

    public static Regex MulRegex = new Regex(@"mul[(](?<left>\d+),(?<right>\d+)[)]");
    public static Regex ConditionalMulRegex = new Regex(@"(?(do[(][)])do[(][)].*?mul[(](?<left>\d+),(?<right>\d+)[)])");

    public override (object? Part1, object? Part2) Solve()
    {
        var part1 = 0L;
        var part2 = 0L;

        var line = string.Join("", Input);

        part1 += ParseText(line, MulRegex, false);

        foreach (var chunk in $"do(){line}".Split("don't()", StringSplitOptions.RemoveEmptyEntries))
        {
            if (Debug)
            {
                Console.WriteLine($"Processing chunk, '{chunk}', ...");
            }
            foreach (var partial in chunk.Split("do()").Skip(1))
            {
                if (Debug)
                {
                    Console.WriteLine($"Processing do() block, '{partial}', ...");
                }
                part2 += ParseText(partial, MulRegex, false);
            }
        }

        return (part1, part2);
    }

    private static long ParseText(string? input, Regex mulRegex, bool debug)
    {
        var sum = 0L;
        foreach (Match match in mulRegex.Matches(input ?? ""))
        {
            if (debug)
            {
                Console.WriteLine($"Match '{match.Value}' at index {match.Index}");
            }

            var left = int.Parse(match.Groups["left"].Value);
            var right = int.Parse(match.Groups["right"].Value);
            sum += left * right;
        }
        return sum;
    }
}