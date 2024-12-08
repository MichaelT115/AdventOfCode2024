namespace Day4_CeresSearch;

public static partial class WordSearchSolver
{
    public static int Solve(ReadOnlySpan<char> input)
    {
        var firstLineBreakIndex = input.IndexOf('\n');
        var columnCount = firstLineBreakIndex != -1 ? firstLineBreakIndex + 1 : input.Length;

        var count = 0;
        for (var i = 0; i < input.Length; ++i)
        {
            var character = input[i];
            if (character != 'X') continue;

            // Vertical Down
            if (i + columnCount * 3 < input.Length && input[i + columnCount] == 'M' &&
                input[i + columnCount * 2] == 'A' && input[i + columnCount * 3] == 'S')
            {
                ++count;
            }

            // Vertical Up
            if (columnCount * 3 <= i && input[i - columnCount] == 'M' &&
                input[i - columnCount * 2] == 'A' && input[i - columnCount * 3] == 'S')
            {
                ++count;
            }

            // Diagonal Forward Down
            if (i + columnCount * 3 + 3 < input.Length && input[i + columnCount + 1] == 'M' &&
                input[i + columnCount * 2 + 2] == 'A' && input[i + columnCount * 3 + 3] == 'S')
            {
                ++count;
            }
            
            // Diagonal Forward Up
            if (columnCount * 3 -3 <= i && input[i - (columnCount - 1)] == 'M' &&
                input[i - (columnCount * 2 - 2)] == 'A' && input[i - (columnCount * 3 - 3)] == 'S')
            {
                ++count;
            }
            
            // Diagonal Backward Up
            if (columnCount * 3 + 3 <= i && input[i - (columnCount + 1)] == 'M' &&
                input[i - (columnCount * 2 + 2)] == 'A' && input[i - (columnCount * 3 + 3)] == 'S')
            {
                ++count;
            }
            
            // Diagonal Backward Down
            if (i + columnCount * 3 - 3 < input.Length && input[i + columnCount - 1] == 'M' &&
                input[i + columnCount * 2 - 2] == 'A' && input[i + columnCount * 3 - 3] == 'S')
            {
                ++count;
            }

            // Horizontal Backward
            if (i >= 3 && input.Slice(i - 3, 4) is "SAMX")
            {
                ++count;
            }

            // Horizontal Forward
            if (input.Length - i >= 4 && input.Slice(i, 4) is "XMAS")
            {
                ++count;
            }
        }

        return count;
    }
}