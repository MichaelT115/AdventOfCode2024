using NUnit.Framework;

namespace Day13_ClawContraption.Tests;

[TestFixture]
[TestOf(typeof(TokenCalculator))]
public class TokenCalculatorTests
{

    [Test]
    [TestCase(1, 0, 0, 0, 1, 0, true, 3)]
    [TestCase(0, 1, 0, 0, 0, 1, true, 3)]
    [TestCase(0, 0, 1, 0, 1, 0, true, 1)]
    [TestCase(0, 0, 0, 1, 0, 1, true, 1)]
    
    [TestCase(1, 1, 0, 0, 1, 1, true, 3)]
    [TestCase(0, 0, 1, 1, 1, 1, true, 1)]
    
    [TestCase(1, 1, 1, 1, 1, 1, true, 1)]
    
    [TestCase(1, 1, 2, 2, 3, 3, true, 4)]
    
    [TestCase(1, 0, 0, 0, 100, 0, true, 300)]
    [TestCase(1, 0, 0, 0, 101, 0, false, -1)]
    
    [TestCase(1, 0, 0, 1, 2, 2, true, 8)]
    [TestCase(1, 0, 0, 1, 50, 50, true, 200)]
    [TestCase(0, 1, 1, 0, 2, 2, true, 8)]
    [TestCase(0, 1, 1, 0, 50, 50, true, 200)]
    [TestCase(0, 1, 1, 0, 100, 100, true, 400)]
    [TestCase(0, 1, 1, 0, 100, 101, false, -1)]
    
    [TestCase(94, 34, 22, 67, 8400, 5400, true, 280)]
    [TestCase(26, 66, 67, 21, 12748, 12176, false, -1)]
    [TestCase(17, 86, 84, 37, 7870, 6450, true, 200)]
    [TestCase(69, 23, 27, 71, 18641, 10279, false, -1)]
    
    
    [TestCase(12, 61, 41, 12, 8771, 12729, false, -1)]
    public void Test_Examples(int aX, int aY, int bX, int bY, int prizeX, int prizeY, bool expectedIsPossible,
        int expectedLowestNeededTokens)
    {
        var isPossible = TokenCalculator.FindLowestNeededTokens(new TokenCalculator.Vector2
            {
                X = aX,
                Y = aY
            }, new TokenCalculator.Vector2
            {
                X = bX,
                Y = bY
            },
            new TokenCalculator.Vector2
            {
                X = prizeX,
                Y = prizeY
            }, 100, out var lowestNeededTokens);

        Assert.That(isPossible, Is.EqualTo(expectedIsPossible));

        if (isPossible)
        {
            Assert.That(lowestNeededTokens, Is.EqualTo(expectedLowestNeededTokens));
        }
    }
    
    [TestCase(26, 60, 64, 23, 3188, 11974, true, 484268970376L)]
    public void Test_Examples2(int aX, int aY, int bX, int bY, int prizeX, int prizeY, bool expectedIsPossible,
        long expectedLowestNeededTokens)
    {
        var isPossible = TokenCalculator.FindLowestNeededTokens(new TokenCalculator.Vector2
            {
                X = aX,
                Y = aY
            }, new TokenCalculator.Vector2
            {
                X = bX,
                Y = bY
            },
            new TokenCalculator.Vector2
            {
                X = prizeX,
                Y = prizeY
            }, long.MaxValue, out var lowestNeededTokens, true);

        Assert.That(isPossible, Is.EqualTo(expectedIsPossible));

        if (isPossible)
        {
            Assert.That(lowestNeededTokens, Is.EqualTo(expectedLowestNeededTokens));
        }
    }
}