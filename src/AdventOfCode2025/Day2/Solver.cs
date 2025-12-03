using AdventOfCodeCore;

namespace AdventOfCode2025.Day2;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => true;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1(), SolvePart2());

    private object? SolvePart1()
    {
        Int64 sum = 0;

        var ranges = GetRanges().OrderBy(r => r.Start);

        /*
        if (Debug)
        {
            foreach (var range in ranges)
            {
                    Console.WriteLine($"{range.Start}-{range.End}");
            }
        }
        */

        foreach (var palindrome in GetSequences(ranges.Last().End, 2))
        {
            if (ranges.Any(r => r.Start <= palindrome && r.End >= palindrome))
                sum += palindrome;
        }

        return sum;
    }

    private object? SolvePart2()
    {
        Int64 sum = 0;

        var ranges = GetRanges().OrderBy(r => r.Start);

        foreach (var palindrome in GetSequences(ranges.Last().End))
        {
            if (ranges.Any(r => r.Start <= palindrome && r.End >= palindrome))
                sum += palindrome;
        }

        return sum;
    }

    private List<(Int64 Start, Int64 End)> GetRanges()
    {
        var ranges = new List<(Int64 Start, Int64 End)>();

        foreach (var line in Input)
        {
            foreach (var section in line.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = section.Split('-');
                var start = Int64.Parse(parts[0]);
                var end = Int64.Parse(parts[1]);
                ranges.Add((start, end));
            }
        }

        return ranges;
    }

    private List<Int64> GetSequences(Int64 max, int? maxRepetitions = null)
    {
        var sequences = new List<Int64>();

        #pragma warning disable S1994
        var maxStrLength = max.ToString().Length;
        for (Int64 i = 1; i.ToString().Length <= maxStrLength / 2; i++)
        {
            var numAsStr = $"{i}";
            for (int j = 2; (!maxRepetitions.HasValue || j <= maxRepetitions) && j * numAsStr.Length <= maxStrLength; j++)
            {
                var sequence = Int64.Parse(String.Join(null, Enumerable.Repeat(numAsStr, j)));
                if (sequence > max)
                {
                    break;
                }

                if (!sequences.Contains(sequence))
                {
                    sequences.Add(sequence);
                    //if (Debug) Console.WriteLine($"{sequence} added");
                }
            }
        }
        #pragma warning restore S1994

        if (Debug) Console.WriteLine($"Generated {sequences.Count} sequences up to {max}");

        return sequences;
    }
}