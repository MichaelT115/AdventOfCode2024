using Day1_HistorianHysteria.Part1;
using NUnit.Framework;

namespace Day1_HistorianHysteria.Tests;

[TestFixture]
[TestOf(typeof(DistanceFinder))]
public class DistanceFinderTest
{
    [TestCase("0 0", 0)]
    [TestCase("1 0", 1)]
    [TestCase("0 1", 1)]
    [TestCase("1 1", 0)]
    [TestCase("-1 1", 2)]
    public void
        The_Distance_Between_Single_Entry_Lists_Is_The_Distance_Between_The_Entries(string input, int expected) =>
        Assert.That(DistanceFinder.FindDistance(input), Is.EqualTo(expected));

    [TestCase("""
              0 0
              0 0
              """, 0)]
    [TestCase("""
              1 0
              0 2
              """, 1)]
    [TestCase("""
              3   4
              4   3
              2   5
              1   3
              3   9
              3   3
              """, 11)]
    public void
        The_Distance_Between_Multi_Entry_Lists_Is_Equal_To_The_Accumulative_Distances_Between_Each_Entry_Of_Those_Lists_Sorted(
            string input, int expected) =>
        Assert.That(DistanceFinder.FindDistance(input), Is.EqualTo(expected));
}