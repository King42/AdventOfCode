namespace AdventOfCode2024;

class Program
{
    static readonly List<(object? part1, object? part2)> Solutions = new List<(object? part1, object? part2)>() {
        (part1: 1579939, part2: 20351745),
        (part1: 660, part2: 689),
        (part1: 184576302, part2: 118173507 ),
        (part1: 2633, part2: 1936)
    };

    const int Year = 2024;

    static void Main(string[] args)
    {
        var maxDays = DateTime.Today.Year == Year && DateTime.Today.Month == 12 ? DateTime.Today.Day : 25;
        for (int day = 1; day <= maxDays; day++)
        {
            (object? part1, object? part2) solution;            
            if (Solutions.Count() >= day)
            {
                solution = Solutions[day - 1];
            }
            else
            {
                solution = SolverFactory.Create(day).Solve();
            }

            Console.WriteLine($"Day {day}:");
            Console.WriteLine($"  Part 1: {solution.part1}");
            Console.WriteLine($"  Part 2: {solution.part2}");
            Console.WriteLine();
        }
    }
}
