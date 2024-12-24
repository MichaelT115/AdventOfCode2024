using System.Text.RegularExpressions;

namespace Day24_CrossedWires;

internal static partial class Regexes
{
    [GeneratedRegex(@"([^:]+): (\d+)")]
    public static partial Regex RegisterRegex();
    
    [GeneratedRegex(@"(\S+) (\S+) (\S+) -> (\S+)")]
    public static partial Regex OperationsRegex();
}