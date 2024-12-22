using Day21_KeypadConundrum;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

var commandSequenceFinder =
    new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
        new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
            KeypadCommandSequenceFinders.NumericKeypad)
    );


var complexitySum = 0;
foreach (var line in input.EnumerateLines())
{
    var sequences = commandSequenceFinder.GetCommandSequences(line.ToArray()).ToArray();

    var numericPortionOfInput = int.Parse(line[..^1]);
    var sequenceLength = sequences.Min(sequence => sequence.Count());
    var complexity = sequenceLength * numericPortionOfInput;

    Console.WriteLine($"{line}: {complexity}");

    complexitySum += complexity;
}

Console.WriteLine($"Result: {complexitySum}");
