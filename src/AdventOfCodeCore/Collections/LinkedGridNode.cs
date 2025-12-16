namespace AdventOfCodeCore.Collections;

public class LinkedGridNode<T>
{
    public (int X, int Y) Position { get; set; }
    public T Value { get; set; }
    public bool IncludeDiagonals { get; set; } = false;
    public bool Visited { get; set; } = false;

    public IEnumerable<LinkedGridNode<T>> Neighbors
    {
        get
        {
            if (Up != null) yield return Up;
            if (Down != null) yield return Down;
            if (Left != null) yield return Left;
            if (Right != null) yield return Right;
            if (IncludeDiagonals)
            {
                if (Up?.Left != null) yield return Up.Left;
                if (Up?.Right != null) yield return Up.Right;
                if (Down?.Left != null) yield return Down.Left;
                if (Down?.Right != null) yield return Down.Right;
            }
        }
    }

    public LinkedGridNode<T>? Up { get; set; }
    public LinkedGridNode<T>? Down { get; set; }
    public LinkedGridNode<T>? Left { get; set; }
    public LinkedGridNode<T>? Right { get; set; }

    public LinkedGridNode(T value)
    {
        Value = value;
    }
}