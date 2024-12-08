using Day6_GuardGallivant;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

var (startPosition, grid) = InputParser.Parse(input);

Console.WriteLine($"Path Positions Count: {PathPredictor.GetPredictedUniqueGuardPathPositionsCount(grid, startPosition)}");
Console.WriteLine($"Possible Obstruction Position Count: {PathPredictor.GetPossibleObstructionPlacementsCount(grid, startPosition)}");