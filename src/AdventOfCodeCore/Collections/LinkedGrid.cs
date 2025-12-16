using System.Collections;

namespace AdventOfCodeCore.Collections;

public class LinkedGrid<T> : IEnumerable<LinkedGridNode<T>>
{
    public bool IncludeDiagonals { get; set; } = false;
    public LinkedGridNode<T>? Head { get; set; }
    public LinkedGridNode<T>? LastRowHead
    {
        get
        {
            var current = Head;
            if (current == null) return null;

            while (current.Down != null)
            {
                current = current.Down;
            }

            return current;
        }
    }
    public LinkedGridNode<T>? Tail
    {
        get
        {
            var current = LastRowHead;
            if (current == null) return null;

            while (current.Right != null)
            {
                current = current.Right;
            }

            return current;
        }
    }

    public IEnumerator<LinkedGridNode<T>> GetEnumerator()
    {
        var current = Head;
        while (current != null)
        {
            var rowNode = current;
            while (rowNode != null)
            {
                yield return rowNode;
                rowNode = rowNode.Right;
            }
            current = current.Down;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public LinkedGrid<T> AppendToLastRow(T value)
    {
        var newNode = new LinkedGridNode<T>(value)
        {
            IncludeDiagonals = IncludeDiagonals
        };

        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            var tail = Tail;
            if (tail != null)
            {
                tail.Right = newNode;
                newNode.Left = tail;
                newNode.Position = (tail.Position.X + 1, tail.Position.Y);

                var previousRowNode = tail.Up?.Right;
                if (previousRowNode != null)
                {
                    previousRowNode.Down = newNode;
                    newNode.Up = previousRowNode;
                }
            }
        }

        return this;
    }

    public LinkedGrid<T> AddRow(IEnumerable<T> values)
    {
        LinkedGridNode<T>? previousRowNode = LastRowHead;
        LinkedGridNode<T>? previousNodeInRow = null;

        foreach (var value in values)
        {
            var newNode = new LinkedGridNode<T>(value)
            {
                IncludeDiagonals = IncludeDiagonals
            };

            if (Head == null)
            {
                Head = newNode;
            }

            if (previousNodeInRow != null)
            {
                previousNodeInRow.Right = newNode;
                newNode.Left = previousNodeInRow;
                newNode.Position = (previousNodeInRow.Position.X + 1, previousNodeInRow.Position.Y);
            }

            if (previousRowNode != null)
            {
                previousRowNode.Down = newNode;
                newNode.Up = previousRowNode;
                newNode.Position = (newNode.Position.X, previousRowNode.Position.Y + 1);

                previousRowNode = previousRowNode.Right;
            }

            previousNodeInRow = newNode;
        }

        return this;
    }
}