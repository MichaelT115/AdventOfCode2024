namespace Day21_KeypadConundrum;

public sealed class KeypadCommandSequenceFinder : KeypadCommandSequenceFinderBase
{
    private readonly Dictionary<char, IntVector2> _buttonToPosition;

    private readonly Dictionary<string, List<List<char>>> _inputToCommands =
        new();

    public KeypadCommandSequenceFinder(Dictionary<char, IntVector2> buttonToPosition)
    {
        _buttonToPosition = buttonToPosition;

        foreach (var startButton in _buttonToPosition.Keys)
        {
            foreach (var currentButton in _buttonToPosition.Keys)
            {
                FindCommandSequences(startButton, currentButton);
            }
        }
    }

    public override List<List<char>> GetCommandSequences(string input)
    {
        if (input.Length < 2)
        {
            return [];
        }

        if (_inputToCommands.TryGetValue(input, out var memoizedSequence))
        {
            return memoizedSequence;
        }

        var sequencesToFirstButton = _inputToCommands[input[..2]];
        var sequencesToLastButton = GetCommandSequences(input[1..]);

        var sequences = (from sequenceToFirstButton in sequencesToFirstButton
            from sequenceToLastButton in sequencesToLastButton
            select sequenceToFirstButton.Concat(sequenceToLastButton).ToList()).ToList();

        _inputToCommands.Add(input, sequences);

        return sequences;
    }

    private void FindCommandSequences(char currentButton, char targetButton)
    {
        var targetPosition = _buttonToPosition[targetButton];

        var currentPosition = _buttonToPosition[currentButton];

        var sequences = new List<List<char>>();
        foreach (var validSequence in GetValidSequences(currentPosition, targetPosition))
        {
            validSequence.Reverse();
            sequences.Add(validSequence);
        }

        _inputToCommands.Add(new string([currentButton, targetButton]), sequences);
    }

    private List<List<char>> GetValidSequences(IntVector2 currentPosition, IntVector2 targetVector)
    {
        if (currentPosition == targetVector)
        {
            return [['A']];
        }

        if (!_buttonToPosition.ContainsValue(currentPosition))
        {
            return [];
        }

        var sequences = new List<List<char>>();

        if (currentPosition.X < targetVector.X)
        {
            var sequencesFromPosition =
                GetValidSequences(currentPosition with { X = currentPosition.X + 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('>');
            }

            sequences.AddRange(sequencesFromPosition);
        }
        else if (targetVector.X < currentPosition.X)
        {
            var sequencesFromPosition =
                GetValidSequences(currentPosition with { X = currentPosition.X - 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('<');
            }

            sequences.AddRange(sequencesFromPosition);
        }

        if (currentPosition.Y < targetVector.Y)
        {
            var sequencesFromPosition =
                GetValidSequences(currentPosition with { Y = currentPosition.Y + 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('v');
            }

            sequences.AddRange(sequencesFromPosition);
        }
        else if (targetVector.Y < currentPosition.Y)
        {
            var sequencesFromPosition =
                GetValidSequences(currentPosition with { Y = currentPosition.Y - 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('^');
            }

            sequences.AddRange(sequencesFromPosition);
        }

        return sequences;
    }
}