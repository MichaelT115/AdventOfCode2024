namespace Day19_LinenLayout;

internal static class Program
{
    public static void Main(string[] args)
    {
        using var streamReader = new StreamReader(args[0]);

        var availablePatterns = streamReader.ReadLine()?.Split(", ") ?? [];

        var designsToPatternCount = new Dictionary<string, long>();

        var possibleDesignsCount = 0L;
        var possibleCombosCount = 0L;
        while (streamReader.Peek() >= 0)
        {
            var design = streamReader.ReadLine() ?? "";
            var possibleArrangementsCount =
                PossibleArrangementsThatMatchDesign(design, availablePatterns, designsToPatternCount);
            if (possibleArrangementsCount > 0)
            {
                ++possibleDesignsCount;
            }

            possibleCombosCount += possibleArrangementsCount;
        }

        Console.WriteLine($"Possible Designs Count: {possibleDesignsCount}");
        Console.WriteLine($"Possible Combos Count: {possibleCombosCount}");
    }

    private static long PossibleArrangementsThatMatchDesign(string design, string[] patterns,
        Dictionary<string, long> designsToPatternCount)
    {
        if (designsToPatternCount.TryGetValue(design, out var existingCount))
        {
            return existingCount;
        }

        var count = 0L;

        foreach (var pattern in patterns)
        {
            if (design == pattern)
            {
                count += 1;
                continue;
            }

            if (design.StartsWith(pattern))
            {
                count += PossibleArrangementsThatMatchDesign(design[pattern.Length..], patterns, designsToPatternCount);
            }
        }

        designsToPatternCount.Add(design, count);
        return count;
    }
}