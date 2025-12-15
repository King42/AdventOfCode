using AdventOfCodeCore;
using AdventOfCodeCore.Collections;

namespace AdventOfCode2025.Day4;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1(ParseInputToGrid(Input)), SolvePart2());

    private object SolvePart1(LinkedGrid<char> grid, bool removeRolls = false)
    {
        int answer = 0;

        var row = 1;
        foreach (var node in grid)
        {
            if (node.Value == '@')
            {
                int adjacentPaperRolls = 0;
                foreach (var neighbor in node.Neighbors)
                {
                    if (neighbor.Value == '@')
                    {
                        adjacentPaperRolls++;
                    }
                }
                if (adjacentPaperRolls < 4)
                {
                    answer++;
                    if (removeRolls)
                        node.Value = 'x';
                }
            }

            if (node.Right == null)
            {
                row++;
            }
        }

        return answer;
    }

    private object SolvePart2()
    {
        var grid = ParseInputToGrid(Input);

        int prevAnswer;
        var answer = 0;
        do
        {
            if (Debug && answer != 0) Console.WriteLine($"Rolls removed so far: {answer}");
            prevAnswer = answer;
            answer += (int)SolvePart1(grid, true);
        } while (answer != prevAnswer);
        return answer;
    }

    private LinkedGrid<char> ParseInputToGrid(string[] input)
    {
        var grid = new LinkedGrid<char>
        {
            IncludeDiagonals = true
        };

        foreach (var line in input)
        {
            grid.AddRow(line.ToCharArray());
        }

        return grid;
    }
}