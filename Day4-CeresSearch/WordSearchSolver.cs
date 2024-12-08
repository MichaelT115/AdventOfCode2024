namespace Day4_CeresSearch;

public static class WordSearchSolver
{
    public static int Solve(ReadOnlySpan<char> input)
    {
        var firstLineBreakIndex = input.IndexOf('\n');
        var columnCount = firstLineBreakIndex != -1 ? firstLineBreakIndex + 1 : input.Length;

        var count = 0;
        for (var index = 0; index < input.Length; ++index)
        {
            if (input[index] != 'X') continue;

            if (CheckVerticalDown(input, index, columnCount))
            {
                ++count;
            }

            if (CheckVerticalUp(input, index, columnCount))
            {
                ++count;
            }

            if (CheckDiagonalForwardDown(input, index, columnCount))
            {
                ++count;
            }

            if (CheckDiagonalForwardUp(input, index, columnCount))
            {
                ++count;
            }

            if (CheckDiagonalBackwardsUp(input, index, columnCount))
            {
                ++count;
            }

            if (CheckDiagonalBackwardsDown(input, index, columnCount))
            {
                ++count;
            }

            if (CheckHorizontalBackwards(input, index))
            {
                ++count;
            }

            if (!CheckHorizontalForwards(input, index)) continue;

            ++count;
            index += 3;
        }

        return count;
    }

    private static bool CheckHorizontalForwards(ReadOnlySpan<char> input, int index) =>
        input.Length - index >= 4 && input.Slice(index, 4) is "XMAS";

    private static bool CheckHorizontalBackwards(ReadOnlySpan<char> input, int index) =>
        index >= 3 && input.Slice(index - 3, 4) is "SAMX";

    private static bool CheckDiagonalBackwardsDown(ReadOnlySpan<char> input, int index, int columnCount) =>
        index + columnCount * 3 - 3 < input.Length && input[index + columnCount - 1] == 'M' &&
        input[index + columnCount * 2 - 2] == 'A' && input[index + columnCount * 3 - 3] == 'S';

    private static bool CheckDiagonalBackwardsUp(ReadOnlySpan<char> input, int index, int columnCount) =>
        columnCount * 3 + 3 <= index && input[index - (columnCount + 1)] == 'M' &&
        input[index - (columnCount * 2 + 2)] == 'A' && input[index - (columnCount * 3 + 3)] == 'S';

    private static bool CheckDiagonalForwardUp(ReadOnlySpan<char> input, int index, int columnCount) =>
        columnCount * 3 - 3 <= index && input[index - (columnCount - 1)] == 'M' &&
        input[index - (columnCount * 2 - 2)] == 'A' && input[index - (columnCount * 3 - 3)] == 'S';

    private static bool CheckDiagonalForwardDown(ReadOnlySpan<char> input, int index, int columnCount) =>
        index + columnCount * 3 + 3 < input.Length && input[index + columnCount + 1] == 'M' &&
        input[index + columnCount * 2 + 2] == 'A' && input[index + columnCount * 3 + 3] == 'S';

    private static bool CheckVerticalUp(ReadOnlySpan<char> input, int index, int columnCount) =>
        columnCount * 3 <= index && input[index - columnCount] == 'M' &&
        input[index - columnCount * 2] == 'A' && input[index - columnCount * 3] == 'S';

    private static bool CheckVerticalDown(ReadOnlySpan<char> input, int index, int columnCount) =>
        index + columnCount * 3 < input.Length && input[index + columnCount] == 'M' &&
        input[index + columnCount * 2] == 'A' && input[index + columnCount * 3] == 'S';
}