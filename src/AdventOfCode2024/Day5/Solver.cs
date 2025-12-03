using AdventOfCodeCore;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day5;

public class Solver : SolverBase
{
    protected override bool UseTestData => true;
    protected override bool Debug => true;

    private readonly List<(int Left, int Right)> _rules = new List<(int, int)>();
    private readonly List<IList<int>> _orderings = new List<IList<int>>();
    private readonly List<Regex> _ruleRegexes = new List<Regex>();

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve()
    {
        PopulatePart1Data();
        CompileRulesIntoRegularExpressions();

        List<IList<int>> validOrderings, invalidOrderings;
        ValidateOrderings(out validOrderings, out invalidOrderings);

        var part1 = 0;
        foreach (var ordering in validOrderings)
        {
            int mid = ordering[ordering.Count() / 2];
            part1 += mid;
            if (Debug)
            {
                Console.WriteLine($"{string.Join(',', ordering)}");
                Console.WriteLine($"{mid}");
            }
        }

        var part2 = 0;
        foreach (var ordering in invalidOrderings)
        {
            for (int i = 1; i < ordering.Count(); i++)
            {
                var rotatedOrdering = ordering.Skip(i).ToList();
                rotatedOrdering.AddRange(ordering.Take(i));
                if (IsOrderingValid(_ruleRegexes, rotatedOrdering))
                {
                    int mid = rotatedOrdering[rotatedOrdering.Count() / 2];
                    part2 += mid;
                    if (Debug)
                    {
                        Console.WriteLine($"{string.Join(',', rotatedOrdering)}");
                        Console.WriteLine($"{mid}");
                    }
                    break;
                }
            }
        }

        return (part1, "NYI");
    }

    private void ValidateOrderings(out List<IList<int>> validPartOrderings, out List<IList<int>> invalidPartOrderings)
    {
        validPartOrderings = new List<IList<int>>();
        invalidPartOrderings = new List<IList<int>>();
        foreach (var ordering in _orderings)
        {
            var isValid = IsOrderingValid(_ruleRegexes, ordering);

            if (isValid)
            {
                validPartOrderings.Add(ordering);
            }
            else
            {
                invalidPartOrderings.Add(ordering);
            }
        }
    }

    private static bool IsOrderingValid(List<Regex> regexes, IList<int> ordering)
    {
        bool isValid = true;

        var text = string.Join(',', ordering);
        foreach (var regex in regexes)
        {
            if (regex.IsMatch(text))
            {
                isValid = false;
                break;
            }
        }

        return isValid;
    }

    private void CompileRulesIntoRegularExpressions()
    {
        foreach (var tuple in _rules)
        {
            var negativePattern = $@"({tuple.Right})(,\d*)*,{tuple.Left}";
            if (Debug)
            {
                Console.WriteLine(negativePattern);
            }
            _ruleRegexes.Add(new Regex(negativePattern));
        }
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

            _rules.Add((left, right));
        }

        for (row = row + 1; row < Input.Count(); row++)
        {
            _orderings.Add(Input[row].Split(',').Select(int.Parse).ToList());
        }

        if (Debug)
        {
            Console.WriteLine("Rules");
            var i = 0;
            foreach (var tuple in _rules)
            {
                if (i++ >= 10) break;
                Console.WriteLine($"{tuple.Left}: {tuple.Right}");
            }

            Console.WriteLine("Orderings");
            var j = 0;
            foreach (var ordering in _orderings)
            {
                if (j++ >= 10) break;
                Console.WriteLine($"{string.Join(' ', ordering.Select(t => t.ToString()))}");
            }
        }
    }
}