using System.Text.RegularExpressions;
using Day17_ChronospatialComputer;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var lines = input.Split('\n');

var registerA = int.Parse(ParsingRegex().Match(lines[0]).ValueSpan);
var registerB = int.Parse(ParsingRegex().Match(lines[1]).ValueSpan);
var registerC = int.Parse(ParsingRegex().Match(lines[2]).ValueSpan);
var computer = new ChronospatialComputer
{
    RegisterA = registerA,
    RegisterB = registerB,
    RegisterC = registerC
};

var program = ParsingRegex().Matches(lines[4]).Select(match => byte.Parse(match.ValueSpan)).ToArray();

var output = computer.RunProgram(program);

Console.WriteLine($"Program Output: {string.Join(',', output)}");
Console.WriteLine($"Lowest Possible Register: {FindPossibleRegisters(registerB, registerC, program).Min()}");

partial class Program
{
    private static readonly long[] Octals = [0, 1, 2, 3, 4, 5, 6, 7];

    [GeneratedRegex(@"\d+")]
    private static partial Regex ParsingRegex();

    private static List<long> FindPossibleRegisters(long registerB, long registerC, byte[] program)
    {
        return FindPossibleRegisters(0, registerB, registerC, program, program.Length - 1);
    }
    
    private static List<long> FindPossibleRegisters(long testRegisterA, long registerB, long registerC, byte[] program, int octalSlot = 15)
    {
        var possibleRegisterValues = Octals
            .Select(octal => testRegisterA | (octal << (3 * octalSlot)))
            .Where(registerA =>
            {
                var output = new ChronospatialComputer
                {
                    RegisterA = registerA,
                    RegisterB = registerB,
                    RegisterC = registerC
                }.RunProgram(program);

                return output.Length == program.Length && output[octalSlot] == program[octalSlot];
            });

        return octalSlot == 0
            ? possibleRegisterValues.ToList()
            : possibleRegisterValues.SelectMany(registerA =>
                FindPossibleRegisters(registerA, registerB, registerC, program, octalSlot - 1)).ToList();
    }
}