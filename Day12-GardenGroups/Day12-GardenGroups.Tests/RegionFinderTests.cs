using System.Linq;
using NUnit.Framework;

namespace Day12_GardenGroups.Tests;

[TestFixture]
[TestOf(typeof(RegionFinder))]
public class RegionFinderTests
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
              """, 32)]
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
    public void Total_Costs_Equals_Sum_Of_The_Multiplication_Of_The_Area_And_Perimeter_Of_Each_Region(string input,
        int expectedCost)
        => Assert.That(RegionFinder.GetRegions(input).Sum(region => region.Area * region.Perimeter),
            Is.EqualTo((ulong)expectedCost));
    
    
    [Test]
    [TestCase("", 0)]
    [TestCase("A", 4)]
    [TestCase("AA", 8)]
    [TestCase("AAA", 12)]
    [TestCase("""
              A
              A
              """, 8)]
    [TestCase("""
              A
              A
              A
              """, 12)]
    [TestCase("""
              AA
              AA
              """, 16)]
    [TestCase("AB", 8)]
    [TestCase("ABA", 12)]
    [TestCase("ABAB", 16)]
    [TestCase("""
              AB
              AB
              """, 16)]
    [TestCase("""
              AAAA
              BBCD
              BBCC
              EEEC
              """, 80)]
    [TestCase("""
              OOOOO
              OXOXO
              OOOOO
              OXOXO
              OOOOO
              """, 436)]
    [TestCase("""
              AAAAAA
              AAABBA
              AAABBA
              ABBAAA
              ABBAAA
              AAAAAA
              """, 368)]
    [TestCase("""
              EEEEE
              EXXXX
              EEEEE
              EXXXX
              EEEEE
              """, 236)]
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
              """, 1206)]
    public void Total_Costs_Equals_Sum_Of_The_Multiplication_Of_The_Area_And_Sides_Of_Each_Region(string input,
        int expectedCost)
        => Assert.That(RegionFinder.GetRegions(input).Sum(region => region.Area * region.Corners),
            Is.EqualTo((ulong)expectedCost));
}