namespace AdventOfCode2024;

public abstract class SolverBase : ISolver
{
    protected readonly bool Debug;
    protected readonly bool UseTestData;
    protected readonly int Day;
    protected readonly string[] Input;
    private string FilePath => $@".\Input\Day{Day}{(UseTestData ? ".test" : null)}.txt";

    protected SolverBase(int day, bool debug, bool useTestData)
    {
        Debug = debug;
        UseTestData = useTestData;
        Day = day;

        if (Debug)
        {
            Console.WriteLine($"Input path: {FilePath}");
        }
        Input = ReadInputLinesAsync().GetAwaiter().GetResult();
    }

    protected async Task<string[]> ReadInputLinesAsync()
    {
        return await File.ReadAllLinesAsync(FilePath);
    }

    public abstract (object part1, object part2) Solve();
}