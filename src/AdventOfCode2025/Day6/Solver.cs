using AdventOfCodeCore;

namespace AdventOfCode2025.Day6;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1(), SolvePart2());

    public object SolvePart1()
    {
        var answer = 0L;

        var matrix = ParseInput();
        foreach (var (operation, values) in matrix)
        {
            Console.WriteLine($"Operation: {operation}, Values: {string.Join(", ", values)}");
            switch (operation)
            {
                case "+":
                    answer += values.Sum();
                    break;
                case "*":
                    answer += values.Aggregate((a, b) => a * b);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown operation: {operation}");
            }
            //Console.WriteLine($"  Intermediate answer: {answer}");
        }

        return answer;
    }

    List<(string operation, List<long> values)> ParseInput()
    {
        List<(string operation, List<long> values)>? grid = null;

        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (grid == null)
            {
                grid = new List<(string operation, List<long> values)>(parts.Length);
                for (int i = 0; i < parts.Length; i++)
                {
                    grid.Add(("", new List<long>()));
                }
            }

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "+" || parts[i] == "*")
                {
                    grid[i] = (parts[i], grid[i].values);
                }
                else
                {
                    grid[i].values.Add(int.Parse(parts[i]));
                }
            }
        }

        if (grid == null)
        {
            throw new InvalidOperationException("No input data");
        }

        return grid;
    }

    public object SolvePart2()
    {
        var answer = 0L;

        var lines = File.ReadLines(FilePath);
        var opReader = new StringReader(lines.Last());
        var valueReaders = lines.SkipLast(1).Select(l => new StringReader(l)).Reverse().ToList();

        Operation op;
        while ((op = ReadOperation(opReader.Read())) != Operation.EOL)
        {
            var interimAnswer = 0L;
            
            var numOfColumns = 1;
            while (ReadOperation(opReader.Peek()) == Operation.None)
            {
                numOfColumns++;
                opReader.Read();
            }

            var valueLists = ReadValues(valueReaders, numOfColumns - 1);

            if (Debug)
            {
                Console.Write($"Operation: {op}, Values:");
                for (int i = 0; i < valueLists.Count; i++)
                {
                    Console.Write($" {string.Join(",", valueLists[i])}");
                }
                Console.WriteLine();
            }

            if (op == Operation.Add)
            {
                foreach (var columnValues in valueLists)
                {
                    interimAnswer += columnValues.Sum();
                }
            }
            else if (op == Operation.Multiply)
            {
                interimAnswer = 1L;
                foreach (var columnValues in valueLists)
                {
                    interimAnswer *= columnValues.Sum();
                    if (Debug) Console.WriteLine($"  Interim answer: {interimAnswer}");
                }
            }

            Console.WriteLine($"  Interim answer: {interimAnswer}");
            answer += interimAnswer;
        }

        return answer;
    }

    private static List<List<long>> ReadValues(List<StringReader> valueReaders, int numOfColumns)
    {
        var valueLists = new List<List<long>>(numOfColumns);

        for (int col = 0; col < numOfColumns; col++)
        {
            valueLists.Add(new List<long>());
            foreach (var valueReader in valueReaders)
            {
                long? digit = ReadDigit(valueReader.Read());
                
                if (digit.HasValue)
                {
                    valueLists[col].Add(digit.Value * (long)Math.Pow(10, valueLists[col].Count));
                }
            }
        }

        foreach (var valueReader in valueReaders)
        {
            // consume separator
            valueReader.Read();
        }

        return valueLists;
    }

    private static long? ReadDigit(int val)
    {
        if (val == -1)
        {
            return null;
        }

        if ((char) val >= '0' && (char) val <= '9')
        {
            return val - '0';
        }

        return null;
    }

    private Operation ReadOperation(int val)
    {
        switch (val)
        {
            case '+':
                return Operation.Add;
            case '*':
                return Operation.Multiply;
            case '\n':
            case -1:
                return Operation.EOL;
            default:
                return Operation.None;
        }
    }

    enum Operation
    {
        EOL = -1,
        None = 0,
        Add,
        Multiply
    }
}