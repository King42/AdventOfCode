namespace AdventOfCode2024.Day1;

public class Solver : SolverBase
{
    public Solver(int day, bool debug, bool useTestData)
        : base(day, debug, useTestData)
    {
    }

    public override (object? part1, object? part2) Solve()
    {
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(parts[0]));
            right.Add(int.Parse(parts[1]));
        }

        left.Sort();
        right.Sort();

        if (Debug)
        {
            Console.WriteLine($"{nameof(left)}: {string.Join(' ', left.Take(5))} .. {string.Join(' ', left.TakeLast(5))}");
            Console.WriteLine($"{nameof(right)}: {string.Join(' ', right.Take(5))} .. {string.Join(' ', right.TakeLast(5))}");
        }
        
        int part1 = 0;
        for (int i = 0; i < left.Count; i++)
        {
            part1 += Math.Abs(right[i] - left[i]);
        }

        var leftDistinct = left.Distinct();

        int part2 = 0;
        foreach (var i in leftDistinct)
        {
            part2 += i * left.Count(l => l == i) * right.Count(r => r == i);
        }

        return (part1, part2);
    }
}