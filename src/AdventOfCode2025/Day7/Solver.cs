using AdventOfCodeCore;
using AdventOfCodeCore.Collections;

namespace AdventOfCode2025.Day7;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => true;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1WithQueue(), SolvePart2WithQueue());

    public object SolvePart1WithQueue()
    {
        var answer = 0L;

        Queue<LinkedGridNode<ManifoldSpace>> beamQueue = new();
        beamQueue.Enqueue(FindStartNode());

        while (beamQueue.Count > 0)
        {
            //if (Debug) Console.WriteLine($"Queue size: {beamQueue.Count}");

            var currentNode = beamQueue.Dequeue();

            if (currentNode == null || currentNode.Visited)
            {
                continue;
            }

            currentNode.Visited = true;

            if (currentNode.Value.IsSplitter())
            {
                answer++;
                beamQueue.Enqueue(currentNode.Left!.Down!);
                beamQueue.Enqueue(currentNode.Right!.Down!);
            }
            else
            {
                beamQueue.Enqueue(currentNode.Down!);
            }
        }

        return answer;
    }

    public object SolvePart2WithQueue()
    {
        var startNode = FindStartNode();
        var answer = 0L;

        Queue<LinkedGridNode<ManifoldSpace>> beamQueue = new();
        beamQueue.Enqueue(startNode);

        while (beamQueue.Count > 0)
        {
            //if (Debug) Console.WriteLine($"Queue size: {beamQueue.Count}");

            var currentNode = beamQueue.Dequeue();

            if (currentNode == null)
            {
                continue;
            }

            if (Debug) Console.WriteLine($"Processing node at ({currentNode.Position.X}, {currentNode.Position.Y}) with weight {currentNode.Value.Weight}");

            if (currentNode.Value.IsSplitter())
            {
                currentNode.Left!.Down!.Value!.Weight += currentNode.Value.Weight;
                currentNode.Right!.Down!.Value!.Weight += currentNode.Value.Weight;

                if (!beamQueue.Contains(currentNode.Left!.Down!))
                    beamQueue.Enqueue(currentNode.Left!.Down!);

                if (!beamQueue.Contains(currentNode.Right!.Down!))
                    beamQueue.Enqueue(currentNode.Right!.Down!);
            }
            else
            {
                if (currentNode.Down == null)
                {
                    continue;
                }

                currentNode.Down.Value.Weight += currentNode.Value.Weight;
                if (!beamQueue.Contains(currentNode.Down))
                    beamQueue.Enqueue(currentNode.Down);
            }
        }

        var node = _grid.LastRowHead;
        while (node != null)
        {
            Console.Write($"{node.Value.Weight}");
            answer += node.Value.Weight;
            node = node.Right;
        }
        Console.WriteLine();

        return answer;
    }

    LinkedGrid<ManifoldSpace> _grid;
    LinkedGridNode<ManifoldSpace> FindStartNode()
    {
        _grid = ParseInputGrid();
        var startNode = _grid.Head;
        
        while (startNode != null && !startNode.Value.IsStartNode())
        {
            startNode = startNode.Right;
        }

        if (startNode == null)
        {
            throw new InvalidOperationException("No start node found");
        }

        startNode.Value.Weight = 1L;
        return startNode;
    }

    LinkedGrid<ManifoldSpace> ParseInputGrid()
    {
        var grid = new LinkedGrid<ManifoldSpace>();

        // skip rows without splitters
        for (int r = 0; r < Input.Length; r = r + 2)
        {
            grid.AddRow(Input[r].Select(c => new ManifoldSpace(c)).ToList());
        }
        
        // add bottom row with no splitters to ensure that beams can terminate properly
        grid.AddRow(Input[Input.Length - 1].Select(c => new ManifoldSpace(c)).ToList());

        return grid;
    }
}

class ManifoldSpace
{
    public char Value { get; set; }
    public long Weight { get; set; } = 0L;

    public ManifoldSpace(char value)
    {
        Value = value;
    }
    
    public bool IsStartNode() => Value == 'S';
    public bool IsSplitter() => Value == '^';
}

static class Extensions
{
}