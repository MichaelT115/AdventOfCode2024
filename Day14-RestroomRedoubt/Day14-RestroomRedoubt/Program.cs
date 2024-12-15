using System.Text.RegularExpressions;

namespace Day14_RestroomRedoubt;

internal partial class Program
{
    private const int Ticks = 100;
    private const int Width = 101;
    private const int Height = 103;
    
    public static void Main(string[] args)
    {
        using var streamReader = new StreamReader(args[0]);

        var robots = new List<(int startPositionX, int startPositionY, int velocityX, int velocityY)>();

        while (streamReader.Peek() > 0)
        {
            var robotInput = streamReader.ReadLine();

            var matches = ParsingRegex().Matches(robotInput!);

            robots.Add((int.Parse(matches[0].Groups[1].ValueSpan),
                int.Parse(matches[0].Groups[2].ValueSpan),
                int.Parse(matches[1].Groups[1].ValueSpan),
                int.Parse(matches[1].Groups[2].ValueSpan)));
        }

        var quadrantOneCount = 0;
        var quadrantTwoCount = 0;
        var quadrantThreeCount = 0;
        var quadrantFourCount = 0;
        foreach (var (startPositionX, startPositionY, velocityX, velocityY) in robots)
        {
            switch (CalculateFinalPosition(startPositionX, startPositionY, velocityX, velocityY, Width, Height, Ticks))
            {
                case (< Width / 2, < Height / 2):
                    ++quadrantOneCount;
                    break;
                case (> Width / 2, < Height / 2):
                    ++quadrantTwoCount;
                    break;
                case (< Width / 2, > Height / 2):
                    ++quadrantThreeCount;
                    break;
                case (> Width / 2, > Height / 2):
                    ++quadrantFourCount;
                    break;
            }
        }

        Console.WriteLine($"Result: {quadrantOneCount * quadrantTwoCount * quadrantThreeCount * quadrantFourCount}");
        

        PrintGridWhenMostRobotsAreInTheMiddleOfTheGrid(robots, Width, Height);
    }

    private static void PrintGridWhenMostRobotsAreInTheMiddleOfTheGrid(List<(int startPositionX, int startPositionY, int velocityX, int velocityY)> robots, int width, int height)
    {
        for (var seconds = 1; seconds < 10403; ++seconds)
        {
            var positions = new HashSet<(int x, int y)>();

            var inCenterCount = 0;
            foreach (var (startPositionX, startPositionY, velocityX, velocityY) in robots)
            {
                var finalPosition = CalculateFinalPosition(startPositionX, startPositionY, velocityX, velocityY, width,
                    height, seconds);
                positions.Add(finalPosition);

                if (finalPosition.finalPositionX is > Width / 4 and < Width / 4 * 3 &&
                    finalPosition.finalPositionY is > Height / 4 and < Height / 4 * 3)
                {
                    ++inCenterCount;
                }
            }

            if (inCenterCount <= positions.Count / 2) continue;
            Console.WriteLine($"Seconds: {seconds}");
            PrintGrid(height, width, positions);
        }
    }

    private static void PrintGrid(int height, int width, HashSet<(int x, int y)> positions)
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; ++x)
            {
                Console.Write(positions.Contains((x, y)) ? 'X' : ' ');
            }
            Console.WriteLine();
        }
    }

    public static (int finalPositionX, int finalPositionY) CalculateFinalPosition(int positionX, int positionY,
        int velocityX, int velocityY, int width, int height, int ticks) =>
        (((positionX + velocityX * ticks) % width + width) % width,
            ((positionY + velocityY * ticks) % height + height) % height);

    [GeneratedRegex(@"((?:\+|-)?\d+),((?:\+|-)?\d+)")]
    private static partial Regex ParsingRegex();
}