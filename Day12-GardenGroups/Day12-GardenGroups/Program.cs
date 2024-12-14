using Day12_GardenGroups;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var regions = RegionFinder.GetRegions(input);

Console.WriteLine($"Result (w/ perimeter): {regions.Sum(region => region.Area * region.Perimeter)}");
Console.WriteLine($"Result (w/ sides): {regions.Sum(region => region.Area * region.Corners)}");