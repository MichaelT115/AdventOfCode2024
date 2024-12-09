namespace Day1_HistorianHysteria.Part1;

public static class SimilarityFinder
{
    public static int FindSimilarity(ReadOnlySpan<char> input)
    {
        var (firstList, secondListCountsByValue) = ParseInput(input);

        var similarityScore = 0;

        foreach (var value in firstList)
        {
            if (secondListCountsByValue.TryGetValue(value, out var count))
            {
                similarityScore += value * count;
            }
        }

        return similarityScore;
    }

    private static (List<int> firstList, Dictionary<int, int> secondListCountsByValue) ParseInput(ReadOnlySpan<char> input)
    {
        var firstList = new List<int>();
        var secondListCountsByValue = new Dictionary<int, int>();
        foreach (var line in input.EnumerateLines())
        {
            var separatorIndex = line.IndexOf(' ');
            firstList.Add(int.Parse(line[..separatorIndex]));

            var secondValue = int.Parse(line[separatorIndex..]);
            if (!secondListCountsByValue.TryAdd(secondValue, 1))
            {
                ++secondListCountsByValue[secondValue];
            }
        }

        return (firstList, secondListCountsByValue);
    }
}