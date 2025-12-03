using AdventOfCodeCore;

namespace AdventOfCode2024.Day4;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    int Width => Input.First().Count();
    int Height => Input.Count();

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve()
    {
        return (SolvePart1(), SolvePart2());
    }

    private int SolvePart1()
    {
        int part1 = 0;

        var rows = Input;
        part1 += FindCount(rows, "Rows");

        var columns = new List<string>();
        for (var col = 0; col < Width; col++)
        {
            columns.Add(new string(Input.Select(line => line[col]).ToArray()));
        }
        part1 += FindCount(columns, "Columns");

        var rightDiagonals = new List<string>();
        for (var col = 1 - Height; col < Width; col++)
        {
            rightDiagonals.Add(new string(Input.Select((line, row) =>
            {
                var diagCol = col + row;
                return 0 <= diagCol && diagCol < Width ? line[diagCol] : '.';
            }).ToArray()).Trim());
        }
        part1 += FindCount(rightDiagonals, "Right Diagonals");

        var leftDiagonals = new List<string>();
        for (var col = 0; col < Width + Height - 1; col++)
        {
            leftDiagonals.Add(new string(Input.Select((line, row) =>
            {
                var diagCol = col - row;
                return 0 <= diagCol && diagCol < Width ? line[diagCol] : '.';
            }).ToArray()).Trim());
        }
        part1 += FindCount(leftDiagonals, "Left Diagonals");
        return part1;
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

    private int SolvePart2()
    {
        var part2 = 0;

        for (int row = 1; row < Height - 1; row++)
        {
            for (int col = 1; col < Width - 1; col++)
            {
                if (Input[row][col] == 'A')
                {
                    var corners = $"{Input[row - 1][col -1]}{Input[row - 1][col + 1]}{Input[row + 1][col + 1]}{Input[row + 1][col - 1]}";
                    /*
                                    M.S                  S.S                  S.M                  M.M
                                    .A.                  .A.                  .A.                  .A.
                                    M.S                  M.M                  S.M                  S.S
                    */
                    if (corners == "MSSM" || corners == "SSMM" || corners == "SMMS" || corners == "MMSS")
                    {
                        part2++;
                    }
                }
            }
        }

        return part2;
    }
}