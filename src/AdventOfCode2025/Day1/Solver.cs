using AdventOfCodeCore;

namespace AdventOfCode2025.Day1;

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
        int count = 0;
        
        int val = 50;
        foreach (var line in Input)
        {
            val = val + ParseLine(line);
            val = val % 100;

            if (val == 0)
            {
                count++;
            }
        }
        
        return count;
    }

    public object SolvePart2()
    {
        var answer = 0;

        var l = new LinkedList<int>(Enumerable.Range(0, 100));
        var currentNode = l.Find(50);

        foreach (var line in Input)
        {
            int moveBy = ParseLine(line);
            if (moveBy > 0)
            {
                for (int i = 0; i < moveBy; i++)
                {
                    currentNode = NextCircular(currentNode);
                    if (currentNode.Value == 0)
                    {
                        answer++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < -moveBy; i++)
                {
                    currentNode = PreviousCircular(currentNode);
                    if (currentNode.Value == 0)
                    {
                        answer++;
                    }
                }
            }

            if (Debug)
            {
                Console.WriteLine($"{line} : {currentNode.Value} : {answer}");
            }
        }

        return answer;
    }

    public static LinkedListNode<int> NextCircular(LinkedListNode<int> node)
    {
        if (node.Next != null)
        {
            return node.Next;
        }
        else
        {
            return node.List!.First!;
        }
    }

    public static LinkedListNode<int> PreviousCircular(LinkedListNode<int> node)
    {
        if (node.Previous != null)
        {
            return node.Previous;
        }
        else
        {
            return node.List!.Last!;
        }
    }

    public static int ParseLine(string line)
    {
        int addend = int.Parse(line.Substring(1, line.Length - 1));
        
        if (line.StartsWith('L'))
        {
            addend = -1 * addend;
        }

        return addend;
    }
}