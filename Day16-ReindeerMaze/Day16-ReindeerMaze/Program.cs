using Day16_ReindeerMaze;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Cheapest Path Cost: {PathSolver.FindCheapestPathCost(input)}");
Console.WriteLine($"Positions Along Cheapest Path(s) Count: {PathSolver.FindPositionsAlongCheapestPathsCount(input)}");