using Day10_HoofIt;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var trailHeadScoreCount = TrailHeadFinder.CountTrailHeadScores(input);
var trailHeadQuality = TrailHeadFinder.CountTrailHeadQuality(input);

Console.WriteLine($"Result (Score): {trailHeadScoreCount}");
Console.WriteLine($"Result (Quality): {trailHeadQuality}");