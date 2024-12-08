namespace Day6_GuardGallivant;

public static class InputParser
{
    public static ((int row, int column) startPosition, PositionType[][] grid) Parse(ReadOnlySpan<char> readOnlySpan)
    {
        var width = readOnlySpan.IndexOfAny("\r\n");

        (int row, int column) startPosition = (0, 0);
        var rowList = new List<PositionType[]>();
        foreach (var rowText in readOnlySpan.EnumerateLines())
        {
            var row = new PositionType[width];
            for (var index = 0; index < row.Length; index++)
            {
                if (rowText[index] == '#')
                {
                    row[index] = PositionType.Obstacle;
                }
                else
                {
                    if (rowText[index] == '^')
                    {
                        startPosition = (rowList.Count, index);
                    }

                    row[index] = PositionType.Empty;
                }
            }

            rowList.Add(row);
        }

        var positionTypes = rowList.ToArray();
        return (startPosition, positionTypes);
    }
}