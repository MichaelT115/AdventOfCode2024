using NUnit.Framework;

namespace Day22_MonkeyMarket.Tests;

[TestFixture]
[TestOf(typeof(MonkeyMarketPredictor))]
public class MonkeyMarketPredictorTests
{
    [Test]
    [TestCase(123, 1, 15887950L)]
    [TestCase(15887950L, 1, 16495136)]
    [TestCase(16495136, 1, 527345)]
    [TestCase(527345, 1, 704524)]
    [TestCase(704524, 1, 1553684)]
    [TestCase(1553684, 1, 12683156)]
    [TestCase(12683156, 1, 11100544)]
    [TestCase(11100544, 1, 12249484)]
    [TestCase(12249484, 1, 7753432)]
    [TestCase(7753432, 1, 5908254)]

    [TestCase(1, 2000, 8685429)]
    [TestCase(10, 2000, 4700978)]
    [TestCase(100, 2000, 15273692)]
    [TestCase(2024, 2000, 8667524)]
    public void Test_Examples(long input, int count, long expectedNextNumber) =>
        Assert.That(MonkeyMarketPredictor.CalculateSecretNumber(input, count), Is.EqualTo(expectedNextNumber));
}