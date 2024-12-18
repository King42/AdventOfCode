using AdventOfCodeCore;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day5;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => false;

    private readonly List<(int Left, int Right)> _part1Rules = new List<(int, int)>();
    private readonly List<IList<int>> _part1Orders = new List<IList<int>>();

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve()
    {
        PopulatePart1Data();

        if (Debug)
        {
            Console.WriteLine("Rules");
            var i = 0;
            foreach (var tuple in _part1Rules)
            {
                if (i++ >= 10) break;
                Console.WriteLine($"{tuple.Left}: {tuple.Right}");
            }

            Console.WriteLine("Orders");
            var j = 0;
            foreach (var order in _part1Orders)
            {
                if (j++ >= 10) break;
                Console.WriteLine($"{string.Join(' ', order.Select(t => t.ToString()))}");
            }
        }
        var validPartOrders = new List<IList<int>>();
        var regexes = CompileRulesIntoRegularExpressions();
        foreach (var order in _part1Orders)
        {
            var text = string.Join(',', order);
            var isValid = true;
            foreach (var regex in regexes)
            {
                if (regex.IsMatch(text))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                validPartOrders.Add(order);
            }
        }

        var part1 = 0;
        foreach (var order in validPartOrders)
        {
            int mid = order[order.Count() / 2];
            part1 += mid;
            if (Debug)
            {
                Console.WriteLine($"{string.Join(',', order)}");
                Console.WriteLine($"{mid}");
            }
        }

        return (part1, "NYI");
    }

    private List<Regex> CompileRulesIntoRegularExpressions()
    {
        var regexes = new List<Regex>();
        foreach (var tuple in _part1Rules)
        {
            var negativePattern = $@"({tuple.Right})(,\d*)*,{tuple.Left}";
            if (Debug)
            {
                Console.WriteLine(negativePattern);
            }
            regexes.Add(new Regex(negativePattern));
        }
        return regexes;
    }

    private void PopulatePart1Data()
    {
        int row;

        for (row = 0; row < Input.Count(); row++)
        {
            var ruleText = Input[row];
            if (ruleText == "")
            {
                break;
            }
            var parts = ruleText.Split('|');
            var left = int.Parse(parts[0]);
            var right = int.Parse(parts[1]);

            _part1Rules.Add((left, right));
        }

        for (row = row + 1; row < Input.Count(); row++)
        {
            _part1Orders.Add(Input[row].Split(',').Select(int.Parse).ToList());
        }
    }
}