using NUnit.Framework;

namespace Day4_CeresSearch.Tests;

[TestFixture]
[TestOf(typeof(WordSearchSolver))]
public class WordSearchSolverTests
{
    [Test]
    [TestCase("")]
    [TestCase(".")]
    [TestCase("....")]
    [TestCase("""
              ....
              ....
              ....
              """)]
    public void Empty_Grids_Have_Return_Zero(string input) =>
        Assert.That(WordSearchSolver.Solve(input), Is.Zero);

    [TestCase("XMAS", 1)]
    [TestCase("XMASXMAS", 2)]
    [TestCase("XMAS.XMAS", 2)]
    [TestCase("XMASXMASXMAS", 3)]
    [TestCase("XMASXMAS..", 2)]
    [TestCase("""
              XMAS
              XMAS
              """, 2)]
    public void Forward_Horizontal_XMAS_Is_Counted(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));

    [TestCase("SAMX", 1)]
    [TestCase("SAMXSAMX", 2)]
    [TestCase("SAMX.SAMX", 2)]
    [TestCase("SAMXSAMXSAMX", 3)]
    [TestCase("SAMXSAMX..", 2)]
    [TestCase("""
              SAMX
              SAMX
              ....
              """, 2)]
    public void Backwards_Horizontal_XMAS_Is_Counted(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));

    [TestCase("""
              X
              M
              A
              S
              """, 1)]
    [TestCase("""
              XX
              MM
              AA
              SS
              """, 2)]
    public void Forwards_Vertical_XMAS_Is_Counted(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));

    [TestCase("""
              S
              A
              M
              X
              """, 1)]
    [TestCase("""
              SS
              AA
              MM
              XX
              """, 2)]
    public void Backwards_Vertical_XMAS_Is_Counted(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));

    [TestCase("""
              X...
              .M..
              ..A.
              ...S
              """, 1)]
    [TestCase("""
              XX...
              .MM..
              ..AA.
              ...SS
              """, 2)]
    [TestCase("""
              ...S
              ..A.
              .M..
              X...
              """, 1)]
    [TestCase("""
              ...SS
              ..AA.
              .MM..
              XX...
              """, 2)]
    public void Forwards_Diagonal_XMAS_Is_Counted(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));

    [TestCase("""
              S...
              .A..
              ..M.
              ...X
              """, 1)]
    [TestCase("""
              SS...
              .AA..
              ..MM.
              ...XX
              """, 2)]
    [TestCase("""
              ...X
              ..M.
              .A..
              S...
              """, 1)]
    [TestCase("""
              ...XX
              ..MM.
              .AA..
              SS...
              """, 2)]
    public void Backwards_Diagonal_XMAS_Is_Counted(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));

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
              """, 18)]
    [TestCase("""
              ....XXMAS.
              .SAMXMS...
              ...S..A...
              ..A.A.MS.X
              XMASAMX.MM
              X.....XA.A
              S.S.S.S.SS
              .A.A.A.A.A
              ..M.M.M.MM
              .X.X.XMASX
              """, 18)]
    public void Test_Examples(string input, int expected) =>
        Assert.That(WordSearchSolver.Solve(input), Is.EqualTo(expected));
}