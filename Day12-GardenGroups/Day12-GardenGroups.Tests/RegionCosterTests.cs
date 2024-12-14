using NUnit.Framework;

namespace Day12_GardenGroups.Tests;

[TestFixture]
[TestOf(typeof(RegionCoster))]
public class RegionCosterTests
{

    [Test]
    [TestCase("", 0)]
    [TestCase("A", 4)]
    [TestCase("AA", 12)]
    [TestCase("AAA", 24)]
    [TestCase("""
              A
              A
              """, 12)]
    [TestCase("""
              A
              A
              A
              """, 24)]
    [TestCase("""
              AA
              AA
              """, 4 * 8)]
    [TestCase("AB", 8)]
    [TestCase("ABA", 12)]
    [TestCase("ABAB", 16)]
    [TestCase("""
              AB
              AB
              """, 24)]
    [TestCase("""
              OOOOO
              OXOXO
              OOOOO
              OXOXO
              OOOOO
              """, 772)]
    [TestCase("""
              RRRRIICCFF
              RRRRIICCCF
              VVRRRCCFFF
              VVRCCCJFFF
              VVVVCJJCFE
              VVIVCCJJEE
              VVIIICJJEE
              MIIIIIJJEE
              MIIISIJEEE
              MMMISSJEEE
              """, 1930)]
    public void Total_Costs_Equals_Sum_Of_The_Multiplication_Of_The_Area_And_Perimeter_Of_Each_Region(string input, int expectedCost)
        => Assert.That(RegionCoster.FindTotalCost(input), Is.EqualTo((ulong)expectedCost));
}