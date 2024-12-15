using NUnit.Framework;

namespace Day14_RestroomRedoubt.Tests;

[TestFixture]
[TestOf(typeof(Program))]
public class RestroomRedoubtTests
{

    [Test]
    [TestCase(0, 0, 0, 0, 1, 1, 0, 0, 0)]
    [TestCase(0, 0, 1, 1, 10, 10, 0, 0, 0)]
    
    [TestCase(2, 4, 2, -3, 11, 7, 0, 2, 4)]
    [TestCase(2, 4, 2, -3, 11, 7, 1, 4, 1)]
    [TestCase(2, 4, 2, -3, 11, 7, 2, 6, 5)]
    [TestCase(2, 4, 2, -3, 11, 7, 3, 8, 2)]
    [TestCase(2, 4, 2, -3, 11, 7, 4, 10, 6)]
    [TestCase(2, 4, 2, -3, 11, 7, 5, 1, 3)]
    public void Test_Examples(int positionX, int positionY,
        int velocityX, int velocityY, int width, int height, int ticks, int expectedX, int expectedY) =>
        Assert.That(Program.CalculateFinalPosition(positionX, positionY, velocityX, velocityY, width, height, ticks),
            Is.EqualTo((expectedX, expectedY)));
}