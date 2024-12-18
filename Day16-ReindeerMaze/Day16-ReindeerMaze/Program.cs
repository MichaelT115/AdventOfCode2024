using Day16_ReindeerMaze;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Result: {PathSolver.FindLowestScorePath(input)}");
Console.WriteLine($"Result: {PathSolver.FindPositionsAlongLowestScorePathsCount(input)}");