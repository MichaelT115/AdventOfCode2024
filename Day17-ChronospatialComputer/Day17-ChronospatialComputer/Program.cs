using System.Text.RegularExpressions;
using Day17_ChronospatialComputer;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var lines = input.Split('\n');

var computer = new ChronospatialComputer
{
    RegisterA = int.Parse(ParsingRegex().Match(lines[0]).ValueSpan),
    RegisterB = int.Parse(ParsingRegex().Match(lines[1]).ValueSpan),
    RegisterC = int.Parse(ParsingRegex().Match(lines[2]).ValueSpan)
};

var program = ParsingRegex().Matches(lines[4]).Select(match => byte.Parse(match.ValueSpan)).ToArray();

var output = computer.RunProgram(program);

Console.WriteLine($"Output: {string.Join(',', output)}");

partial class Program
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex ParsingRegex();
}