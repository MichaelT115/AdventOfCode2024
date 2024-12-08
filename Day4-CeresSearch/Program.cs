using Day4_CeresSearch;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Result (XMAS): {WordSearchSolver.Solve(input)}");
Console.WriteLine($"Result (X-MAS): {WordSearchXMasSolver.Solve(input)}");