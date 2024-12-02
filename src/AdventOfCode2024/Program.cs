namespace AdventOfCode2024;

class Program
{
    static void Main(string[] args)
    {
        //var day1 = new Day1.Solver(1, false, false).Solve();
        var day1 = (part1: "1579939", part2: "20351745");
        Console.WriteLine("Day 1:");
        Console.WriteLine($"  Part 1: {day1.part1}");
        Console.WriteLine($"  Part 2: {day1.part2}");
        Console.WriteLine();

        //var day2 = new Day2.Solver(2, false, false).Solve();
        var day2 = (part1: "660", part2: "689");
        Console.WriteLine("Day 2:");
        Console.WriteLine($"  Part 1: {day2.part1}");
        Console.WriteLine($"  Part 2: {day2.part2}");
        Console.WriteLine();
    }
}
