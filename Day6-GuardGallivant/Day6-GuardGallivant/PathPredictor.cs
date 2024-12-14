namespace Day6_GuardGallivant;

public static class PathPredictor
{
    private enum Direction : byte
    {
        North,
        East,
        South,
        West,

        Count
    }

    public static int GetPredictedUniqueGuardPathPositionsCount(PositionType[][] grid,
        (int row, int column) guardStartPrediction) =>
        GetPredictedUniqueGuardPathPositions(grid, guardStartPrediction).Count;

    private static HashSet<(int row, int column)> GetPredictedUniqueGuardPathPositions(PositionType[][] grid,
        (int row, int column) guardStartPosition)
    {
        var positionsHashSet = new HashSet<(int, int)>();
        var currentDirection = Direction.North;
        var currentPosition = guardStartPosition;

        while (IsPositionInGrid(currentPosition, grid))
        {
            positionsHashSet.Add(currentPosition);

            var nextPosition = currentDirection switch
            {
                Direction.North => currentPosition with { row = currentPosition.row - 1 },
                Direction.East => currentPosition with { column = currentPosition.column + 1 },
                Direction.South => currentPosition with { row = currentPosition.row + 1 },
                Direction.West => currentPosition with { column = currentPosition.column - 1 },
                _ => throw new ArgumentOutOfRangeException()
            };

            if (!IsPositionInGrid(nextPosition, grid))
            {
                break;
            }

            if (grid[nextPosition.row][nextPosition.column] == PositionType.Empty)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentDirection = (Direction)((byte)(currentDirection + 1) % (byte)Direction.Count);
            }
        }

        return positionsHashSet;
    }

    public static int GetPossibleObstructionPlacementsCount(PositionType[][] grid,
        (int row, int column) guardStartPosition)
    {
        var positionsToCheck = GetPredictedUniqueGuardPathPositions(grid, guardStartPosition);
        positionsToCheck.Remove(guardStartPosition);

        var placedObstructionPositionCount = 0;

        foreach (var position in positionsToCheck)
        {
            grid[position.row][position.column] = PositionType.Obstacle;
            if (IsPredictingLoopedPath(grid, guardStartPosition))
            {
                ++placedObstructionPositionCount;
            }

            grid[position.row][position.column] = PositionType.Empty;
        }

        return placedObstructionPositionCount;
    }

    private static bool IsPredictingLoopedPath(PositionType[][] grid, (int row, int column) guardStartPosition)
    {
        var positionsHashSet = new HashSet<((int, int), Direction)>();
        var currentDirection = Direction.North;
        var currentPosition = guardStartPosition;

        while (IsPositionInGrid(currentPosition, grid))
        {
            if (!positionsHashSet.Add((currentPosition, currentDirection)))
            {
                return true;
            }

            var nextPosition = currentDirection switch
            {
                Direction.North => currentPosition with { row = currentPosition.row - 1 },
                Direction.East => currentPosition with { column = currentPosition.column + 1 },
                Direction.South => currentPosition with { row = currentPosition.row + 1 },
                Direction.West => currentPosition with { column = currentPosition.column - 1 },
                _ => throw new ArgumentOutOfRangeException()
            };

            if (!IsPositionInGrid(nextPosition, grid))
            {
                break;
            }

            if (grid[nextPosition.row][nextPosition.column] == PositionType.Empty)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentDirection = (Direction)((byte)(currentDirection + 1) % (byte)Direction.Count);
            }
        }

        return false;
    }

    private static bool IsPositionInGrid((int row, int column) position, PositionType[][] grid) =>
        position.row >= 0 && position.row < grid.Length && position.column >= 0 &&
        position.column < grid[0].Length;
}