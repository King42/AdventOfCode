using AdventOfCodeCore;

namespace AdventOfCode2025.Day3;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1(), SolvePart2());

    private object? SolvePart1()
    {
        long total = 0;
        
        foreach (var line in Input)
        {
            total += GetJoltage(line);
        }

        return total;
    }

    private object? SolvePart2()
    {
        long total = 0;
        
        foreach (var line in Input)
        {
            total += GetJoltage(line, 12);
        }

        return total;
    }

    private long GetJoltage(string line, int numOfBatteries = 2)
    {
        long joltage = 0;
        var batteries = line.Select(t => t - '0').ToList();
        if (Debug) Console.WriteLine($"Batteries: {string.Join(",", batteries)} =>");
        for (int i = numOfBatteries - 1; i >= 0; i--)
        {
            int val = batteries.SkipLast(i).Max();
            joltage = joltage * 10 + val;
            int firstIndex = batteries.IndexOf(val);
            batteries.RemoveRange(0, firstIndex + 1);
        }
        if (Debug) Console.WriteLine($"   Joltage: {joltage}");
        return joltage;
    }
}