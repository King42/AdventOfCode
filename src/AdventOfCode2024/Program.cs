namespace AdventOfCode2024;

class Program
{
    static readonly List<(object part1, object part2)> Solutions = new List<(object part1, object part2)>() {
        (part1: 1579939, part2: 20351745),
        (part1: "660", part2: "689")
    };

    const int Year = 2024;
    const bool Debug = true;
    const bool UseTestData = true;

    static void Main(string[] args)
    {
        var maxDays = DateTime.Today.Year == Year && DateTime.Today.Month == 12 ? DateTime.Today.Day : 25;
        for (int day = 1; day <= maxDays; day++)
        {
            (object part1, object part2) solution;            
            if (Solutions.Count() >= day)
            {
                solution = Solutions[day - 1];
            }
            else
            {
                solution = SolverFactory.Create(day, Debug, UseTestData).Solve();
            }

            Console.WriteLine($"Day {day}:");
            Console.WriteLine($"  Part 1: {solution.part1}");
            Console.WriteLine($"  Part 2: {solution.part2}");
            Console.WriteLine();
        }
    }
}
