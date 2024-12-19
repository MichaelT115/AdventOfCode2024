namespace Day17_ChronospatialComputer;

public sealed class ChronospatialComputer
{
    public required long RegisterA;
    public required long RegisterB;
    public required long RegisterC;

    public (long registerA, long registerB, long registerC) Registers => (RegisterA, RegisterB, RegisterC);

    public int[] RunProgram(ReadOnlySpan<byte> program)
    {
        var output = new List<int>();
        
        var instructionPointer = 0;
        while (instructionPointer < program.Length - 1)
        {
            var operand = program[instructionPointer + 1];
            switch (program[instructionPointer])
            {
                case 0:
                    RegisterA >>= (int)GetComboOperandValue(operand);
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
                    output.Add((int)(GetComboOperandValue(operand) % 8));
                    instructionPointer += 2;
                    break;
                case 6:
                    RegisterB = RegisterA >> (int)GetComboOperandValue(operand);
                    instructionPointer += 2;
                    break;
                case 7:
                    RegisterC = RegisterA >> (int)GetComboOperandValue(operand);
                    instructionPointer += 2;
                    break;
                default: throw new Exception("Unknown Operation");
            }
        }

        return output.ToArray();

        long GetComboOperandValue(byte operand) =>
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