using Day11_PlutonianPebbles;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var stones = input.Split(' ').Select(ulong.Parse).ToArray();

Console.WriteLine($"Result (25): {StoneCounter.CountStones(stones, 25)}");

Console.WriteLine($"Result (75): {StoneCounter.CountStones(stones, 75)}");
