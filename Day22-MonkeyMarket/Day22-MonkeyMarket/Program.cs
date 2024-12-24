using Day22_MonkeyMarket;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

var sum = 0L;

foreach (var line in input.EnumerateLines())
{
    sum += MonkeyMarketPredictor.CalculateSecretNumber(long.Parse(line), 2000);
}

Console.WriteLine($"Result: {sum}");

var sequenceCounts = new Dictionary<(int first, int second, int third, int fourth), long>();

//FillOutProfitFromCommands(123, 9, sequenceCount);

foreach (var line in input.EnumerateLines())
{
    MonkeyMarketPredictor.FillOutProfitFromCommands(long.Parse(line), 2000, sequenceCounts);
}

Console.WriteLine($"Result (Part 2): {sequenceCounts.Values.Max()}");
