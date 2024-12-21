using Day20_RaceCondition;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Result: {CheatFinder.FindCheatPositions(input, 100)}");