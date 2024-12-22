namespace Day21_KeypadConundrum;

public sealed class NestedKeypadCommandSequenceFinder(
    KeypadCommandSequenceFinderBase nestingKeypad,
    KeypadCommandSequenceFinderBase nestedKeypad) : KeypadCommandSequenceFinderBase
{
    public override IEnumerable<IEnumerable<char>> GetCommandSequences(char[] input)
    {
        var internalSequence = nestedKeypad.GetCommandSequences(input);
        return internalSequence.SelectMany(sequence => nestingKeypad.GetCommandSequences(sequence.ToArray()));
    }
}