namespace Day21_KeypadConundrum;

public sealed class NestedKeypadCommandSequenceFinder(
    KeypadCommandSequenceFinderBase nestingKeypad,
    KeypadCommandSequenceFinderBase nestedKeypad) : KeypadCommandSequenceFinderBase
{
    private readonly Dictionary<string, List<List<char>>> _inputToCommands =
        new();

    public override List<List<char>> GetCommandSequences(string input)
    {
        List<List<char>> sequences = [[]];

        for (var index = 0; index <= input.Length - 2; ++index)
        {
            var pair = input.Substring(index, 2);

            if (_inputToCommands.TryGetValue(pair, out var memoizedSequence))
            {
                sequences.AddRange(memoizedSequence);
                continue;
            }

            var shortestSequences = FindShortestSequence(pair);
            _inputToCommands.Add(pair, shortestSequences);

            sequences = shortestSequences
                .SelectMany(shortestSequence
                    => sequences.Select(sequence => sequence.Concat(shortestSequence).ToList()))
                .ToList();
        }

        return sequences;
    }

    private List<List<char>> FindShortestSequence(string pair)
    {
        var commandSequences = nestedKeypad.GetCommandSequences(pair);
        var sequences = commandSequences
            .SelectMany(nestedKeypadSequence =>
                nestingKeypad.GetCommandSequences(new string(['A', ..nestedKeypadSequence.ToArray()]))).ToList();
        return sequences;
    }
}