namespace AdventOfCode2024;

public class SolverFactory
{
    public static ISolver Create(int day, bool debug, bool useTestData)
    {
        switch (day)
        {
            case 1:
                return new Day1.Solver(day, debug, useTestData);
            case 2:
                return new Day2.Solver(day, debug, useTestData);
            case 3:
                return new Day3.Solver(day, debug, useTestData);
            case 4:
                return new Day4.Solver(day, debug, useTestData);
            // case 5:
            //     return new Day5.Solver(day, debug, useTestData);
            // case 6:
            //     return new Day6.Solver(day, debug, useTestData);
            // case 7:
            //     return new Day7.Solver(day, debug, useTestData);
            // case 8:
            //     return new Day8.Solver(day, debug, useTestData);
            // case 9:
            //     return new Day9.Solver(day, debug, useTestData);
            // case 10:
            //     return new Day10.Solver(day, debug, useTestData);
            // case 11:
            //     return new Day11.Solver(day, debug, useTestData);
            // case 12:
            //     return new Day12.Solver(day, debug, useTestData);
            // case 13:
            //     return new Day13.Solver(day, debug, useTestData);
            // case 14:
            //     return new Day14.Solver(day, debug, useTestData);
            // case 15:
            //     return new Day15.Solver(day, debug, useTestData);
            // case 16:
            //     return new Day16.Solver(day, debug, useTestData);
            // case 17:
            //     return new Day17.Solver(day, debug, useTestData);
            // case 18:
            //     return new Day18.Solver(day, debug, useTestData);
            // case 19:
            //     return new Day19.Solver(day, debug, useTestData);
            // case 20:
            //     return new Day20.Solver(day, debug, useTestData);
            // case 21:
            //     return new Day21.Solver(day, debug, useTestData);
            // case 22:
            //     return new Day22.Solver(day, debug, useTestData);
            // case 23:
            //     return new Day23.Solver(day, debug, useTestData);
            // case 24:
            //     return new Day24.Solver(day, debug, useTestData);
            // case 25:
            //     return new Day25.Solver(day, debug, useTestData);
            default:
                throw new NotImplementedException();
        }
    }
}