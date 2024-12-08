using NUnit.Framework;

namespace Day4_CeresSearch.Tests;

[TestFixture]
[TestFixture]
[TestOf(typeof(WordSearchXMasSolver))]
public class WordSearchXMasSolverTests
{
    [TestCase("""
              M.M
              .A.
              S.S
              """, 1)]
    [TestCase("""
              S.S
              .A.
              M.M
              """, 1)]
    [TestCase("""
              S.M
              .A.
              S.M
              """, 1)]
    [TestCase("""
              M.S
              .A.
              M.S
              """, 1)]
    public void Counts_XMas_Patterns(string input, int expected)
        => Assert.That(WordSearchXMasSolver.Solve(input), Is.EqualTo(expected));

    [TestCase("""
              MMMSXXMASM
              MSAMXMSMSA
              AMXSXMAAMM
              MSAMASMSMX
              XMASAMXAMM
              XXAMMXXAMA
              SMSMSASXSS
              SAXAMASAAA
              MAMMMXMMMM
              MXMXAXMASX
              """, 9)]
    [TestCase("""
              .M.S......
              ..A..MSMS.
              .M.S.MAA..
              ..A.ASMSM.
              .M.S.M....
              ..........
              S.S.S.S.S.
              .A.A.A.A..
              M.M.M.M.M.
              ..........
              """, 9)]
    public void Test_Examples(string input, int expected) =>
        Assert.That(WordSearchXMasSolver.Solve(input), Is.EqualTo(expected));
}