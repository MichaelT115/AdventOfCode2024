using NUnit.Framework;

namespace Day10_HoofIt.Tests;

[TestFixture]
[TestOf(typeof(TrailHeadFinder))]
public class TrailHeadFinderTests
{

    [Test]
    [TestCase("", 0)]
    [TestCase("0", 0)]
    [TestCase("09", 0)]
    [TestCase("1234566789", 0)]
    [TestCase("0123456789", 1)]
    [TestCase("9876543210", 1)]
    [TestCase("9876543210123456789", 2)]
    [TestCase("0123456789876543210", 2)]
    [TestCase("""
              0123456789
              0123456789
              """, 2)]
    [TestCase("""
              0123456789
              ........9.
              """, 2)]

    [TestCase("""
              ...0...
              ...1...
              ...2...
              6543456
              7.....7
              8.....8
              9.....9
              """, 2)]
    [TestCase("""
              ..90..9
              ...1.98
              ...2..7
              6543456
              765.987
              876....
              987....
              """, 4)]
    [TestCase("""
              10..9..
              2...8..
              3...7..
              4567654
              ...8..3
              ...9..2
              .....01
              """, 3)]
    [TestCase("""
              89010123
              78121874
              87430965
              96549874
              45678903
              32019012
              01329801
              10456732
              """, 36)]
    public void Find_Trail_Head_Scores(string input, int expected) =>
        Assert.That(TrailHeadFinder.CountTrailHeadScores(input), Is.EqualTo(expected));
    
    [Test]
    [TestCase("", 0)]
    [TestCase("0", 0)]
    [TestCase("09", 0)]
    [TestCase("1234566789", 0)]
    [TestCase("0123456789", 1)]
    [TestCase("9876543210", 1)]
    [TestCase("9876543210123456789", 2)]
    [TestCase("0123456789876543210", 2)]
    [TestCase("""
              0123456789
              0123456789
              """, 2)]
    [TestCase("""
              0123456789
              ........9.
              """, 2)]

    [TestCase("""
              ...0...
              ...1...
              ...2...
              6543456
              7.....7
              8.....8
              9.....9
              """, 2)]
    [TestCase("""
              ..90..9
              ...1.98
              ...2..7
              6543456
              765.987
              876....
              987....
              """, 13)]
    [TestCase("""
              10..9..
              2...8..
              3...7..
              4567654
              ...8..3
              ...9..2
              .....01
              """, 3)]
    [TestCase("""
              89010123
              78121874
              87430965
              96549874
              45678903
              32019012
              01329801
              10456732
              """, 81)]
    public void Find_Trail_Head_Quality(string input, int expected) =>
        Assert.That(TrailHeadFinder.CountTrailHeadQuality(input), Is.EqualTo(expected));
}