namespace Day1_HistorianHysteria.Part1;

internal static class Program
{
    public static void Main(string[] args)
    {
        using var streamReader = new StreamReader(args[0]);

        var input = streamReader.ReadToEnd();
        var distance = DistanceFinder.FindDistance(input);
        var similarity = SimilarityFinder.FindSimilarity(input);

        Console.WriteLine($"Distance: {distance}");
        Console.WriteLine($"Similarity: {similarity}");
    }
}