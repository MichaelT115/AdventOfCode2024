using System.Linq;
using NUnit.Framework;

namespace Day21_KeypadConundrum.Tests;

[TestFixture]
[TestOf(typeof(KeypadCommandSequenceFinder))]
public class KeypadCommandSequenceFindersTests
{
    [TestFixture]
    public class Numeric_Keypad_Commands_Finder
    {
        private static readonly char[] NumericKeypadButtons = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A'];

        [Test]
        public void Returns_Command_Sequence_From_Any_Button_To_Any_Button(
            [ValueSource(nameof(NumericKeypadButtons))]
            char startButton,
            [ValueSource(nameof(NumericKeypadButtons))]
            char targetButton) =>
            Assert.That(
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences($"{startButton}{targetButton}"),
                Is.Not.Empty);

        [Test]
        [TestCase("A0", 0, 0, 0, 1)]
        [TestCase("AA", 0, 0, 0, 0)]
        [TestCase("A1", 1, 0, 0, 2)]
        [TestCase("A2", 1, 0, 0, 1)]
        [TestCase("A3", 1, 0, 0, 0)]
        [TestCase("A4", 2, 0, 0, 2)]
        [TestCase("A5", 2, 0, 0, 1)]
        [TestCase("A6", 2, 0, 0, 0)]
        [TestCase("A7", 3, 0, 0, 2)]
        [TestCase("A8", 3, 0, 0, 1)]
        [TestCase("A9", 3, 0, 0, 0)]
        public void Returns_Sequence_With_Smallest_Needed_Directions(string input,
            int expectedUpCommandsCount, int expectedRightCommandsCount, int expectedDownCommandsCount,
            int expectedLeftCommandsCount)
        {
            var sequences =
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences(input);

            foreach (var sequence in sequences.Select(sequence => sequence.ToArray()))
            {
                Assert.That(sequence.Count(command => command == '^'), Is.EqualTo(expectedUpCommandsCount));
                Assert.That(sequence.Count(command => command == '>'), Is.EqualTo(expectedRightCommandsCount));
                Assert.That(sequence.Count(command => command == 'v'), Is.EqualTo(expectedDownCommandsCount));
                Assert.That(sequence.Count(command => command == '<'), Is.EqualTo(expectedLeftCommandsCount));
            }
        }

        [Test]
        [TestCase("A1", "<<^A")]
        public void Never_Returns_Path_Through_Position_Without_Button(string input,
            params object[] invalidCommandSequences) =>
            Assert.That(
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences(input),
                Is.Not.AnyOf(invalidCommandSequences));
    }

    [TestFixture]
    public class Directional_Keypad_Command_Sequence_Finder
    {
        private static readonly char[] DirectionalKeypadButtons = ['^', '>', 'v', '<', 'A'];

        [Test]
        public void Returns_Command_Sequence_From_Any_Button_To_Any_Button(
            [ValueSource(nameof(DirectionalKeypadButtons))]
            char startButton,
            [ValueSource(nameof(DirectionalKeypadButtons))]
            char targetButton) =>
            Assert.That(
                KeypadCommandSequenceFinders.DirectionalKeypad.GetCommandSequences($"{startButton}{targetButton}")
                    .ToString(), Is.Not.Empty);

        [Test]
        [TestCase("A^", 0, 0, 0, 1)]
        [TestCase("AA", 0, 0, 0, 0)]
        [TestCase("A<", 0, 0, 1, 2)]
        [TestCase("Av", 0, 0, 1, 1)]
        [TestCase("A>", 0, 0, 1, 0)]
        public void Returns_Sequence_With_Smallest_Needed_Directions(string input,
            int expectedUpCommandsCount, int expectedRightCommandsCount, int expectedDownCommandsCount,
            int expectedLeftCommandsCount)
        {
            var sequences =
                KeypadCommandSequenceFinders.DirectionalKeypad.GetCommandSequences(input);

            foreach (var sequence in sequences.Select(sequence => sequence.ToArray()))
            {
                Assert.That(sequence.Count(command => command == '^'), Is.EqualTo(expectedUpCommandsCount));
                Assert.That(sequence.Count(command => command == '>'), Is.EqualTo(expectedRightCommandsCount));
                Assert.That(sequence.Count(command => command == 'v'), Is.EqualTo(expectedDownCommandsCount));
                Assert.That(sequence.Count(command => command == '<'), Is.EqualTo(expectedLeftCommandsCount));
            }
        }

        [Test]
        [TestCase("A^", '<', "<<VA")]
        public void Never_Returns_Path_Through_Position_Without_Button(string input,
            params object[] invalidCommandSequences) =>
            Assert.That(KeypadCommandSequenceFinders.DirectionalKeypad
                .GetCommandSequences(input)
                .ToString(), Is.Not.AnyOf(invalidCommandSequences));
    }

    [TestFixture]
    public class Nested_Keypad_Command_Sequence_Finder
    {
        private static readonly char[] NumericKeypadButtons = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A'];

        [Test]
        public void Returns_Command_Sequence_From_Any_Button_To_Any_Button(
            [ValueSource(nameof(NumericKeypadButtons))]
            char targetButton) =>
            Assert.That(
                new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
                        KeypadCommandSequenceFinders.NumericKeypad)
                    .GetCommandSequences($"{targetButton}")
                    .ToString(),
                Is.Not.Empty);

    }

    [Test]
    [TestCase("029A", 28 * 29)]
    public void Test_Examples_2(string input, int expectedComplexity)
    {
        var commandSequenceFinder =
            new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
                KeypadCommandSequenceFinders.NumericKeypad);

        Assert.That(commandSequenceFinder.GetComplexity($"A{input}"), Is.EqualTo(expectedComplexity));
    }
    

    [Test]
    [TestCase("029A", 68 * 29)]
    [TestCase("980A", 60 * 980)]
    [TestCase("179A", 68 * 179)]
    [TestCase("456A", 64 * 456)]
    [TestCase("379A", 64 * 379)]

    public void Test_Examples(string input, int expectedComplexity)
    {
        var commandSequenceFinder =
            new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
                new NestedKeypadCommandSequenceFinder(KeypadCommandSequenceFinders.DirectionalKeypad,
                    KeypadCommandSequenceFinders.NumericKeypad)
            );

        Assert.That(commandSequenceFinder.GetComplexity($"A{input}"), Is.EqualTo(expectedComplexity));
    }
}