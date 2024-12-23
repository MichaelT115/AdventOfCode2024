namespace Day21_KeypadConundrum;

public static class KeypadCommandSequenceFinders
{
    public static KeypadCommandSequenceFinder NumericKeypad => new(
        new Dictionary<char, IntVector2>
        {
            { '7', new IntVector2(0, 0) },
            { '8', new IntVector2(1, 0) },
            { '9', new IntVector2(2, 0) },
            { '4', new IntVector2(0, 1) },
            { '5', new IntVector2(1, 1) },
            { '6', new IntVector2(2, 1) },
            { '1', new IntVector2(0, 2) },
            { '2', new IntVector2(1, 2) },
            { '3', new IntVector2(2, 2) },
            { '0', new IntVector2(1, 3) },
            { 'A', new IntVector2(2, 3) },
        });

    public static KeypadCommandSequenceFinder DirectionalKeypad => new(
        new Dictionary<char, IntVector2>
        {
            { '^', new IntVector2(1, 0) },
            { 'A', new IntVector2(2, 0) },
            { '<', new IntVector2(0, 1) },
            { 'v', new IntVector2(1, 1) },
            { '>', new IntVector2(2, 1) },
        });
}