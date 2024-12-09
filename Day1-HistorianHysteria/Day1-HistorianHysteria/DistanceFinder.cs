namespace Day1_HistorianHysteria.Part1;

public static class DistanceFinder
{
    public static int FindDistance(ReadOnlySpan<char> input)
    {
        var (firstList, secondList) = ParseInput(input);

        firstList.Sort();
        secondList.Sort();

        return firstList.Select((value, index) => Math.Abs(value - secondList[index])).Sum();
    }

    private static (List<int> firstList, List<int> secondList) ParseInput(ReadOnlySpan<char> input)
    {
        var firstList = new List<int>();
        var secondList = new List<int>();
        foreach (var line in input.EnumerateLines())
        {
            var separatorIndex = line.IndexOf(' ');
            firstList.Add(int.Parse(line[..separatorIndex]));
            secondList.Add(int.Parse(line[separatorIndex..]));
        }

        return (firstList, secondList);
    }
}