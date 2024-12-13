using System.Linq;
using Day11_PlutonianPebbles;
using NUnit.Framework;

namespace Day11_PlutonianPebblesTests;

[TestFixture]
[TestOf(typeof(StoneCounter))]
public class StoneCounterTest
{
    public sealed class Stones_With_An_Odd_Number_Of_Digits
    {
        [Test]
        [TestCase(0ul)]
        public void With_The_Value_Zero_Is_Replaced_By_One(ulong stone) =>
            Assert.That(StoneCounter.ProcessStone(stone), Has.All.EqualTo(1));

        [Test]
        [TestCase(1ul, new[] { 2024ul })]
        [TestCase(999ul, new[] { 2021976ul })]
        public void With_Non_Zero_Value_Is_Multiplied_By_2024(ulong stone, ulong[] expected) =>
            Assert.That(StoneCounter.ProcessStone(stone), Is.EquivalentTo(expected));
    }

    public sealed class Stones_With_An_Even_Number_Of_Digits
    {
        [Test]
        [TestCase(11ul, new[] { 1ul, 1ul })]
        [TestCase(10ul, new[] { 1ul, 0ul })]
        [TestCase(22ul, new[] { 2ul, 2ul })]
        [TestCase(2222ul, new[] { 22ul, 22ul })]
        public void
            Are_Replaced_With_Two_Stones_With_One_Stone_With_The_Left_Half_Of_The_Digits_And_One_Stone_With_The_Right_Half_Of_The_Digits(
                ulong stone, ulong[] expected) =>
            Assert.That(StoneCounter.ProcessStone(stone), Is.EquivalentTo(expected));
    }
}