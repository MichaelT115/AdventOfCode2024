namespace Day16_ReindeerMaze;

public static class PathSolver
{
    private  static readonly Position[] Directions= [
        new Position(1, 0),
        new Position(0, 1),
        new Position(-1, 0),
        new Position(0, -1)
    ];

    private record struct Position(int X, int Y)
    {
        public static Position operator +(Position a, Position b)
            => new(a.X + b.X, a.Y + b.Y);
        
        public static Position operator -(Position a, Position b)
            => new(a.X - b.X, a.Y - b.Y);
    }
    
    private record struct PathNode
    {
        public Position Position;
        public Position ComeFromPosition;

        public int CheapestPathToNodeCost;
        public Position ComeFromDirection;
        public int EstimatedPathThatGoesThroughNodeCost;
    }
    
    public static long FindLowestScorePath(ReadOnlySpan<char> input)
    {
        var (robotStartPosition, exitPosition, walls) = ParseInput(input);

        var pathNodes = new Dictionary<Position, PathNode>
        {
            {
                robotStartPosition, new PathNode
                {
                    Position = robotStartPosition,
                    ComeFromDirection = new Position(1, 0),
                    ComeFromPosition = new Position(-1, -1),
                    CheapestPathToNodeCost = 0,
                    EstimatedPathThatGoesThroughNodeCost = 0
                }
            },
            {
                exitPosition, new PathNode
                {
                    Position = exitPosition,
                    ComeFromPosition = new Position(-1, -1),
                    CheapestPathToNodeCost = int.MaxValue,
                }
            }
        };

        var openPositions = new List<Position> { robotStartPosition };
        var closedPositions = new HashSet<Position>();

        while (openPositions.Count > 0)
        {
            var lowestCostNode = GetLowestCostPosition(pathNodes, openPositions);

            if (lowestCostNode.Position == exitPosition)
            {
                break;
            }
            
            var lowestCostNodePosition = lowestCostNode.Position;

            openPositions.Remove(lowestCostNodePosition);
            
            closedPositions.Add(lowestCostNodePosition);
            
            foreach (var direction in Directions)
            {
                var neighborPosition = lowestCostNodePosition + direction;

                if (walls.Contains(neighborPosition) || closedPositions.Contains(neighborPosition))
                {
                    continue;
                }
                
                var tentativePathToNeighborNodeCost = lowestCostNode.CheapestPathToNodeCost +
                                                      DistanceCost(lowestCostNodePosition,  lowestCostNode.ComeFromDirection, neighborPosition);
                var estimatedPathCost =
                    tentativePathToNeighborNodeCost + DistanceCost(neighborPosition, direction, exitPosition);

                if (pathNodes.TryGetValue(neighborPosition, out var neighborNode))
                {
                    if (tentativePathToNeighborNodeCost >= neighborNode.CheapestPathToNodeCost)
                    {
                        continue;
                    }

                    pathNodes[neighborPosition] = neighborNode with
                    {
                        ComeFromPosition = lowestCostNodePosition,
                        ComeFromDirection = direction, 
                        CheapestPathToNodeCost = tentativePathToNeighborNodeCost,
                        EstimatedPathThatGoesThroughNodeCost = estimatedPathCost,
                    };

                    openPositions.Add(neighborPosition);
                }
                else
                {
                    pathNodes.Add(neighborPosition, new PathNode
                    {
                        Position = neighborPosition,
                        ComeFromPosition = lowestCostNodePosition,
                        ComeFromDirection = direction, 
                        CheapestPathToNodeCost = tentativePathToNeighborNodeCost,
                        EstimatedPathThatGoesThroughNodeCost = estimatedPathCost
                    });
                }
                
                if (!openPositions.Contains(neighborPosition))
                {
                    openPositions.Add(neighborPosition);
                }

                foreach (var (position, pathNode) in pathNodes)
                {
                    if (position != pathNode.Position)
                    {
                        throw new Exception();
                    }
                }
            }
        }

        return pathNodes[exitPosition].CheapestPathToNodeCost;
    }

    private static (Position robotStartPosition, Position exitPosition, HashSet<Position> walls) ParseInput(ReadOnlySpan<char> input)
    {
        var lineEnumerator = input.EnumerateLines();

        var robotStartPosition = new Position();
        var exitPosition = new Position();
        var walls = new HashSet<Position>(input.Length);

        var y = 0;
        foreach (var line in lineEnumerator)
        {
            for (var x = 0; x < line.Length; ++x)
            {
                var character = line[x];
                switch (character)
                {
                    case '#':
                        walls.Add(new Position(x, y));
                        break;
                    case 'S':
                        robotStartPosition = new Position(x, y);
                        break;
                    case 'E':
                        exitPosition = new Position(x, y);
                        break;
                }
            }

            ++y;
        }

        return (robotStartPosition, exitPosition, walls);
    }


    private static int DistanceCost(Position currentPosition, Position currentDirection, Position neighborPosition)
    {
        var offset = neighborPosition - currentPosition;
        var xDirection = Math.Sign(offset.X);
        var turns = currentDirection.X == xDirection
            ? Math.Abs(Math.Sign(offset.Y) - currentDirection.Y)
            : Math.Abs(xDirection - currentDirection.X);

        var moves = Math.Abs(offset.X) + Math.Abs(offset.Y);


        return turns * 1000 + moves;
    }

    private static PathNode GetLowestCostPosition(Dictionary<Position, PathNode> pathNodes, List<Position> openPositions)
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