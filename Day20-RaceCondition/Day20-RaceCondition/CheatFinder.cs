namespace Day20_RaceCondition;

public static class CheatFinder
{
    private record struct IntVector2(int X, int Y)
    {
        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
            => new(a.X + b.X, a.Y + b.Y);

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
            => new(a.X - b.X, a.Y - b.Y);
    }

    private static readonly IntVector2[] Directions =
    [
        new IntVector2(1, 0),
        new IntVector2(0, 1),
        new IntVector2(-1, 0),
        new IntVector2(0, -1)
    ];

    public static int FindCheatPositions(ReadOnlySpan<char> input, int cheatLimit)
    {
        var (walls, startPosition, endPosition) = ParseInput(input);
        var distanceFromEndByPosition = GetDistancesFromEnd(endPosition, startPosition, walls);

        var validCheatsCount = 0;
        foreach (var (position, positionDistanceFromEnd) in distanceFromEndByPosition)
        {
            foreach (var direction in Directions)
            {
                if (!walls.Contains(position + direction))
                {
                    continue;
                }

                var cheatToPosition = position + direction + direction;

                if (!distanceFromEndByPosition.TryGetValue(cheatToPosition, out var cheatPositionDistanceFromEnd))
                {
                    continue;
                }

                var distanceSaved = positionDistanceFromEnd - (cheatPositionDistanceFromEnd + 1);
                if (cheatLimit <= distanceSaved)
                {
                    ++validCheatsCount;
                }
            }
        }

        return validCheatsCount;
    }

    private static Dictionary<IntVector2, int> GetDistancesFromEnd(IntVector2 endPosition, IntVector2 startPosition,
        HashSet<IntVector2> walls)
    {
        var distanceFromEndByPosition = new Dictionary<IntVector2, int>();
        var distanceFromEnd = 0;
        var previousPosition = new IntVector2();
        var currentPosition = endPosition;
        while (currentPosition != startPosition)
        {
            distanceFromEndByPosition.Add(currentPosition, distanceFromEnd++);

            foreach (var direction in Directions)
            {
                var neighborPosition = currentPosition + direction;
                if (neighborPosition == previousPosition || walls.Contains(neighborPosition))
                {
                    continue;
                }

                previousPosition = currentPosition;
                currentPosition = neighborPosition;
                break;
            }
        }

        distanceFromEndByPosition.Add(startPosition, distanceFromEnd);
        return distanceFromEndByPosition;
    }

    private static (HashSet<IntVector2> walls, IntVector2 startPosition, IntVector2 endPosition) ParseInput(
        ReadOnlySpan<char> input)
    {
        var walls = new HashSet<IntVector2>();
        var startPosition = new IntVector2();
        var endPosition = new IntVector2();

        var y = 0;
        foreach (var line in input.EnumerateLines())
        {
            for (var x = 0; x < line.Length; x++)
            {
                switch (line[x])
                {
                    case '#':
                        walls.Add(new IntVector2(x, y));
                        break;
                    case 'S':
                        startPosition = new IntVector2(x, y);
                        break;
                    case 'E':
                        endPosition = new IntVector2(x, y);
                        break;
                }
            }

            ++y;
        }

        return (walls, startPosition, endPosition);
    }
}