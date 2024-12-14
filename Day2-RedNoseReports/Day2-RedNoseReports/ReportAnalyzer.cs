using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Day2_RedNoseReports;

public static class ReportAnalyzer
{
    public static int CalculateSafeReportsCount(ReadOnlySpan<char> reports, bool allowRemovals = false)
    {
        var count = 0;

        foreach (var report in reports.EnumerateLines())
        {
            if (IsSafe(report, allowRemovals))
            {
                ++count;
            }
        }

        return count;
    }

    public static bool IsSafe(ReadOnlySpan<char> report, bool allowRemovals = false)
    {
        var values = ParseReportIntoLevels(report).AsSpan();

        return IsSafe(values[0], values[1..], allowRemovals);
    }

    private static int[] ParseReportIntoLevels(ReadOnlySpan<char> report)
    {
        var values = new List<int>();
        foreach (var range in Regex.EnumerateSplits(report, "\\s"))
        {
            var (start, length) = range.GetOffsetAndLength(report.Length);
            values.Add(int.Parse(report.Slice(start, length)));
        }

        return values.ToArray();
    }

    private static bool IsSafe(int firstLevel, ReadOnlySpan<int> reportValues, bool allowRemoval) =>
        IsSafe(firstLevel, reportValues, firstLevel < reportValues[0], allowRemoval);

    private static bool IsSafe(int firstLevel, ReadOnlySpan<int> reportValues, bool isAscending, bool allowRemoval)
    {
        if (reportValues.IsEmpty)
        {
            return true;
        }

        var secondLevel = reportValues[0];
        if (!IsGoodLevel(firstLevel, secondLevel, isAscending))
        {
            if (!allowRemoval)
            {
                return false;
            }

            return IsSafe(firstLevel, reportValues[1..], false) ||
                   IsSafe(secondLevel, reportValues[2..], false);
        }

        for (var index = 1; index < reportValues.Length; ++index)
        {
            var previousLevel = reportValues[index - 1];
            var currentLevel = reportValues[index];
            if (IsGoodLevel(previousLevel, currentLevel, isAscending)) continue;

            if (!allowRemoval) return false;
            
            if (index == 1)
            {
                return IsSafe(firstLevel, reportValues[index..], false) ||
                       IsSafe(previousLevel, reportValues[(index + 1)..], isAscending, false);
            }

            return IsSafe(reportValues[index - 2], reportValues[index..], isAscending,
                       false) ||
                   IsSafe(previousLevel, reportValues[(index + 1)..], isAscending, false);

        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsGoodLevel(int previousLevel, int currentLevel, bool isAscending) =>
        previousLevel != currentLevel
        && Math.Abs(currentLevel - previousLevel) is >= 1 and <= 3
        && isAscending == previousLevel < currentLevel;
}