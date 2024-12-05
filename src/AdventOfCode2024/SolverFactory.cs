namespace AdventOfCode2024;

public class SolverFactory
{
    public static ISolver Create(int day)
    {
        switch (day)
        {
            case 1:
                return new Day1.Solver(day);
            case 2:
                return new Day2.Solver(day);
            case 3:
                return new Day3.Solver(day);
            case 4:
                return new Day4.Solver(day);
            case 5:
                return new Day5.Solver(day);
            // case 6:
            //     return new Day6.Solver(day);
            // case 7:
            //     return new Day7.Solver(day);
            // case 8:
            //     return new Day8.Solver(day);
            // case 9:
            //     return new Day9.Solver(day);
            // case 10:
            //     return new Day10.Solver(day);
            // case 11:
            //     return new Day11.Solver(day);
            // case 12:
            //     return new Day12.Solver(day);
            // case 13:
            //     return new Day13.Solver(day);
            // case 14:
            //     return new Day14.Solver(day);
            // case 15:
            //     return new Day15.Solver(day);
            // case 16:
            //     return new Day16.Solver(day);
            // case 17:
            //     return new Day17.Solver(day);
            // case 18:
            //     return new Day18.Solver(day);
            // case 19:
            //     return new Day19.Solver(day);
            // case 20:
            //     return new Day20.Solver(day);
            // case 21:
            //     return new Day21.Solver(day);
            // case 22:
            //     return new Day22.Solver(day);
            // case 23:
            //     return new Day23.Solver(day);
            // case 24:
            //     return new Day24.Solver(day);
            // case 25:
            //     return new Day25.Solver(day);
            default:
                throw new NotImplementedException();
        }
    }
}