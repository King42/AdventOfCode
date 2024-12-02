namespace AdventOfCode2024.Day2;

public class Solver : SolverBase
{
    public Solver(int day, bool debug, bool useTestData)
        : base(day, debug, useTestData)
    {
    }

    public override (object part1, object part2) Solve()
    {
        var matrix = Input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t))).ToList();

        if (Debug)
        {
            Console.WriteLine($"{nameof(matrix)}: {string.Join(' ', matrix.Take(2).SelectMany(t => t))} .. {string.Join(' ', matrix.TakeLast(2).SelectMany(t => t))}");
        }
        
        var safeReports = new List<int>();
        var safeWithDampenerReports = new List<int>();
        for (int i = 0; i < matrix.Count(); i++)
        {
            IEnumerable<int> levels = matrix[i];

            bool isSafe, isSafeWithDampener;
            CheckLevels(levels, out isSafe, out isSafeWithDampener);

            if (isSafe)
            {
                safeReports.Add(i);
            }

            if (!isSafeWithDampener)
            {
                CheckLevels(levels.Reverse(), out _, out isSafeWithDampener);
            }

            if (isSafeWithDampener)
            {
                safeWithDampenerReports.Add(i);
            }
        }

        return (safeReports.Count(), safeWithDampenerReports.Count());
    }

    private static void CheckLevels(IEnumerable<int> levels, out bool isSafe, out bool isSafeWithDampener)
    {
        isSafe = true;
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

                if (isSafe)
                {
                    isSafe = false;
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