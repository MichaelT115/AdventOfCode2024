namespace Day6_GuardGallivant;

public static class PathPredictor
{
    private enum Direction
    {
        North,
        East,
        South,
        West,

        Count
    }

    public static int PredictedUniquePathPositionsCount(PositionType[][] grid,
        (int row, int column) guardStartPrediction) =>
        GetPredictedGuardPathUniquePositions(grid, guardStartPrediction).Count;

    private static HashSet<(int row, int column)> GetPredictedGuardPathUniquePositions(PositionType[][] grid,
        (int row, int column) guardStartPosition)
    {
        var positionsHashSet = new HashSet<(int, int)>();
        var currentDirection = Direction.North;
        var currentPosition = guardStartPosition;

        while (IsInGrid(currentPosition, grid))
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

            if (!IsInGrid(nextPosition, grid))
            {
                break;
            }

            if (grid[nextPosition.row][nextPosition.column] == PositionType.Empty)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentDirection = (Direction)((ushort)(currentDirection + 1) % (ushort)Direction.Count);
            }
        }

        return positionsHashSet;
    }

    public static int PathObstructionPredictor(PositionType[][] grid, (int row, int column) guardStartPosition)
    {
        var positionsToCheck = GetPredictedGuardPathUniquePositions(grid, guardStartPosition);
        positionsToCheck.Remove(guardStartPosition);

        var placedObstructionPositionCount = 0;

        foreach (var position in positionsToCheck)
        {
            grid[position.row][position.column] = PositionType.Obstacle;
            if (GuardIsInLoop(grid, guardStartPosition))
            {
                ++placedObstructionPositionCount;
            }
            grid[position.row][position.column] = PositionType.Empty;
        }

        return placedObstructionPositionCount;
    }

    private static bool GuardIsInLoop(PositionType[][] grid, (int row, int column) guardStartPosition)
    {
        var positionsHashSet = new HashSet<((int, int), Direction)>();
        var currentDirection = Direction.North;
        var currentPosition = guardStartPosition;

        while (IsInGrid(currentPosition, grid))
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

            if (!IsInGrid(nextPosition, grid))
            {
                break;
            }

            if (grid[nextPosition.row][nextPosition.column] == PositionType.Empty)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentDirection = (Direction)((ushort)(currentDirection + 1) % (ushort)Direction.Count);
            }
        }

        return false;
    }

    private static bool IsInGrid((int row, int column) position, PositionType[][] grid) =>
        position.row >= 0 && position.row < grid.Length && position.column >= 0 &&
        position.column < grid[0].Length;
}