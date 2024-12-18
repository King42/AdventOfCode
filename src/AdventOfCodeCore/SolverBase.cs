namespace AdventOfCodeCore;

public class SolverBase : ISolver
{
    protected virtual (object Part1, object Part2)? Solution => null;
    protected virtual (object, object) DefaultSolution => ("NYI", "NYI");
    protected virtual bool Debug => false;
    protected virtual bool UseTestData => false;
    protected readonly int Day;
    protected readonly string[] Input;
    private string FilePath => $@".\Input\Day{Day}{(UseTestData ? ".test" : null)}.txt";

    protected SolverBase(int day)
    {
        Day = day;
        Input = ReadInputLinesAsync().GetAwaiter().GetResult();
    }

    protected async Task<string[]> ReadInputLinesAsync()
    {
        if (Debug)
        {
            Console.WriteLine($"Input path: {FilePath}");
        }

        return await File.ReadAllLinesAsync(FilePath);
    }

    public virtual (object? Part1, object? Part2) Solve()
    {
        if (Solution != null)
        {
            return Solution.Value;
        }

        return DefaultSolution;
    }
}