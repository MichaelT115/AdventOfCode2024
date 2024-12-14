using System.Text.RegularExpressions;

namespace Day3_MullItOver;

public static partial class MemoryScanner
{
    [GeneratedRegex(@"mul\([0-9]{1,3},[0-9]{1,3}\)")]
    private static partial Regex ValidMultiplicationRegex();

    [GeneratedRegex(@"(mul\([0-9]{1,3},[0-9]{1,3}\))|(do\(\))|(don't\(\))")]
    private static partial Regex ValidMultiplicationAndConditionalRegex();

    public static int Scan(ReadOnlySpan<char> input)
    {
        var sum = 0;
        foreach (var match in ValidMultiplicationRegex().EnumerateMatches(input))
        {
            sum += ParseOperation(input.Slice(match.Index, match.Length));
        }

        return sum;
    }

    private static int ParseOperation(ReadOnlySpan<char> input)
    {
        int commaIndex;
        if (input[^3] == ',')
        {
            commaIndex = input.Length - 3;
        }
        else if (input[^4] == ',')
        {
            commaIndex = input.Length - 4;
        }
        else
        {
            commaIndex = input.Length - 5;
        }

        const int firstNumberStartIndex = 4;
        var secondNumberStartIndex = commaIndex + 1;
        return int.Parse(input.Slice(firstNumberStartIndex, commaIndex - firstNumberStartIndex)) *
               int.Parse(input.Slice(secondNumberStartIndex, input.Length - secondNumberStartIndex - 1));
    }

    public static int ScanWithConditionals(ReadOnlySpan<char> input)
    {
        var sum = 0;
        var isMultiplying = true;
        foreach (var match in ValidMultiplicationAndConditionalRegex().EnumerateMatches(input))
        {
            var inputMatch = input.Slice(match.Index, match.Length);

            const int doMatchLength = 4;
            const int dontMatchLength = 7;
            switch (inputMatch.Length)
            {
                case doMatchLength:
                    isMultiplying = true;
                    break;
                case dontMatchLength:
                    isMultiplying = false;
                    break;
                default:
                    if (isMultiplying)
                    {
                        sum += ParseOperation(inputMatch);
                    }
                    break;
            }
        }

        return sum;
    }
}