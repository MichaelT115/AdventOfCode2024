namespace Day18_RAMRun;

public static class PathSolver
{
    private record struct IntVector2(int X, int Y)
    {
        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
            => new(a.X + b.X, a.Y + b.Y);

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
            => new(a.X - b.X, a.Y - b.Y);
    }
    
    private record struct PathNode(
        IntVector2 Position,
        int CheapestPathToNodeCost,
        int EstimatedPathThatGoesThroughNodeCost);
    
    private static readonly IntVector2[] Directions =
    [
        new IntVector2(1, 0),
        new IntVector2(0, 1),
        new IntVector2(-1, 0),
        new IntVector2(0, -1)
    ];

    public static long FindShortestPathLength(ReadOnlySpan<char> input)
    {
        var obstacles= ParseInput(input, 1024);

        var startPosition = new IntVector2(0, 0);
        var exitPosition = new IntVector2(70, 70);
        var pathNodes = GeneratePathNodes(startPosition, exitPosition, obstacles);

        return pathNodes[exitPosition].CheapestPathToNodeCost;
    }
    private static Dictionary<IntVector2, PathNode> GeneratePathNodes(IntVector2 startPosition,
        IntVector2 exitPosition, HashSet<IntVector2> walls)
    {
        var pathNodes = new Dictionary<IntVector2, PathNode>
        {
            {
                startPosition, new PathNode
                {
                    Position = startPosition,
                    CheapestPathToNodeCost = 0,
                    EstimatedPathThatGoesThroughNodeCost = 0
                }
            }
        };

        var openPositions = new List<IntVector2> { startPosition };
        var closedPositions = new HashSet<IntVector2>();

        while (openPositions.Count > 0)
        {
            var lowestCostNode = GetLowestCostPathNode(pathNodes, openPositions);
            openPositions.Remove(lowestCostNode.Position);

            if (lowestCostNode.Position == exitPosition)
            {
                break;
            }

            closedPositions.Add(lowestCostNode.Position);

            foreach (var direction in Directions)
            {
                var neighbor = lowestCostNode.Position + direction;

                if (neighbor.X is < 0 or > 70 || neighbor.Y is < 0 or > 70 || walls.Contains(neighbor) || closedPositions.Contains(neighbor))
                {
                    continue;
                }

                var tentativePathToNeighborNodeCost = lowestCostNode.CheapestPathToNodeCost +
                                                      DistanceCost(lowestCostNode.Position, neighbor);
                var estimatedPathCost =
                    tentativePathToNeighborNodeCost + DistanceCost(neighbor, exitPosition);

                if (pathNodes.TryGetValue(neighbor, out var neighborNode))
                {
                    if (tentativePathToNeighborNodeCost >= neighborNode.CheapestPathToNodeCost)
                    {
                        continue;
                    }

                    pathNodes[neighbor] = neighborNode with
                    {
                        CheapestPathToNodeCost = tentativePathToNeighborNodeCost,
                        EstimatedPathThatGoesThroughNodeCost = estimatedPathCost
                    };
                }
                else
                {
                    pathNodes.Add(neighbor, new PathNode
                    {
                        Position = neighbor,
                        CheapestPathToNodeCost = tentativePathToNeighborNodeCost,
                        EstimatedPathThatGoesThroughNodeCost = estimatedPathCost
                    });
                }

                if (!openPositions.Contains(neighbor))
                {
                    openPositions.Add(neighbor);
                }
            }
        }

        return pathNodes;
    }

    private static HashSet<IntVector2> ParseInput(ReadOnlySpan<char> input, int maxReadCount)
    {
        var obstacles = new HashSet<IntVector2>(input.Length);

        var index = 0;
        foreach (var line in input.EnumerateLines())
        {
            if (index++ > maxReadCount)
            {
                break;
            }
             
            var commaIndex = line.IndexOf(',');
            obstacles.Add(new IntVector2(int.Parse(line[..commaIndex]), int.Parse(line[(commaIndex + 1)..])));
        }
        
        return obstacles;
    }

    private static int DistanceCost(IntVector2 currentPosition, IntVector2 neighborPosition)
    {
        if (currentPosition == neighborPosition)
        {
            return 0;
        }

        var offset = neighborPosition - currentPosition;
        return Math.Abs(offset.X) + Math.Abs(offset.Y);
    }

    private static PathNode GetLowestCostPathNode(Dictionary<IntVector2, PathNode> pathNodes,
        List<IntVector2> openPositions)
    {
        var lowestCostNode = pathNodes[openPositions[0]];

        for (var index = 1; index < openPositions.Count; index++)
        {
            var openPosition = openPositions[index];
            var pathNode = pathNodes[openPosition];
            if (pathNode.EstimatedPathThatGoesThroughNodeCost <
                lowestCostNode.EstimatedPathThatGoesThroughNodeCost)
            {
                lowestCostNode = pathNode;
            }
        }

        return lowestCostNode;
    }
}