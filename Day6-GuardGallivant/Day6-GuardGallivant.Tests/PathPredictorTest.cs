﻿using NUnit.Framework;

namespace Day6_GuardGallivant.Tests;

[TestFixture]
[TestOf(typeof(PathPredictor))]
public class PathPredictorTest
{

    [TestCase("""
              ....#.....
              .........#
              ..........
              ..#.......
              .......#..
              ..........
              .#..^.....
              ........#.
              #.........
              ......#...
              """, 41)]
    public void Test_Example(string input, int expected)
    {
        var (startPosition, grid) = InputParser.Parse(input);
        Assert.That(PathPredictor.GetPredictedUniqueGuardPathPositionsCount(grid, startPosition), Is.EqualTo(expected));
    }
    
    [TestCase("""
              ....#.....
              .........#
              ..........
              ..#.......
              .......#..
              ..........
              .#..^.....
              ........#.
              #.........
              ......#...
              """, 6)]
    public void Test_Loop_Example(string input, int expected)
    {
        var (startPosition, grid) = InputParser.Parse(input);
        Assert.That(PathPredictor.GetPossibleObstructionPlacementsCount(grid, startPosition), Is.EqualTo(expected));
    }
}