using Position = (int column, int row);

namespace Day8_ResonantCollinearity;

public static class Parser
{
    public static (int gridWidth, int gridHeight, List<Position>[] antennaSets) ParseInput(ReadOnlySpan<char> input)
    {
        var gridWidth = input.IndexOfAny("\r\n");
        var row = 0;

        var antennaSetsByFrequency = new Dictionary<char, List<Position>>();

        foreach (var inputLine in input.EnumerateLines())
        {
            for (var column = 0; column < gridWidth; ++column)
            {
                var character = inputLine[column];
                if (character == '.') continue;
                if (antennaSetsByFrequency.TryGetValue(character, out var antennaList))
                {
                    antennaList.Add((column, row));
                }
                else
                {
                    antennaSetsByFrequency[character] = [(column, row)];
                }
            }

            ++row;
        }

        return (gridWidth, row, antennaSetsByFrequency.Values.ToArray());
    }
}