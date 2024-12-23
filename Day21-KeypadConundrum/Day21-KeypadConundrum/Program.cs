using Day21_KeypadConundrum;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

// Part 1
// {
//     var commandSequenceFinder =
//         new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
//             new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
//                 KeypadCommandSequenceFinders.NumericKeypad)
//         );
//
//
//     var complexitySum = 0;
//     foreach (var line in input.EnumerateLines())
//     {
//         var sequences = commandSequenceFinder.GetCommandSequences(line.ToString()).ToArray();
//
//         var numericPortionOfInput = int.Parse(line[..^1]);
//         var sequenceLength = sequences.Min(sequence => sequence.Count);
//         var complexity = sequenceLength * numericPortionOfInput;
//
//         Console.WriteLine($"{line}: {complexity}");
//
//         complexitySum += complexity;
//     }
//
//     Console.WriteLine($"Result: {complexitySum}");
// }


// Part 2
{
    KeypadCommandSequenceFinderBase commandSequenceFinder = KeypadCommandSequenceFinders.NumericKeypad;
    for (var i = 0; i < 25; i++)
    {
        commandSequenceFinder =
            new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
                commandSequenceFinder);
    }

    var complexitySum = 0;
    foreach (var line in input.EnumerateLines())
    {
        var sequences = commandSequenceFinder.GetCommandSequences(line.ToString()).ToArray();

        var numericPortionOfInput = int.Parse(line[..^1]);
        var sequenceLength = sequences.Min(sequence => sequence.Count);
        var complexity = sequenceLength * numericPortionOfInput;

        Console.WriteLine($"{line}: {complexity}");

        complexitySum += complexity;
    }

    Console.WriteLine($"Result: {complexitySum}");
}