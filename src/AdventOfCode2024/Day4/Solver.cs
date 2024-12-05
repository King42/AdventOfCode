using System.Globalization;

namespace AdventOfCode2024.Day4;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    public Solver(int day) : base(day)
    {
    }

    public override (object? part1, object? part2) Solve()
    {
        var width = Input.First().Count();
        var height = Input.Count();

        int part1 = 0;

        var rows = Input;
        part1 += FindCount(rows, "Rows");

        var columns = new List<string>();
        for (var col = 0; col < width; col++)
        {
            columns.Add(new string(Input.Select(line => line[col]).ToArray()));
        }
        part1 += FindCount(columns, "Columns");

        var rightDiagonals = new List<string>();
        for (var col = 1 - height; col < width; col++)
        {
            rightDiagonals.Add(new string(Input.Select((line, row) => {
                var diagCol = col + row;
                return 0 <= diagCol && diagCol < width ? line[diagCol] : '.';
            }).ToArray()).Trim());
        }
        part1 += FindCount(rightDiagonals, "Right Diagonals");

        var leftDiagonals = new List<string>();
        for (var col = 0; col < width + height - 1; col++)
        {
            leftDiagonals.Add(new string(Input.Select((line, row) => {
                var diagCol = col - row;
                return 0 <= diagCol && diagCol < width ? line[diagCol] : '.';
            }).ToArray()).Trim());
        }
        part1 += FindCount(leftDiagonals, "Left Diagonals");

        return (part1, "NYI");
    }

    private int FindCount(IEnumerable<string> strings, string header = "UNKNOWN")
    {
        if (Debug)
        {
            Console.WriteLine($"{header}:");
        }

        string term = "XMAS";
        string termReversed = new string(term.Reverse().ToArray());

        int count = 0;
        foreach (var text in strings)
        {
            if (Debug)
            {
                Console.WriteLine(text);
            }
            count += text.Split(term).Length - 1;
            count += text.Split(termReversed).Length - 1;
        }

        if (Debug)
        {
            Console.WriteLine($"{count} instances found");
        }

        return count;
    }
}