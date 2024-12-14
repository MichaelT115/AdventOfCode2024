using Day12_GardenGroups;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Result: {RegionCoster.FindTotalCost(input)}");