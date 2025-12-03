using AdventOfCodeCore;

namespace AdventOfCode2024.Day2;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve()
    {
        var matrix = Input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t))).ToList();

        if (Debug)
        {
            Console.WriteLine($"{nameof(matrix)}: {string.Join(' ', matrix.Take(2).SelectMany(t => t))} .. {string.Join(' ', matrix.TakeLast(2).SelectMany(t => t))}");
        }
        
        var safeReportsWithoutDampener = new List<int>();
        var safeReportsWithDampener = new List<int>();
        for (int i = 0; i < matrix.Count(); i++)
        {
            IEnumerable<int> levels = matrix[i];

            bool isSafeWithoutDampener, isSafeWithDampener;
            CheckLevels(levels, out isSafeWithoutDampener, out isSafeWithDampener);

            if (isSafeWithoutDampener)
            {
                safeReportsWithoutDampener.Add(i);
            }

            if (!isSafeWithDampener)
            {
                // Covers edge case for when the level to remove is the first one, e.g. 1 9 8 7 6
                CheckLevels(levels.Reverse(), out _, out isSafeWithDampener);
            }

            if (isSafeWithDampener)
            {
                safeReportsWithDampener.Add(i);
            }
        }

        return (safeReportsWithoutDampener.Count(), safeReportsWithDampener.Count());
    }

    private static void CheckLevels(IEnumerable<int> levels, out bool isSafeWithoutDampener, out bool isSafeWithDampener)
    {
        isSafeWithoutDampener = true;
        isSafeWithDampener = true;
        int prevLevel = levels.First();
        int? prevDiff = default;

        foreach (var level in levels.Skip(1))
        {
            var diff = level - prevLevel;

            if (diff < -3
                || diff == 0
                || diff > 3
                || (diff * prevDiff) < 0)
            {

                if (isSafeWithoutDampener)
                {
                    isSafeWithoutDampener = false;
                    continue;
                }
                isSafeWithDampener = false;
                break;
            }

            prevLevel = level;
            prevDiff = diff;
        }
    }
}