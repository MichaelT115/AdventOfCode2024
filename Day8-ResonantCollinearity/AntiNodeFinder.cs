using Position = (int column, int row);

namespace Day8_ResonantCollinearity;

public static class AntiNodeFinder
{
    public static int FindAntiNodeCount(int gridWidth, int gridHeight, List<Position>[] antennaSets)
    {

        var antiNodePositions = new HashSet<Position>();
        foreach (var antennaSet in antennaSets)
        {
            for (var i = 0; i < antennaSet.Count; ++i)
            {
                for (var j = 0; j < antennaSet.Count; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var basePosition = antennaSet[i];
                    var otherPosition = antennaSet[j];

                    var columnDifference = otherPosition.column - basePosition.column;
                    var rowDifference = otherPosition.row - basePosition.row;

                    var antiNodePosition = new Position(otherPosition.column + columnDifference,
                        otherPosition.row + rowDifference);
                    if (IsInGrid(gridWidth, gridHeight, antiNodePosition))
                    {
                        antiNodePositions.Add(antiNodePosition);
                    }
                }
            }
        }

        return antiNodePositions.Count;
    }

    private static bool IsInGrid(int gridWidth, int gridHeight, Position position) =>
        0 <= position.column && position.column < gridWidth &&
        0 <= position.row && position.row < gridHeight;

    public static int FindAntiNodeCountWithResonance(int gridWidth, int gridHeight, List<Position>[] antennaSets)
    {
        var antiNodePositions = new HashSet<Position>();
        foreach (var antennaSet in antennaSets)
        {
            for (var i = 0; i < antennaSet.Count; ++i)
            {
                for (var j = 0; j < antennaSet.Count; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var basePosition = antennaSet[i];
                    var otherPosition = antennaSet[j];

                    var columnDifference = otherPosition.column - basePosition.column;
                    var rowDifference = otherPosition.row - basePosition.row;

                    var antiNodePosition = basePosition;
                    while (IsInGrid(gridWidth, gridHeight, antiNodePosition))
                    {
                        antiNodePositions.Add(antiNodePosition);
                        antiNodePosition = new Position(antiNodePosition.column + columnDifference,
                            antiNodePosition.row + rowDifference);
                    }
                }
            }
        }

        return antiNodePositions.Count;
    }
}