using NUnit.Framework;

namespace Day2_RedNoseReports.Tests;

[TestFixture]
[TestOf(typeof(ReportAnalyzer))]
public class ReportAnalyzerTests
{
    [TestCase("7 6 4 2 1", false, true)]
    [TestCase("1 2 7 8 9", false, false)]
    [TestCase("9 7 6 2 1", false, false)]
    [TestCase("1 3 2 4 5", false, false)]
    [TestCase("8 6 4 4 1", false, false)]
    [TestCase("1 3 6 7 9", false, true)]

    [TestCase("7 6 4 2 1", true, true)]
    [TestCase("1 2 7 8 9", true, false)]
    [TestCase("9 7 6 2 1", true, false)]
    [TestCase("1 3 2 4 5", true, true)]
    [TestCase("8 6 4 4 1", true, true)]
    [TestCase("1 3 6 7 9", true, true)]

    [TestCase("1 1 2 3", true, true, Description = "First two repeat")]
    [TestCase("1 2 3 3", true, true, Description = "Last two repeat")]
    [TestCase("5 4 6 7", true, true, Description = "Second Value is less than first, the rest are ascending.")]
    public void TestExamples(string input, bool allowRemoval, bool isSafe) =>
        Assert.That(ReportAnalyzer.IsSafe(input, allowRemoval), Is.EqualTo(isSafe));

    [TestCase("""
              7 6 4 2 1
              1 2 7 8 9
              9 7 6 2 1
              1 3 2 4 5
              8 6 4 4 1
              1 3 6 7 9
              """, false, 2)]
    [TestCase("""
              7 6 4 2 1
              1 2 7 8 9
              9 7 6 2 1
              1 3 2 4 5
              8 6 4 4 1
              1 3 6 7 9
              """, true, 4)]
    public void TestReports(string input, bool allowRemoval, int expectedCount) =>
        Assert.That(ReportAnalyzer.CalculateSafeReportsCount(input, allowRemoval), Is.EqualTo(expectedCount));

    [TestFixture]
    public sealed class A_Level_Is_Bad_When
    {
        public sealed class It_Is_Not_The_First_One_And
        {
            [TestCase(0, 0, false)]
            [TestCase(1, 1, false)]
            public void It_Is_Equal_To_The_Previous_Level(int previousLevel, int currentLevel, bool isAscending) =>
                Assert.That(ReportAnalyzer.IsGoodLevel(previousLevel, currentLevel, isAscending), Is.EqualTo(false));

            [TestCase(0, 4, true)]
            [TestCase(4, 0, false)]
            public void
                The_Difference_From_The_Previous_Level_Is_More_Than_Three(int previousLevel, int currentLevel,
                    bool isAscending) =>
                Assert.That(ReportAnalyzer.IsGoodLevel(previousLevel, currentLevel, isAscending), Is.EqualTo(false));

            [TestCase(0, 1, false)]
            [TestCase(1, 0, true)]
            public void
                It_Breaks_With_An_Ascending_Or_Descending_Pattern(int previousLevel, int currentLevel,
                    bool isAscending) =>
                Assert.That(ReportAnalyzer.IsGoodLevel(previousLevel, currentLevel, isAscending), Is.EqualTo(false));
        }
    }

    [TestFixture]
    public sealed class Levels_Are_Safe_When
    {
        [TestFixture]
        public sealed class Numbers_Sequentially_Differ_By_More_Than_Zero_And_Less_Than_Four_And
        {
            [Test]
            [TestCase("2 1")]
            [TestCase("5 4 3 2 1")]
            [TestCase("3 2 1 0")]
            public void All_Numbers_Are_Decreasing(string input) =>
                Assert.That(ReportAnalyzer.IsSafe(input), Is.True);

            [Test]
            [TestCase("0 1 2 3 4 5")]
            [TestCase("0 2 4 6 8 10")]
            [TestCase("0 3 6 9 12 15")]
            [TestCase("0 1 3 6 7 9")]
            public void All_Numbers_Are_Increasing(string input) =>
                Assert.That(ReportAnalyzer.IsSafe(input), Is.True);
        }
    }

    public sealed class Levels_Are_Not_Safe_When
    {
        [Test]
        [TestCase("0 0")]
        [TestCase("1 1")]
        [TestCase("0 0 1 2")]
        [TestCase("0 0 1 1")]
        public void Adjacent_Numbers_Are_The_Same(string input) => Assert.That(ReportAnalyzer.IsSafe(input), Is.False);

        [Test]
        [TestCase("0 4")]
        [TestCase("4 0")]
        [TestCase("0 1 2 8")]
        [TestCase("0 1 4 0")]
        public void Adjacent_Numbers_Are_More_Than_Three_Apart(string input) =>
            Assert.That(ReportAnalyzer.IsSafe(input), Is.False);

        [Test]
        [TestCase("0 1 2 3 2 1 0")]
        [TestCase("3 2 1 0 1 2 3")]
        public void Numbers_Do_Not_All_Increase_Or_Do_Not_All_Decrease(string input) =>
            Assert.That(ReportAnalyzer.IsSafe(input), Is.False);
    }
}