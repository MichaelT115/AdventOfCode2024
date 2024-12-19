using NUnit.Framework;

namespace Day17_ChronospatialComputer.Tests;

[TestFixture]
[TestOf(typeof(ChronospatialComputer))]
public class ChronospatialComputerTests
{
    [TestFixture]
    public class Running_An_Empty_Program
    {
        [Test]
        public void Does_Not_Change_Registers([Values(0, int.MaxValue)] int registerA,
            [Values(0, int.MaxValue)] int registerB, [Values(0, int.MaxValue)] int registerC)
        {
            var chronospatialComputer = new ChronospatialComputer
            {
                RegisterA = registerA,
                RegisterB = registerB,
                RegisterC = registerC
            };

            chronospatialComputer.RunProgram([]);

            Assert.That(chronospatialComputer.Registers, Is.EqualTo((registerA, registerB, registerC)));
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 2, 3)]
        public void Outputs_An_Empty_List([Values(0, int.MaxValue)] int registerA,
            [Values(0, int.MaxValue)] int registerB, [Values(0, int.MaxValue)] int registerC)
        {
            var chronospatialComputer = new ChronospatialComputer
            {
                RegisterA = registerA,
                RegisterB = registerB,
                RegisterC = registerC
            };

            var output = chronospatialComputer.RunProgram([]);

            Assert.That(output, Is.Empty);
        }
    }

    [Test]
    [TestCase(0, 0, 0, 1, 0, 0, 0)]
    [TestCase(4, 0, 0, 1, 2, 0, 0)]
    [TestCase(4, 0, 0, 2, 1, 0, 0)]
    [TestCase(5, 0, 0, 2, 1, 0, 0)]
    [TestCase(8, 0, 0, 3, 1, 0, 0)]
    [TestCase(8, 0, 0, 3, 1, 0, 0)]
    [TestCase(8, 0, 0, 4, 0, 0, 0)]
    [TestCase(8, 2, 0, 5, 2, 2, 0)]
    [TestCase(8, 0, 2, 6, 2, 0, 2)]
    public void adv_Instruction_sets_Register_A_to_Division_of_Register_A_and_2_to_the_Power_of_the_Combo_Operand(
        int registerA, int registerB, int registerC, byte operand, int expectedA, int expectedB, int expectedC)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        chronospatialComputer.RunProgram([0, operand]);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo((expectedA, expectedB, expectedC)));
    }

    [Test]
    [TestCase(0, 0, 0, 0, 0, 0, 0)]
    [TestCase(0, 1, 0, 0, 0, 1, 0)]
    [TestCase(0, 0, 0, 1, 0, 1, 0)]
    [TestCase(0, 1, 0, 2, 0, 0b_11, 0)]
    public void bxl_Instruction_sets_Register_B_to_Bitwise_XOR_of_Register_B_And_Literal_Operand(int registerA,
        int registerB, int registerC, byte operand, int expectedA, int expectedB, int expectedC)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        chronospatialComputer.RunProgram([1, operand]);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo((expectedA, expectedB, expectedC)));
    }

    [Test]
    [TestCase(0, 0, 0, 0, 0, 0, 0)]
    [TestCase(0, 1, 0, 0, 0, 0, 0)]
    [TestCase(0, 0, 0, 1, 0, 1, 0)]
    [TestCase(0, 0, 0, 2, 0, 2, 0)]
    [TestCase(0, 0, 0, 3, 0, 3, 0)]
    [TestCase(1, 0, 0, 4, 1, 1, 0)]
    [TestCase(9, 0, 0, 4, 9, 1, 0)]
    [TestCase(0, 0, 0, 5, 0, 0, 0)]
    [TestCase(0, 1, 0, 5, 0, 1, 0)]
    [TestCase(0, 9, 0, 5, 0, 1, 0)]
    [TestCase(0, 0, 1, 6, 0, 1, 1)]
    [TestCase(0, 0, 9, 6, 0, 1, 9)]
    public void bst_Instruction_sets_Register_B_to_the_Modulo_of_the_Combo_Operand_And_8(int registerA, int registerB,
        int registerC, byte operand, int expectedA, int expectedB, int expectedC)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        chronospatialComputer.RunProgram([2, operand]);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo((expectedA, expectedB, expectedC)));
    }

    [TestFixture]
    public class Jnx_Instruction
    {
        [TestFixture]
        public class When_Register_A_Is_Zero
        {
            [Test]
            public void Does_Not_Change_Registers([Values(0, 1, int.MaxValue)] int registerB,
                [Values(0, 1, int.MaxValue)] int registerC, [Range(0, 8)] byte operand)
            {
                var chronospatialComputer = new ChronospatialComputer
                {
                    RegisterA = 0,
                    RegisterB = registerB,
                    RegisterC = registerC
                };

                chronospatialComputer.RunProgram([3, operand]);

                Assert.That(chronospatialComputer.Registers, Is.EqualTo((0, registerB, registerC)));
            }
        }

        [TestFixture]
        public class When_Register_A_Is_Not_Zero
        {
            [Test]
            public void Moves_Instruction_Pointer_To_Literal_Operand([Values(1, int.MaxValue)] int registerA)
            {
                var chronospatialComputer = new ChronospatialComputer
                {
                    RegisterA = registerA,
                    RegisterB = 0,
                    RegisterC = 0
                };

                var output = chronospatialComputer.RunProgram([3, 3, 0, 5, 0]);

                Assert.That(output, Is.EqualTo(new[] { 0 }));
            }
        }
    }

    [Test]
    [TestCase(0, 0, 0, 0, 0, 0, 0)]
    [TestCase(0, 1, 0, 0, 0, 1, 0)]
    [TestCase(0, 0, 1, 0, 0, 1, 1)]
    [TestCase(0, 1, 1, 0, 0, 0, 1)]
    [TestCase(0, 2, 1, 0, 0, 3, 1)]
    public void bxc_Instruction_sets_Register_B_to_Bitwise_XOR_Register_B_and_Register_C(int registerA, int registerB,
        int registerC, byte operand, int expectedA, int expectedB, int expectedC)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        chronospatialComputer.RunProgram([4, operand]);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo((expectedA, expectedB, expectedC)));
    }

    [Test]
    [TestCase(8, 9, 10, 0, 0)]
    [TestCase(8, 9, 10, 1, 1)]
    [TestCase(8, 9, 10, 2, 2)]
    [TestCase(8, 9, 10, 3, 3)]
    [TestCase(8, 9, 10, 4, 0)]
    [TestCase(8, 9, 10, 5, 1)]
    [TestCase(8, 9, 10, 6, 2)]
    public void out_Instruction_Outputs_the_Combo_Operand_Modulo_8(int registerA, int registerB,
        int registerC, byte operand, int expectedOutput)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        var output = chronospatialComputer.RunProgram([5, operand]);

        Assert.That(output, Is.EqualTo(new[] { expectedOutput }));
    }

    [Test]
    [TestCase(0, 0, 0, 1, 0, 0, 0)]
    [TestCase(4, 0, 0, 1, 4, 2, 0)]
    [TestCase(4, 0, 0, 2, 4, 1, 0)]
    [TestCase(5, 0, 0, 2, 5, 1, 0)]
    [TestCase(8, 0, 0, 3, 8, 1, 0)]
    [TestCase(8, 0, 0, 4, 8, 0, 0)]
    [TestCase(8, 2, 0, 5, 8, 2, 0)]
    [TestCase(8, 0, 2, 6, 8, 2, 2)]
    public void bdv_Instruction_sets_Register_B_to_Division_of_Register_A_and_2_to_the_Power_of_the_Combo_Operand(
        int registerA, int registerB, int registerC, byte operand, int expectedA, int expectedB, int expectedC)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        chronospatialComputer.RunProgram([6, operand]);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo((expectedA, expectedB, expectedC)));
    }

    [Test]
    [TestCase(0, 0, 0, 1, 0, 0, 0)]
    [TestCase(4, 0, 0, 1, 4, 0, 2)]
    [TestCase(4, 0, 0, 2, 4, 0, 1)]
    [TestCase(5, 0, 0, 2, 5, 0, 1)]
    [TestCase(8, 0, 0, 3, 8, 0, 1)]
    [TestCase(8, 0, 0, 4, 8, 0, 0)]
    [TestCase(8, 2, 0, 5, 8, 2, 2)]
    [TestCase(8, 0, 2, 6, 8, 0, 2)]
    public void cdv_Instruction_sets_Register_A_to_Division_of_Register_A_and_2_to_the_Power_of_the_Combo_Operand(
        int registerA, int registerB, int registerC, byte operand, int expectedA, int expectedB, int expectedC)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = registerA,
            RegisterB = registerB,
            RegisterC = registerC
        };

        chronospatialComputer.RunProgram([7, operand]);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo((expectedA, expectedB, expectedC)));
    }

    [TestCaseSource(nameof(_exampleTestCases))]
    public void Example_Programs(
        (int registerA, int registerB, int registerC) startingRegisters, byte[] program,
        (int registerA, int registerB, int registerC) expectedRegisters,
        int[] expectedOutput)
    {
        var chronospatialComputer = new ChronospatialComputer
        {
            RegisterA = startingRegisters.registerA,
            RegisterB = startingRegisters.registerB,
            RegisterC = startingRegisters.registerC
        };

        var output = chronospatialComputer.RunProgram(program);

        Assert.That(chronospatialComputer.Registers, Is.EqualTo(expectedRegisters));
        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    private static object[] _exampleTestCases =
    [
        new object[] { (0, 0, 9), new byte[] { 2, 6 }, (0, 1, 9), System.Array.Empty<int>() },
        new object[] { (10, 0, 0), new byte[] { 5, 0, 5, 1, 5, 4 }, (10, 0, 0), new[] { 0, 1, 2 } },
        new object[]
            { (2024, 0, 0), new byte[] { 0, 1, 5, 4, 3, 0 }, (0, 0, 0), new[] { 4, 2, 5, 6, 7, 7, 7, 7, 3, 1, 0 } },
        new object[] { (0, 29, 0), new byte[] { 1, 7 }, (0, 26, 0), System.Array.Empty<int>() },
        new object[] { (0, 2024, 43690), new byte[] { 4, 0 }, (0, 44354, 43690), System.Array.Empty<int>() },
        new object[]
        {
            (46337277, 0, 0), new byte[] { 2, 4, 1, 1, 7, 5, 4, 4, 1, 4, 0, 3, 5, 5, 3, 0 }, (0, 7, 0),
            new[] { 7, 4, 2, 0, 5, 0, 5, 3, 7 }
        }
    ];
}