using AdventOfCodeCore;

namespace AdventOfCode2025;

class Program
{
    static readonly List<(object? part1, object? part2)> Solutions = new List<(object? part1, object? part2)>() {
        (part1: 1026, part2: 5923),
        (part1: 30608905813, part2: 31898925685),
        (part1: 17100, part2: 170418192256861),
    };

    const int Year = 2025;

    static void Main(string[] args)
    {
        var maxDays = DateTime.Today.Year == Year && DateTime.Today.Month == 12 ? DateTime.Today.Day : 12;
        for (int day = 1; day <= maxDays; day++)
        {
            (object? part1, object? part2) solution;            
            if (Solutions.Count() >= day)
            {
                solution = Solutions[day - 1];
            }
            else
            {
                solution = SolverFactory.Create(Year, day)?.Solve() ?? (null, null);
            }

            Console.WriteLine($"Day {day}:");
            Console.WriteLine($"  Part 1: {solution.part1}");
            Console.WriteLine($"  Part 2: {solution.part2}");
            Console.WriteLine();
        }
    }
}
