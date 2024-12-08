using NUnit.Framework;

namespace Day7_BridgeRepair.Tests;

[TestFixture]
[TestOf(typeof(Calibrator))]
public class CalibratorTests
{
    [TestFixture]
    public class A_Calibration_Equation_Is_Invalid_When
    {
        [Test]
        [TestCase("1: 0 0")]
        [TestCase("3: 1 1")]
        [TestCase("4: 1 2")]
        public void There_Are_Two_Numbers_That_When_Added_Or_Multiplied_Together_Does_Not_Equal_The_Test_Value(
            string input) =>
            Assert.That(Calibrator.CanEquationBeTrue(input, out _), Is.False);
    }

    [TestFixture]
    public class A_Calibration_Equation_Is_Valid_When
    {
        [Test]
        [TestCase("0: 0 0")]
        [TestCase("2: 1 1")]
        [TestCase("3: 1 1 1")]
        public void The_Numbers_Added_Together_Equal_The_Test_Value(string input) =>
            Assert.That(Calibrator.CanEquationBeTrue(input, out _), Is.True);

        [Test]
        [TestCase("0: 0 0")]
        [TestCase("1: 1 1")]
        [TestCase("8: 2 2 2")]
        public void The_Numbers_Multiplied_Together_Equal_The_Test_Value(string input) =>
            Assert.That(Calibrator.CanEquationBeTrue(input, out _), Is.True);
        
        [Test]
        [TestCase("0: 0 0")]
        [TestCase("11: 1 1")]
        [TestCase("222: 2 2 2")]
        [TestCase("123456789: 0 1 2 3 4 5 6 7 8 9")]
        public void The_Numbers_Concatenated_Together_Equal_The_Test_Value(string input) =>
            Assert.That(Calibrator.CanEquationBeTrueWithConcatOperation(input, out _), Is.True);
    }

    [TestCase("190: 10 19", true)]
    [TestCase("3267: 81 40 27", true)]
    [TestCase("83: 17 5", false)]
    [TestCase("156: 15 6", false)]
    [TestCase("7290: 6 8 6 15", false)]
    [TestCase("161011: 16 10 13", false)]
    [TestCase("192: 17 8 14", false)]
    [TestCase("21037: 9 7 18 13", false)]
    [TestCase("292: 11 6 16 20", true)]
    public void Text_Examples(string input, bool expected) =>
        Assert.That(Calibrator.CanEquationBeTrue(input, out _), Is.EqualTo(expected));
    
    [TestCase("190: 10 19", true)]
    [TestCase("3267: 81 40 27", true)]
    [TestCase("83: 17 5", false)]
    [TestCase("156: 15 6", true)]
    [TestCase("7290: 6 8 6 15", true)]
    [TestCase("161011: 16 10 13", false)]
    [TestCase("192: 17 8 14", true)]
    [TestCase("21037: 9 7 18 13", false)]
    [TestCase("292: 11 6 16 20", true)]
    public void Text_Examples_Part_2(string input, bool expected) =>
        Assert.That(Calibrator.CanEquationBeTrueWithConcatOperation(input, out _), Is.EqualTo(expected));
}