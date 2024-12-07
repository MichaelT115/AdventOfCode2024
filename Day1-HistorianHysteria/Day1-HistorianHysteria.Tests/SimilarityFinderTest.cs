using Day1_HistorianHysteria.Part1;
using NUnit.Framework;

namespace Day1_HistorianHysteria.Tests;

[TestFixture]
[TestOf(typeof(SimilarityFinder))]
public class SimilarityFinderTest
{
    [TestCase("""
              3   4
              4   3
              2   5
              1   3
              3   9
              3   3
              """, 31)]
    public void SimilarityTests(string input, int expected) =>
        Assert.That(SimilarityFinder.FindSimilarity(input), Is.EqualTo(expected));
}