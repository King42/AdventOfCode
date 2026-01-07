using AdventOfCodeCore;

namespace AdventOfCode2025.Day8;

public class Solver : SolverBase
{
    protected override bool UseTestData => false;
    protected override bool Debug => true;

    public Solver(int day) : base(day)
    {
    }

    public override (object? Part1, object? Part2) Solve() => (SolvePart1(), SolvePart2());

    public object SolvePart1()
    {
        var coordinates = ParseInputCoordinates();
        var pairs = GetPairs(coordinates);
        //if (Debug) pairs.ForEach(d => Console.WriteLine($"Distance {d.Distance} between {d.Pair.coordinateA.Id} and {d.Pair.coordinateB.Id}"));
        var circuits = CreateCircuits(pairs.Select(p => p.Pair).Take(UseTestData ? 10 : 1000).ToList());
        if (Debug) circuits.ForEach((c) =>
        {
            Console.WriteLine($"Circuit (size {c.Count}): {string.Join(", ", c.Select(coord => coord.Id))}");
        });

        return circuits.Take(3).Aggregate(1L, (acc, c) => acc * c.Count);
    }

    List<List<Coordinate3D>> CreateCircuits(List<(Coordinate3D CoordinateA, Coordinate3D CoordinateB)> pairs)
    {
        var circuits = new List<List<Coordinate3D>>();
        Queue<Coordinate3D> toVisit = new Queue<Coordinate3D>();
        foreach (var pair in pairs)
        {
            if (pair.CoordinateA.Visited)
            {
                continue;
            }

            var nodes = new List<Coordinate3D>();

            toVisit.Enqueue(pair.CoordinateA);
            pair.CoordinateA.Visited = true;

            while (toVisit.Count > 0)
            {
                var current = toVisit.Dequeue();
                nodes.Add(current);

                var connectedPairs = pairs.Where(p => p.CoordinateA == current || p.CoordinateB == current);
                foreach (var connectedPair in connectedPairs)
                {
                    var neighbor = connectedPair.CoordinateA == current ? connectedPair.CoordinateB : connectedPair.CoordinateA;
                    if (!neighbor.Visited)
                    {
                        toVisit.Enqueue(neighbor);
                        neighbor.Visited = true;
                    }
                }
            }

            circuits.Add(nodes);
        }

        return circuits.OrderByDescending(c => c.Count).ToList();
    }

    public long SolvePart2()
    {
        var coordinates = ParseInputCoordinates();
        var pairs = GetPairs(coordinates);
        var masterCircuit = CreateMasterCircuit(pairs.Select(p => p.Pair).ToList(), coordinates.Count);

        /*
        foreach (var pair in masterCircuit)
        {
            Console.WriteLine($"Distance {pair.CoordinateA.EuclideanDistanceFrom(pair.CoordinateB)} between {pair.CoordinateA.Id} ({pair.CoordinateA.X}, {pair.CoordinateA.Y}, {pair.CoordinateA.Z}) and {pair.CoordinateB.Id} ({pair.CoordinateB.X}, {pair.CoordinateB.Y}, {pair.CoordinateB.Z})");
        }
        */
        return (long)masterCircuit.Last().CoordinateA.X * masterCircuit.Last().CoordinateB.X;
    }

    List<(Coordinate3D CoordinateA, Coordinate3D CoordinateB)> CreateMasterCircuit(List<(Coordinate3D CoordinateA, Coordinate3D CoordinateB)> pairs, int totalCoordinates)
    {
        var pairsUsed = new List<(Coordinate3D CoordinateA, Coordinate3D CoordinateB)>();
        var masterCircuit = new List<Coordinate3D>()
        {
            // Bootstrap with the first coordinate from the first pair
            pairs[0].CoordinateA
        };
        
        var disconnectedCircuits = new List<List<Coordinate3D>>();
        foreach (var pair in pairs)
        {
            if (Debug)
            {
                Console.WriteLine($"Master circuit contains {masterCircuit.Count}/{totalCoordinates} coordinates: {string.Join(", ", masterCircuit.Select(c => c.Id))}");
                foreach (var disconnectedCircuit in disconnectedCircuits)
                {
                    Console.WriteLine($"Disconnected circuit contains {disconnectedCircuit.Count} coordinates: {string.Join(", ", disconnectedCircuit.Select(c => c.Id))}");
                }
            }

            if (masterCircuit.Count == totalCoordinates)
            {
                if (Debug)
                {
                    Console.WriteLine($"All {masterCircuit.Count} coordinates have been added to the master circuit with {pairsUsed.Count} pairs processed");
                }
                break;
            }

            if (Debug)
            {
                Console.WriteLine($"Considering pair between {pair.CoordinateA.Id} and {pair.CoordinateB.Id}");
            }

            pairsUsed.Add(pair);
            if (masterCircuit.Contains(pair.CoordinateA) && masterCircuit.Contains(pair.CoordinateB))
            {
                if (Debug)
                {
                    Console.WriteLine($"Skipping pair between {pair.CoordinateA.Id} and {pair.CoordinateB.Id} as both are already in the master circuit");
                }
                continue;
            }

            if (masterCircuit.Contains(pair.CoordinateA) || masterCircuit.Contains(pair.CoordinateB))
            {
                var coordinateToAdd = masterCircuit.Contains(pair.CoordinateA) ? pair.CoordinateB : pair.CoordinateA;
                if (Debug)
                {
                    Console.WriteLine($"Adding coordinate {coordinateToAdd.Id} to master circuit");
                }
                masterCircuit.Add(coordinateToAdd);

                ProcessDisconnectedCircuits(masterCircuit, disconnectedCircuits, coordinateToAdd);
                continue;
            }

            bool attachedToDisconnectedCircuit = false;
            foreach (var disconnectedCircuit in disconnectedCircuits)
            {
                if (disconnectedCircuit.Contains(pair.CoordinateA))
                {
                    attachedToDisconnectedCircuit = true;
                    if (Debug)
                    {
                        Console.WriteLine($"Adding coordinate {pair.CoordinateB.Id} to disconnected circuit containing coordinates: {string.Join(", ", disconnectedCircuit.Select(c => c.Id))}");
                    }
                    disconnectedCircuit.Add(pair.CoordinateB);
                }
                else if (disconnectedCircuit.Contains(pair.CoordinateB))
                {
                    attachedToDisconnectedCircuit = true;
                    if (Debug)
                    {
                        Console.WriteLine($"Adding coordinate {pair.CoordinateA.Id} to disconnected circuit containing coordinates: {string.Join(", ", disconnectedCircuit.Select(c => c.Id))}");
                    }
                    disconnectedCircuit.Add(pair.CoordinateA);
                }
            }

            if (!attachedToDisconnectedCircuit)
            {
                if (Debug)
                {
                    Console.WriteLine($"Creating new disconnected circuit with coordinates {pair.CoordinateA.Id} and {pair.CoordinateB.Id}");
                }
                disconnectedCircuits.Add(new List<Coordinate3D>() { pair.CoordinateA, pair.CoordinateB });
            }
        }

        return pairsUsed;
    }

    private void ProcessDisconnectedCircuits(List<Coordinate3D> masterCircuit, List<List<Coordinate3D>> disconnectedCircuits, Coordinate3D coordinateToAdd)
    {
        Queue<Coordinate3D> coordinatesToCheck = new Queue<Coordinate3D>();
        coordinatesToCheck.Enqueue(coordinateToAdd);

        while (coordinatesToCheck.Any())
        {
            var coord = coordinatesToCheck.Dequeue();

            if (Debug)
            {
                Console.WriteLine($"Processing coordinate {coord.Id} for attaching disconnected circuits");
            }

            List<Coordinate3D> coordsToAdd = AttachDisconnectedCircuitsForCoordinate(masterCircuit, disconnectedCircuits, coord);

            if (Debug && coordsToAdd.Any())
            {
                Console.WriteLine($"Added coordinates {string.Join(", ", coordsToAdd.Select(c => c.Id))} to master circuit");
            }
            foreach (var c in coordsToAdd)
            {
                coordinatesToCheck.Enqueue(c);
            }
        }
    }

    private List<Coordinate3D> AttachDisconnectedCircuitsForCoordinate(List<Coordinate3D> masterCircuit, List<List<Coordinate3D>> disconnectedCircuits, Coordinate3D coord)
    {
        if (Debug)
        {
            foreach (var disconnectedCircuit in disconnectedCircuits
                .Where(disconnectedCircuit => disconnectedCircuit.Contains(coord)))
            {
                Console.WriteLine($"Attaching disconnected circuit containing coordinates: {string.Join(", ", disconnectedCircuit.Select(c => c.Id))} to master circuit");
            }
        }

        // Attach any disconnected circuits that contain this coordinate to the master circuit
        var coordsToAdd = disconnectedCircuits.Where(disconnectedCircuit =>
                disconnectedCircuit.Contains(coord))
                .SelectMany(disconnectedCircuit =>
                    disconnectedCircuit.Where(c => !masterCircuit.Contains(c))).ToList();
        masterCircuit.AddRange(coordsToAdd);

        // Remove circuits which were attached to the master circuit
        disconnectedCircuits.RemoveAll(disconnectedCircuit =>
            disconnectedCircuit.Contains(coord));
        return coordsToAdd;
    }

    public List<Coordinate3D> ParseInputCoordinates()
    {
        var coordinates = new List<Coordinate3D>();

        int id = 0;
        foreach (var line in Input)
        {
            var parts = line.Split(',', StringSplitOptions.TrimEntries);
            var x = int.Parse(parts[0]);
            var y = int.Parse(parts[1]);
            var z = int.Parse(parts[2]);
            coordinates.Add(new Coordinate3D(id, x, y, z));
            id++;
        }

        return coordinates;
    }

    static List<(long Distance, (Coordinate3D coordinateA, Coordinate3D coordinateB) Pair)> GetPairs(List<Coordinate3D> coordinates)
    {
        var distances = new List<(long Distance, (Coordinate3D coordinateA, Coordinate3D coordinateB) Pair)>();

        for (int i = 0; i < coordinates.Count; i++)
        {
            var coordA = coordinates[i];

            for (int j = i + 1; j < coordinates.Count; j++)
            {
                var coordB = coordinates[j];
                distances.Add((coordA.EuclideanDistanceFrom(coordB), (coordA, coordB)));
            }
        }

        return distances.OrderBy(d => d.Distance).ToList();
    }
}

public class Coordinate3D
{
    public int Id { get; }
    public int X { get; }
    public int Y { get; }
    public int Z { get; }
    public bool Visited { get; set; } = false;

    public Coordinate3D(int id, int x, int y, int z)
    {
        Id = id;
        X = x;
        Y = y;
        Z = z;
    }
}

static class Extensions
{
    public static long EuclideanDistanceFrom(this Coordinate3D self, Coordinate3D other)
    {
        return (long)Math.Sqrt(Math.Pow(self.X - other.X, 2) + Math.Pow(self.Y - other.Y, 2) + Math.Pow(self.Z - other.Z, 2));
    }
}