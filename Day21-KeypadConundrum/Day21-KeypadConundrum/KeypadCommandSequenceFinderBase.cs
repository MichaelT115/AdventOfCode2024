namespace Day21_KeypadConundrum;

public abstract class KeypadCommandSequenceFinderBase
{
    public abstract IEnumerable<IEnumerable<char>> GetCommandSequences(char[] chars);
}