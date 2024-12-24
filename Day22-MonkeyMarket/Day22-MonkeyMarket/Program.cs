using Day22_MonkeyMarket;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

var sum = 0L;

foreach (var line in input.EnumerateLines())
{
    sum += MonkeyMarketPredictor.CalculateSecretNumber(long.Parse(line), 2000);
}

Console.WriteLine($"Result: {sum}");