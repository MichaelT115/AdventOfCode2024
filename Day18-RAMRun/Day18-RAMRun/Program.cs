using Day18_RAMRun;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Shortest Path Length: {PathSolver.FindShortestPathLength(input)}");