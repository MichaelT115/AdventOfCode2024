using Day13_ClawContraption;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();
var lines = input.Split('\n');

var tokenCountPart1 = 0L;
var tokenCountPart2 = 0L;

for (var i = 0; i < lines.Length; i += 4)
{
    var buttonALine = lines[i].AsSpan();
    var buttonA =
        new TokenCalculator.Vector2(ParseNumber(buttonALine, buttonALine.IndexOf('+') + 1),
            ParseNumber(buttonALine, buttonALine.LastIndexOf('+') + 1));
    var buttonBLine = lines[i + 1].AsSpan();
    var buttonB =
        new TokenCalculator.Vector2(ParseNumber(buttonBLine, buttonBLine.IndexOf('+') + 1),
            ParseNumber(buttonBLine, buttonBLine.LastIndexOf('+') + 1));
    var prizeLine = lines[i + 2].AsSpan();
    var prize =
        new TokenCalculator.Vector2(ParseNumber(prizeLine, prizeLine.IndexOf('=') + 1),
            ParseNumber(prizeLine, prizeLine.LastIndexOf('=') + 1));

    if (TokenCalculator.FindLowestNeededTokens(buttonA, buttonB, prize, 100, out var lowestNeededTokens1))
    {
        tokenCountPart1 += lowestNeededTokens1;
    }

    if (TokenCalculator.FindLowestNeededTokens(buttonA, buttonB, prize, long.MaxValue, out var lowestNeededTokens2,
            true))
    {
        tokenCountPart2 += lowestNeededTokens2;
    }
}

Console.WriteLine($"Result Part 1: {tokenCountPart1}");
Console.WriteLine($"Result Part 2: {tokenCountPart2}");

return;

int ParseNumber(ReadOnlySpan<char> line, int numberStart)
{
    int length;
    for (length = 0; length < line.Length - numberStart; ++length)
    {
        if (!char.IsDigit(line[numberStart + length]))
        {
            break;
        }
    }

    return int.Parse(line.Slice(numberStart, length));
}