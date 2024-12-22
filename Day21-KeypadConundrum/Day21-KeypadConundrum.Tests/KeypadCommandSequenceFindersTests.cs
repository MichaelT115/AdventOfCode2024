using System;
using System.Collections.Generic;
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
            char targetButton) =>
            Assert.That(
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences([targetButton]),
                Is.Not.Empty);

        [Test]
        public void Returns_Command_Sequence_Ending_With_Activate_Command(
            [ValueSource(nameof(NumericKeypadButtons))]
            char targetButton) =>
            Assert.That(
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences([targetButton])
                    .Select(sequence => sequence.Last()), Is.All.EqualTo('A'));

        [Test]
        [TestCase('0', 0, 0, 0, 1)]
        [TestCase('A', 0, 0, 0, 0)]
        [TestCase('1', 1, 0, 0, 2)]
        [TestCase('2', 1, 0, 0, 1)]
        [TestCase('3', 1, 0, 0, 0)]
        [TestCase('4', 2, 0, 0, 2)]
        [TestCase('5', 2, 0, 0, 1)]
        [TestCase('6', 2, 0, 0, 0)]
        [TestCase('7', 3, 0, 0, 2)]
        [TestCase('8', 3, 0, 0, 1)]
        [TestCase('9', 3, 0, 0, 0)]
        public void Returns_Sequence_With_Smallest_Needed_Directions(char targetButton,
            int expectedUpCommandsCount, int expectedRightCommandsCount, int expectedDownCommandsCount,
            int expectedLeftCommandsCount)
        {
            var sequences =
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences([targetButton]);

            foreach (var sequence in sequences.Select( sequence => sequence.ToArray()))
            {
                Assert.That(sequence.Count(command => command == '^'), Is.EqualTo(expectedUpCommandsCount));
                Assert.That(sequence.Count(command => command == '>'), Is.EqualTo(expectedRightCommandsCount));
                Assert.That(sequence.Count(command => command == 'V'), Is.EqualTo(expectedDownCommandsCount));
                Assert.That(sequence.Count(command => command == '<'), Is.EqualTo(expectedLeftCommandsCount));
            }
        }

        [Test]
        [TestCase('1', "<<^A")]
        public void Never_Returns_Path_Through_Position_Without_Button(char targetButton,
            params object[] invalidCommandSequences) =>
            Assert.That(
                KeypadCommandSequenceFinders.NumericKeypad.GetCommandSequences([targetButton]),
                Is.Not.AnyOf(invalidCommandSequences));
    }

    [TestFixture]
    public class Directional_Keypad_Command_Sequence_Finder
    {
        private static readonly char[] DirectionalKeypadButtons = ['^', '>', 'V', '<', 'A'];

        [Test]
        public void Returns_Command_Sequence_From_Any_Button_To_Any_Button(
            [ValueSource(nameof(DirectionalKeypadButtons))] char targetButton) =>
            Assert.That(
                KeypadCommandSequenceFinders.DirectionalKeypad.GetCommandSequences([targetButton])
                    .ToString(), Is.Not.Empty);

        [Test]
        public void Returns_Command_Sequence_Ending_With_Activate_Command(
            [ValueSource(nameof(DirectionalKeypadButtons))] char targetButton) =>
            Assert.That(
                KeypadCommandSequenceFinders.DirectionalKeypad.GetCommandSequences([targetButton])
                    .Select(sequence => sequence.Last()), Is.All.EqualTo('A'));

        [Test]
        [TestCase('^', 0, 0, 0, 1)]
        [TestCase('A', 0, 0, 0, 0)]
        [TestCase('<', 0, 0, 1, 2)]
        [TestCase('V', 0, 0, 1, 1)]
        [TestCase('>', 0, 0, 1, 0)]
        public void Returns_Sequence_With_Smallest_Needed_Directions(char targetButton,
            int expectedUpCommandsCount, int expectedRightCommandsCount, int expectedDownCommandsCount,
            int expectedLeftCommandsCount)
        {
            var sequences =
                KeypadCommandSequenceFinders.DirectionalKeypad.GetCommandSequences([targetButton]);

            foreach (var sequence in sequences.Select( sequence => sequence.ToArray()))
            {
                Assert.That(sequence.Count(command => command == '^'), Is.EqualTo(expectedUpCommandsCount));
                Assert.That(sequence.Count(command => command == '>'), Is.EqualTo(expectedRightCommandsCount));
                Assert.That(sequence.Count(command => command == 'V'), Is.EqualTo(expectedDownCommandsCount));
                Assert.That(sequence.Count(command => command == '<'), Is.EqualTo(expectedLeftCommandsCount));
            }
        }

        [Test]
        [TestCase('^', '<', "<<VA")]
        public void Never_Returns_Path_Through_Position_Without_Button(char targetButton,
            params object[] invalidCommandSequences) =>
            Assert.That(KeypadCommandSequenceFinders.DirectionalKeypad
                .GetCommandSequences([targetButton])
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
                    .GetCommandSequences([targetButton])
                    .ToString(),
                Is.Not.Empty);
        
    }
}