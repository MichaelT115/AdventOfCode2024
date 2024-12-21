using Day20_RaceCondition;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

Console.WriteLine($"Result (Max Cheat Time 2): {CheatFinder.FindCheatPositions(input, 100, 2)}");
Console.WriteLine($"Result (Max Cheat Time 20): {CheatFinder.FindCheatPositions(input, 100, 20)}");