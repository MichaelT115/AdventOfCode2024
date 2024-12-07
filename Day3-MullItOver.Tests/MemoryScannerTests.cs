using NUnit.Framework;

namespace Day3_MullItOver.Tests;

[TestFixture]
[TestOf(typeof(MemoryScanner))]
public class MemoryScannerTests
{
    [Test]
    [TestCase("mul(4*")]
    [TestCase("mul(6,9!")]
    [TestCase("?(12,34)")]
    [TestCase("mul ( 2 , 4 )")]
    public void Does_Not_Multiply_Improperly_Formated_Numbers(string input) =>
        Assert.That(MemoryScanner.Scan(input), Is.Zero);

    [Test]
    [TestCase("mul(0,0)", 0)]
    [TestCase("mul(1,1)", 1)]
    [TestCase("mul(12,1)", 12)]
    [TestCase("mul(123,1)", 123)]
    [TestCase("mul(1,12)", 12)]
    [TestCase("mul(1,123)", 123)]
    [TestCase("mul(10,10)", 100)]
    [TestCase("mul(100,100)", 10_000)]

    [TestCase("mul(44,46)", 2024)]
    [TestCase("mul(123,4)", 492)]
    public void Multiplies_Properly_Formated_Numbers(string input, int expected) =>
        Assert.That(MemoryScanner.Scan(input), Is.EqualTo(expected));

    [TestCase("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", 161)]
    public void Multiplies_And_Sums_All_Properly_Formated_Numbers(string input, int expected) =>
        Assert.That(MemoryScanner.Scan(input), Is.EqualTo(expected));

    [Test]
    [TestCase("do()mul(0,0)", 0)]
    [TestCase("do()mul(1,1)", 1)]
    [TestCase("do()mul(12,1)", 12)]
    [TestCase("do()mul(123,1)", 123)]
    [TestCase("do()mul(1,12)", 12)]
    [TestCase("do()mul(1,123)", 123)]
    [TestCase("do()mul(10,10)", 100)]
    [TestCase("do()mul(100,100)", 10_000)]

    [TestCase("don't()do()mul(0,0)", 0)]
    [TestCase("don't()do()mul(1,1)", 1)]
    [TestCase("don't()do()mul(12,1)", 12)]
    [TestCase("don't()do()mul(123,1)", 123)]
    [TestCase("don't()do()mul(1,12)", 12)]
    [TestCase("don't()do()mul(1,123)", 123)]
    [TestCase("don't()do()mul(10,10)", 100)]
    [TestCase("don't()do()mul(100,100)", 10_000)]
    public void Multiplies_Properly_Formated_Numbers_That_Occur_After_Do_Conditional(string input, int expected) =>
        Assert.That(MemoryScanner.ScanWithConditionals(input), Is.EqualTo(expected));

    [Test]
    [TestCase("don't()mul(0,0)")]
    [TestCase("don't()mul(1,1)")]
    [TestCase("don't()mul(12,1)")]
    [TestCase("don't()mul(123,1)")]
    [TestCase("don't()mul(1,12)")]
    [TestCase("don't()mul(1,123)")]
    [TestCase("don't()mul(10,10)")]
    [TestCase("don't()do()don't()mul(100,100)")]
    public void Does_Not_Multiply_Properly_Formated_Numbers_That_Occur_After_Dont_Conditional(string input) =>
        Assert.That(MemoryScanner.ScanWithConditionals(input), Is.Zero);

    [TestCase("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48)]
    public void Multiplies_And_Sums_All_Properly_Formated_Numbers_With_Conditionals(string input, int expected) =>
        Assert.That(MemoryScanner.ScanWithConditionals(input), Is.EqualTo(expected));
}