using System.Text.RegularExpressions;

namespace Day14_RestroomRedoubt;

internal partial class Program
{
    public static void Main(string[] args)
    {
        using var streamReader = new StreamReader(args[0]);

        const int ticks = 100;
        const int width = 101;
        const int height = 103;

        var quadrantOneCount = 0;
        var quadrantTwoCount = 0;
        var quadrantThreeCount = 0;
        var quadrantFourCount = 0;
        
        while (streamReader.Peek() > 0)
        {
            var robotInput = streamReader.ReadLine();

            var matches = ParsingRegex().Matches(robotInput!);

            var positionX = int.Parse(matches[0].Groups[1].ValueSpan);
            var positionY = int.Parse(matches[0].Groups[2].ValueSpan);
            var velocityX = int.Parse(matches[1].Groups[1].ValueSpan);
            var velocityY = int.Parse(matches[1].Groups[2].ValueSpan);
            
            switch (CalculateFinalPosition(positionX, positionY, velocityX, velocityY, width, height, ticks))
            {
                case (< width / 2, < height / 2):
                    ++quadrantOneCount;
                    break;
                case (> width / 2, < height / 2):
                    ++quadrantTwoCount;
                    break;
                case (< width / 2, > height / 2):
                    ++quadrantThreeCount;
                    break;
                case (> width / 2, > height / 2):
                    ++quadrantFourCount;
                    break;
            }
        }

        Console.WriteLine($"Result: {quadrantOneCount * quadrantTwoCount * quadrantThreeCount * quadrantFourCount}");
    }

    public static (int finalPositionX, int finalPositionY) CalculateFinalPosition(int positionX, int positionY,
        int velocityX, int velocityY, int width, int height, int ticks) =>
        (((positionX + velocityX * ticks) % width + width) % width,
            ((positionY + velocityY * ticks) % height + height) % height);

    [GeneratedRegex(@"((?:\+|-)?\d+),((?:\+|-)?\d+)")]
    private static partial Regex ParsingRegex();
}