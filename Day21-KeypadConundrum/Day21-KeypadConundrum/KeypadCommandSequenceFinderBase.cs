namespace Day21_KeypadConundrum;

public abstract class KeypadCommandSequenceFinderBase
{
    public abstract List<List<char>> GetCommandSequences(string input);

    public long GetComplexity(string input)
    {
        var sequences = GetCommandSequences(input);

        var numericPortionOfInput = int.Parse(input.Substring(1, input.Length - 2));
        var sequenceLength = sequences.Min(sequence => sequence.Count);
        var complexity = sequenceLength * numericPortionOfInput;
        return complexity;
    }
}