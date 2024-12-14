namespace Day4_CeresSearch;

public static class WordSearchXMasSolver
{
    public static int Solve(ReadOnlySpan<char> input)
    {
        var columnCount = input.IndexOf('\n') + 1;
        var firstPossibleXIndex = columnCount + 1;
        var lastPossibleXIndex = input.Length - columnCount - 1;

        var count = 0;
        for (var index = firstPossibleXIndex; index <= lastPossibleXIndex; ++index)
        {
            if (input[index] != 'A') continue;

            var topLeftXIndex = index - columnCount - 1;
            var bottomRightXIndex = index + columnCount + 1;
            var topRightXIndex = index - columnCount + 1;
            var bottomLeftXIndex = index + columnCount - 1;
            if ((input[topLeftXIndex] == 'S' && input[bottomRightXIndex] == 'M' ||
                 input[topLeftXIndex] == 'M' && input[bottomRightXIndex] == 'S') &&
                (input[topRightXIndex] == 'S' && input[bottomLeftXIndex] == 'M' ||
                 input[topRightXIndex] == 'M' && input[bottomLeftXIndex] == 'S'))
            {
                ++count;
            }
        }

        return count;
    }
}