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
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            default:
                throw new NotImplementedException();
        }
    }
}