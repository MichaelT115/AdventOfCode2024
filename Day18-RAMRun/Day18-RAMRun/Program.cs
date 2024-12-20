using Day18_RAMRun;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Shortest Path Length: {PathSolver.FindShortestPathLength(input)}");
Console.WriteLine($"Obstacle That Makes Path Impossible: {PathSolver.FindObstacleThatMakesPathImpossible(input)}"); 