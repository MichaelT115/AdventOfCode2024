
namespace Day15_WarehouseWoes;

internal static class Program
{
    public static void Main(string[] args)
    {
        using var streamReader = new StreamReader(args[0]);

        var input = streamReader.ReadToEnd().AsSpan();

        var sectionSplit = input.IndexOf("\r\n\r\n");
        {
            var (robotPosition, walls, boxes) = ParseGrid(input[..sectionSplit]);
            ProcessCommands(input[sectionSplit..], robotPosition, walls, boxes);

            Console.WriteLine($"Result: {boxes.Aggregate(0L, (current, box) => current + (box.X + box.Y * 100))}");
        }

        {
            var (robotPosition, walls, boxes) = ParseGrid(input[..sectionSplit], true);
            ProcessCommands(input[sectionSplit..], robotPosition, walls, boxes, true);

            Console.WriteLine(
                $"Result (Part 2): {boxes.Aggregate(0L, (current, box) => current + (box.X + box.Y * 100))}");
        }
    }

    private static bool IsMoveValid(Position startPosition, Position direction, HashSet<Position> walls,
        HashSet<Position> boxes,
        out HashSet<Position> affectedBoxPositions, bool widen = false)
    {
        affectedBoxPositions = [];
        if (direction is { X: 0, Y: 0 })
        {
            return false;
        }

        var lookAtPositions = new Queue<Position>();
        lookAtPositions.Enqueue(startPosition + direction);

        if (!widen)
        {
            while (lookAtPositions.TryDequeue(out var lookAtPosition))
            {
                if (walls.Contains(lookAtPosition))
                {
                    return false;
                }

                if (boxes.Contains(lookAtPosition))
                {
                    affectedBoxPositions.Add(lookAtPosition);
                    lookAtPositions.Enqueue(lookAtPosition + direction);
                }
            }
        }
        else
        {
            while (lookAtPositions.TryDequeue(out var lookAtPosition))
            {
                if (walls.Contains(lookAtPosition))
                {
                    return false;
                }

                var rightOfPosition = lookAtPosition + new Position(1, 0);
                var leftOfPosition = lookAtPosition + new Position(-1, 0);

                if (boxes.Contains(lookAtPosition))
                {
                    affectedBoxPositions.Add(lookAtPosition);

                    if (direction.X == -1)
                    {
                        throw new Exception();
                    }

                    if (direction.X == 1)
                    {
                        lookAtPositions.Enqueue(rightOfPosition + direction);
                    }
                    else
                    {
                        lookAtPositions.Enqueue(lookAtPosition + direction);
                        lookAtPositions.Enqueue(rightOfPosition + direction);
                    }
                }
                else if (boxes.Contains(leftOfPosition))
                {
                    affectedBoxPositions.Add(leftOfPosition);

                    if (direction.X == 1)
                    {
                        throw new Exception();
                    }

                    if (direction.X == -1)
                    {
                        lookAtPositions.Enqueue(leftOfPosition + direction);
                    }
                    else
                    {
                        lookAtPositions.Enqueue(lookAtPosition + direction);
                        lookAtPositions.Enqueue(leftOfPosition + direction);
                    }
                }
            }
        }

        return true;
    }

    private static void ProcessCommands(ReadOnlySpan<char> commands, Position robotPosition,
        HashSet<Position> walls,
        HashSet<Position> boxes, bool widen = false)
    {
        foreach (var command in commands)
        {
            var direction = command switch
            {
                '^' => new Position(0, -1),
                '>' => new Position(1, 0),
                'v' => new Position(0, 1),
                '<' => new Position(-1, 0),
                _ => default
            };

            if (!IsMoveValid(robotPosition, direction, walls, boxes, out var affectedBoxPositions, widen))
            {
                continue;
            }

            robotPosition += direction;
            boxes.ExceptWith(affectedBoxPositions);
            foreach (var position in affectedBoxPositions)
            {
                boxes.Add(position + direction);
            }
        }
    }

    private static (Position robotPosition, HashSet<Position> walls, HashSet<Position> boxes) ParseGrid(
        ReadOnlySpan<char> grid, bool widen = false)
    {
        var width = grid.IndexOf("\n") + 1;

        var position = new Position();
        var wallPositions = new HashSet<Position>();
        var boxPositions = new HashSet<Position>();

        if (!widen)
        {
            for (var i = 0; i < grid.Length; i++)
            {
                switch (grid[i])
                {
                    case '#':
                        wallPositions.Add(new Position(i % width, i / width));
                        break;
                    case 'O':
                        boxPositions.Add(new Position(i % width, i / width));
                        break;
                    case '@':
                        position = new Position(i % width, i / width);
                        break;
                }
            }
        }
        else
        {
            for (var i = 0; i < grid.Length; i++)
            {
                var x = (i * 2) % (width * 2);
                var y = (i * 2) / (width * 2);
                switch (grid[i])
                {
                    case '#':
                        wallPositions.Add(new Position(x, y));
                        wallPositions.Add(new Position(x + 1, y));
                        break;
                    case 'O':
                        boxPositions.Add(new Position(x, y));
                        break;
                    case '@':
                        position = new Position(x, y);
                        break;
                }
            }
        }

        return (position, wallPositions, boxPositions);
    }
}