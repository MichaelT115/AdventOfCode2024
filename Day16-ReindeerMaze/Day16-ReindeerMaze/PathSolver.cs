namespace Day16_ReindeerMaze;

public static class PathSolver
{
    private  static readonly IntVector2[] Directions= [
        new IntVector2(1, 0),
        new IntVector2(0, 1),
        new IntVector2(-1, 0),
        new IntVector2(0, -1)
    ];

    private record struct IntVector2(int X, int Y)
    {
        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
            => new(a.X + b.X, a.Y + b.Y);
        
        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
            => new(a.X - b.X, a.Y - b.Y);
    }

    private record struct RobotState(IntVector2 Position, IntVector2 Direction);
    
    private record struct PathNode
    {
        public RobotState RobotState;
        public RobotState ComeFromRobotState;

        public int CheapestPathToNodeCost;
        public IntVector2 ComeFromDirection;
        public int EstimatedPathThatGoesThroughNodeCost;
    }

    public static long FindLowestScorePath(ReadOnlySpan<char> input)
    {
        var (startRobotState, exitPosition, walls) = ParseInput(input);

        var pathNodes = GeneratePathNodes(startRobotState, exitPosition, walls);
        
        return Directions
            .Select(direction => pathNodes.TryGetValue(new RobotState(exitPosition, direction), out var pathNode)
                ? pathNode.EstimatedPathThatGoesThroughNodeCost
                : int.MaxValue)
            .Min();
    }
    
    public static long FindPositionsAlongLowestScorePathsCount(ReadOnlySpan<char> input)
    {
        var (startRobotState, exitPosition, walls) = ParseInput(input);

        var pathNodes = GeneratePathNodes(startRobotState, exitPosition, walls);

        var cheapestPathCost = Directions
            .Select(direction => pathNodes.TryGetValue(new RobotState(exitPosition, direction), out var pathNode)
                ? pathNode.EstimatedPathThatGoesThroughNodeCost
                : int.MaxValue)
            .Min();

        var positionsAlongCheapestPath = new HashSet<IntVector2>();

        var endRobotStates = Directions
            .Select(direction => new RobotState(exitPosition, direction))
            .Where(state => pathNodes.TryGetValue(state, out var pathNode) &&
                            pathNode.CheapestPathToNodeCost == cheapestPathCost);
        foreach (var endRobotState in endRobotStates)
        {
            AddPositionsOnCheapestPath(endRobotState, pathNodes, positionsAlongCheapestPath);
        }
        
        return positionsAlongCheapestPath.Count;
    }

    private static void AddPositionsOnCheapestPath(RobotState robotState, Dictionary<RobotState, PathNode> pathNodes,
        HashSet<IntVector2> positionsAlongCheapestPath)
    {
        var cheapestCostToReachRobotState = pathNodes[robotState].CheapestPathToNodeCost;
        
        positionsAlongCheapestPath.Add(robotState.Position);
        
        var neighborsOnCheapestPath = Directions
            .SelectMany(offset =>
                Directions.Select(direction => new RobotState(robotState.Position + offset, direction)))
            .Where(neighborRobotState => pathNodes.TryGetValue(neighborRobotState, out var pathNode) &&
                                         pathNode.CheapestPathToNodeCost
                                         + DistanceCost(neighborRobotState, robotState.Position) == cheapestCostToReachRobotState);
        
        foreach (var neighborRobotState in neighborsOnCheapestPath)
        {
            AddPositionsOnCheapestPath(neighborRobotState, pathNodes, positionsAlongCheapestPath);
        }
    }

    private static Dictionary<RobotState, PathNode> GeneratePathNodes(RobotState startRobotState, IntVector2 exitPosition, HashSet<IntVector2> walls)
    {
        var pathNodes = new Dictionary<RobotState, PathNode>
        {
            {
                startRobotState, new PathNode
                {
                    RobotState = startRobotState,
                    ComeFromDirection = new IntVector2(1, 0),
                    CheapestPathToNodeCost = 0,
                    EstimatedPathThatGoesThroughNodeCost = 0
                }
            }
        };

        var openPositions = new List<RobotState> { startRobotState };
        var closedPositions = new HashSet<RobotState>();

        while (openPositions.Count > 0)
        {
            var lowestCostNode = GetLowestCostPathNode(pathNodes, openPositions);
            openPositions.Remove(lowestCostNode.RobotState);

            if (lowestCostNode.RobotState.Position == exitPosition)
            {
                continue;
            }

            closedPositions.Add(lowestCostNode.RobotState);

            foreach (var direction in Directions)
            {
                var potentialRobotState = new RobotState(lowestCostNode.RobotState.Position + direction, direction);

                if (walls.Contains(potentialRobotState.Position) || closedPositions.Contains(potentialRobotState))
                {
                    continue;
                }

                var tentativePathToNeighborNodeCost = lowestCostNode.CheapestPathToNodeCost +
                                                      DistanceCost(lowestCostNode.RobotState,
                                                          potentialRobotState.Position);
                var estimatedPathCost =
                    tentativePathToNeighborNodeCost + DistanceCost(potentialRobotState, exitPosition);

                if (pathNodes.TryGetValue(potentialRobotState, out var neighborNode))
                {
                    if (tentativePathToNeighborNodeCost >= neighborNode.CheapestPathToNodeCost)
                    {
                        continue;
                    }

                    pathNodes[potentialRobotState] = neighborNode with
                    {
                        ComeFromRobotState = lowestCostNode.RobotState,
                        CheapestPathToNodeCost = tentativePathToNeighborNodeCost,
                        EstimatedPathThatGoesThroughNodeCost = estimatedPathCost,
                    };

                    openPositions.Add(potentialRobotState);
                }
                else
                {
                    pathNodes.Add(potentialRobotState, new PathNode
                    {
                        RobotState = potentialRobotState,
                        ComeFromRobotState = lowestCostNode.RobotState,
                        CheapestPathToNodeCost = tentativePathToNeighborNodeCost,
                        EstimatedPathThatGoesThroughNodeCost = estimatedPathCost
                    });
                }

                if (!openPositions.Contains(potentialRobotState))
                {
                    openPositions.Add(potentialRobotState);
                }
            }
        }

        return pathNodes;
    }

    private static (RobotState robotStartState, IntVector2 exitPosition, HashSet<IntVector2> walls) ParseInput(ReadOnlySpan<char> input)
    {
        var lineEnumerator = input.EnumerateLines();

        var robotStartState = new RobotState();
        var exitPosition = new IntVector2();
        var walls = new HashSet<IntVector2>(input.Length);

        var y = 0;
        foreach (var line in lineEnumerator)
        {
            for (var x = 0; x < line.Length; ++x)
            {
                var character = line[x];
                switch (character)
                {
                    case '#':
                        walls.Add(new IntVector2(x, y));
                        break;
                    case 'S':
                        robotStartState = new RobotState(new IntVector2(x, y), new IntVector2(1, 0));
                        break;
                    case 'E':
                        exitPosition = new IntVector2(x, y);
                        break;
                }
            }

            ++y;
        }

        return (robotStartState, exitPosition, walls);
    }
    
    private static int DistanceCost(RobotState currentRobotState, IntVector2 neighborPosition)
    {
        if (currentRobotState.Position == neighborPosition)
        {
            return 0;
        }
        
        var offset = neighborPosition - currentRobotState.Position;
        var xDirection = Math.Sign(offset.X);
        var turns = currentRobotState.Direction.X == xDirection
            ? Math.Abs(Math.Sign(offset.Y) - currentRobotState.Direction.Y)
            : Math.Abs(xDirection - currentRobotState.Direction.X);
        var moves = Math.Abs(offset.X) + Math.Abs(offset.Y);

        return turns * 1000 + moves;
    }

    private static PathNode GetLowestCostPathNode(Dictionary<RobotState, PathNode> pathNodes, List<RobotState> openPositions)
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