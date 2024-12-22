namespace Day21_KeypadConundrum;

public sealed class KeypadCommandSequenceFinder : KeypadCommandSequenceFinderBase
{
    private readonly Dictionary<char, IntVector2> _buttonToPosition;

    private readonly Dictionary<(char currentButton, char targetButton), List<List<char>>> _buttonsToCommand =
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

    public KeypadCommandSequenceFinder(KeypadCommandSequenceFinder keypadCommandSequenceFinder)
    {
        _buttonToPosition = keypadCommandSequenceFinder._buttonToPosition;
        _buttonsToCommand = keypadCommandSequenceFinder._buttonsToCommand;
    }

    public override IEnumerable<IEnumerable<char>> GetCommandSequences(char[] input)
    {
        IEnumerable<IEnumerable<char>> possibleSequences = _buttonsToCommand[('A', input[0])];
        var currentButton = input[0];

        foreach (var button in input[1..])
        {
            var sequencesToNextButton = _buttonsToCommand[(currentButton, button)];

            possibleSequences = from possibleSequence in possibleSequences
                from sequenceToNextButton in sequencesToNextButton
                select possibleSequence.Concat(sequenceToNextButton);
            
            currentButton = button;
        }

        return possibleSequences;
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

        _buttonsToCommand.Add((currentButton, targetButton), sequences);
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
            var sequencesFromPosition = GetValidSequences(currentPosition with { X = currentPosition.X + 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('>');
            }
            sequences.AddRange(sequencesFromPosition);
        }
        else if (targetVector.X < currentPosition.X)
        {
            var sequencesFromPosition = GetValidSequences(currentPosition with { X = currentPosition.X - 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('<');
            }
            sequences.AddRange(sequencesFromPosition);
        }
        
        if (currentPosition.Y < targetVector.Y)
        {
            var sequencesFromPosition = GetValidSequences(currentPosition with { Y = currentPosition.Y + 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('V');
            }
            sequences.AddRange(sequencesFromPosition);
        }
        else if (targetVector.Y< currentPosition.Y)
        {
            var sequencesFromPosition = GetValidSequences(currentPosition with { Y = currentPosition.Y - 1 }, targetVector);
            foreach (var sequence in sequencesFromPosition)
            {
                sequence.Add('^');
            }
            sequences.AddRange(sequencesFromPosition);
        }

        return sequences;
    } 
}