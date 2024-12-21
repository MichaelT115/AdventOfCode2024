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

    public static int FindCheatPositions(ReadOnlySpan<char> input, int minimumTimeSaved, int maxCheatDistance)
    {
        var (walls, startPosition, endPosition, width, height) = ParseInput(input);
        var distanceFromEndByPosition = GetDistancesFromEnd(endPosition, startPosition, walls);

        var validCheatsCount = 0;
        foreach (var (position, positionDistanceFromEnd) in distanceFromEndByPosition.Reverse())
        {
            var minX = Math.Max(0, position.X - maxCheatDistance);
            var maxX = Math.Min(width - 1, position.X + maxCheatDistance);

            for (var xIndex = 0; xIndex <= maxX - minX; ++xIndex)
            {
                var x = minX + xIndex;
                var xDistance = Math.Abs(x - position.X);
                var yMaxDistance = maxCheatDistance - xDistance;

                var minY = Math.Max(0, position.Y - yMaxDistance);
                var maxY = Math.Min(height - 1, position.Y + yMaxDistance);

                for (var yIndex = 0; yIndex <= maxY - minY; yIndex++)
                {
                    var y = minY + yIndex;
                    var yDistance = Math.Abs(y - position.Y);

                    var cheatToPosition = new IntVector2(x, y);

                    if (!distanceFromEndByPosition.TryGetValue(cheatToPosition, out var cheatPositionDistanceFromEnd))
                    {
                        continue;
                    }

                    var distanceToCheatToPosition = xDistance + yDistance;
                    var distanceSaved = positionDistanceFromEnd -
                                        (cheatPositionDistanceFromEnd + distanceToCheatToPosition);
                    if (minimumTimeSaved <= distanceSaved)
                    {
                        ++validCheatsCount;
                    }
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

    private static (HashSet<IntVector2> walls, IntVector2 startPosition, IntVector2 endPosition, int width, int height)
        ParseInput(
            ReadOnlySpan<char> input)
    {
        var walls = new HashSet<IntVector2>();
        var startPosition = new IntVector2();
        var endPosition = new IntVector2();

        var width = input.IndexOf("\r\n");

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

        return (walls, startPosition, endPosition, width, y);
    }
}