using Day4_CeresSearch;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Result: {WordSearchSolver.Solve(input)}");