using Day1_HistorianHysteria.Part1;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Distance: {DistanceFinder.FindDistance(input)}");
Console.WriteLine($"Similarity: {SimilarityFinder.FindSimilarity(input)}");