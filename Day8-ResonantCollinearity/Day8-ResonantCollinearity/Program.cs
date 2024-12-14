using Day8_ResonantCollinearity;

using var streamReader = new StreamReader(args[0]);

var parsedInput = Parser.ParseInput(streamReader.ReadToEnd());

Console.WriteLine($"Result: {AntiNodeFinder.FindAntiNodeCount(parsedInput.gridWidth, parsedInput.gridHeight,
    parsedInput.antennaSets)}");
Console.WriteLine($"Result (w/ resonance): {AntiNodeFinder.FindAntiNodeCountWithResonance(parsedInput.gridWidth, parsedInput.gridHeight,
    parsedInput.antennaSets)}");