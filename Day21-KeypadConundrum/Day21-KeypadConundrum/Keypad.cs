using System.Text.RegularExpressions;

namespace Day21_KeypadConundrum;

public sealed partial class Keypad
{
    private readonly Keypad? _controller;

    private readonly Dictionary<char, IntVector2> _buttonToPosition;

    private readonly Dictionary<string, List<List<char>>> _inputToCommands =
        new();

    private readonly Dictionary<string, long> _inputsToMinimumCommandLengths =
        new();

    public Keypad(Keypad? controller,
        Dictionary<char, IntVector2> buttonToPosition)
    {
        _controller = controller;

        _buttonToPosition = buttonToPosition;

        foreach (var startButton in _buttonToPosition.Keys)
        {
            foreach (var currentButton in _buttonToPosition.Keys)
            {
                FindCommandSequences(startButton, currentButton);
            }
        }
    }

    public long GetComplexity(string input)
        => GetMinimumCommandLength(input) * int.Parse(input[..^1]);

    private long GetMinimumCommandLength(string input)
    {
        if (_inputsToMinimumCommandLengths.TryGetValue(input, out var length))
        {
            return length;
        }

        length = 0L;

        var previousCharacter = 'A';
        var sections = SectionSplittingRegex().Split(input);
        foreach (var section in sections)
        {
            if (section.Length == 0)
            {
                continue;
            }

            var fullSection = $"{previousCharacter}{section}";

            if (_inputsToMinimumCommandLengths.TryGetValue(fullSection, out var sectionLength))
            {
                length += sectionLength;
                continue;
            }

            var commandSequences = GetCommandSequences(fullSection);
            var sectionsLengths = _controller != null
                ? commandSequences.Select(sequence =>
                    _controller.GetMinimumCommandLength(new string(sequence.ToArray()))).ToArray()
                : commandSequences.Select(sequence => (long)sequence.Count).ToArray();


            var shortestSectionLength = sectionsLengths.Length != 0 ? sectionsLengths.Min() : 0;

            _inputsToMinimumCommandLengths.Add(fullSection, shortestSectionLength);
            length += shortestSectionLength;
            previousCharacter = fullSection[^1];
        }

        return length;
    }

    public List<List<char>> GetCommandSequences(string input)
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

    // Normal string.split() does not include the delimiter.
    [GeneratedRegex("(?<=A)")]
    private static partial Regex SectionSplittingRegex();
}