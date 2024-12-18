using NUnit.Framework;

namespace Day16_ReindeerMaze.Tests;

[TestFixture]
[TestOf(typeof(PathSolver))]
public class PathSolverTests
{

    [Test]
    [TestCase("""
              ####
              #SE#
              ####
              """, 1, 2)]
    [TestCase("""
              #####
              #S E#
              #####
              """, 2, 3)]
    [TestCase("""
              #####
              #E S#
              #####
              """, 2002, 3)]

    [TestCase("""
              #####
              #E  #
              # # #
              #  S#
              #####
              """, 2004, 5)]

    [TestCase("""
              #####
              #   #
              #S#E#
              #   #
              #####
              """, 3004, 8)]

    [TestCase("""
              ########
              #S   ###
              ## # ###
              ##    E#
              ########
              """, 2007, 11)]

    [TestCase("""
              ###############
              #.......#....E#
              #.#.###.#.###.#
              #.....#.#...#.#
              #.###.#####.#.#
              #.#.#.......#.#
              #.#.#####.###.#
              #...........#.#
              ###.#.#####.#.#
              #...#.....#.#.#
              #.#.#.###.#.#.#
              #.....#...#.#.#
              #.###.#.#.#.#.#
              #S..#.....#...#
              ###############
              """, 7036, 45)]
    [TestCase("""
              #################
              #...#...#...#..E#
              #.#.#.#.#.#.#.#.#
              #.#.#.#...#...#.#
              #.#.#.#.###.#.#.#
              #...#.#.#.....#.#
              #.#.#.#.#.#####.#
              #.#...#.#.#.....#
              #.#.#####.#.###.#
              #.#.#.......#...#
              #.#.###.#####.###
              #.#.#...#.....#.#
              #.#.#.#####.###.#
              #.#.#.........#.#
              #.#.#.#########.#
              #S#.............#
              #################
              """, 11048, 64)]
    public void Test_Examples(string input, int expectedPathCost, int expectedPositionsOnCheapestPaths)
    {
        Assert.That(PathSolver.FindCheapestPathCost(input), Is.EqualTo(expectedPathCost));
        Assert.That(PathSolver.FindPositionsAlongCheapestPathsCount(input),
            Is.EqualTo(expectedPositionsOnCheapestPaths));
    }
}