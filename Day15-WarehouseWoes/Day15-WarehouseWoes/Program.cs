
namespace Day15_WarehouseWoes;

internal static class Program
{
    public static void Main(string[] args)
    {
        using var streamReader = new StreamReader(args[0]);

        var input = streamReader.ReadToEnd().AsSpan();

        var sectionSplit = input.IndexOf("\r\n\r\n");

        var (robotPosition, walls, boxes) = ParseGrid(input[..sectionSplit]);
        ProcessCommands(input[sectionSplit..], robotPosition, walls, boxes);

        Console.WriteLine($"Result: {boxes.Aggregate(0L, (current, box) => current + (box.X + box.Y * 100))}");
    }

    private static bool IsMoveValid(Position startPosition, Position direction, HashSet<Position> walls,
        HashSet<Position> boxes,
        out Position firstEmptyPositionInDirection)
    {
        firstEmptyPositionInDirection = default;
        if (direction is { X: 0, Y: 0 })
        {
            return false;
        }

        var lookAtPosition = startPosition;

        while (true)
        {
            lookAtPosition += direction;

            if (walls.Contains(lookAtPosition))
            {
                return false;
            }

            if (boxes.Contains(lookAtPosition))
            {
                continue;
            }

            firstEmptyPositionInDirection = lookAtPosition;
            return true;
        }
    }

    private static void ProcessCommands(ReadOnlySpan<char> commands, Position robotPosition,
        HashSet<Position> walls,
        HashSet<Position> boxes)
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

            if (!IsMoveValid(robotPosition, direction, walls, boxes, out var firstEmptyPositionInDirection))
            {
                continue;
            }

            robotPosition += direction;
            if (firstEmptyPositionInDirection == robotPosition)
            {
                continue;
            }

            boxes.Remove(robotPosition);
            boxes.Add(firstEmptyPositionInDirection);
        }
    }

    private static ( Position robotPosition, HashSet<Position> walls, HashSet<Position> boxes) ParseGrid(
        ReadOnlySpan<char> grid)
    {
        var width = grid.IndexOf("\n") + 1;

        var position = new Position();
        var wallPositions = new HashSet<Position>();
        var boxPositions = new HashSet<Position>();

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

        return (position, wallPositions, boxPositions);
    }
}