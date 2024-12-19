using Microsoft.VisualBasic;

namespace Day17_ChronospatialComputer;

public sealed class ChronospatialComputer
{
    public required int RegisterA;
    public required int RegisterB;
    public required int RegisterC;

    public (int registerA, int registerB, int registerC) Registers => (RegisterA, RegisterB, RegisterC);

    public int[] RunProgram(ReadOnlySpan<byte> program)
    {
        var output = new List<int>();

        var operationCount = 0;
        
        var instructionPointer = 0;
        while (instructionPointer < program.Length - 1)
        {
            var operand = program[instructionPointer + 1];
            
            Console.WriteLine(
                $"{operationCount++}:\t{{ {program[instructionPointer]}, {operand} }}\tA: {RegisterA}\tB: {RegisterB}\tC: {RegisterC}\tCurrent Output: {string.Join(',', output)}");

            switch (program[instructionPointer])
            {
                case 0:
                    RegisterA = (int)(RegisterA / Math.Pow(2, GetComboOperandValue(operand)));
                    instructionPointer += 2;
                    break;
                case 1:
                    RegisterB ^= operand;
                    instructionPointer += 2;
                    break;
                case 2:
                    RegisterB = GetComboOperandValue(operand) % 8;
                    instructionPointer += 2;
                    break;
                case 3:
                    if (RegisterA == 0)
                    {
                        instructionPointer += 2;
                    }
                    else
                    {
                        instructionPointer = operand;
                    }

                    break;
                case 4:
                    RegisterB ^= RegisterC;
                    instructionPointer += 2;
                    break;
                case 5:
                    output.Add(GetComboOperandValue(operand) % 8);
                    instructionPointer += 2;
                    break;
                case 6:
                    RegisterB = (int)(RegisterA / Math.Pow(2, GetComboOperandValue(operand)));
                    instructionPointer += 2;
                    break;
                case 7:
                    RegisterC = (int)(RegisterA / Math.Pow(2, GetComboOperandValue(operand)));
                    instructionPointer += 2;
                    break;
                default: throw new Exception("Unknown Operation");
            }
        }

        return output.ToArray();

        int GetComboOperandValue(byte operand) =>
            operand switch
            {
                <= 3 => operand,
                4 => RegisterA,
                5 => RegisterB,
                6 => RegisterC,
                _ => throw new Exception()
            };
    }
}