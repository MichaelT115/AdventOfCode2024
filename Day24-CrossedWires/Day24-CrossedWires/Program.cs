using System.Text;
using Day24_CrossedWires;

{
    using var streamReader = new StreamReader(args[0]);

    var lines = streamReader.ReadToEnd().Split('\n');

    var registerValues = new Dictionary<string, bool>();
    var operations = new List<(string registerA, Operation operation, string registerB, string targetRegister)>();

    int index;
    for (index = 0; index < lines.Length; ++index)
    {
        if (string.IsNullOrWhiteSpace(lines[index]))
        {
            break;
        }

        var match = Regexes.RegisterRegex().Match(lines[index]);
        registerValues[match.Groups[1].Value] = match.Groups[2].Value == "1";
    }

    ++index;
    for (; index < lines.Length; ++index)
    {
        var match = Regexes.OperationsRegex().Match(lines[index]);

        var registerA = match.Groups[1].Value;
        var operation = match.Groups[2].ValueSpan switch
        {
            "AND" => Operation.And,
            "OR" => Operation.Or,
            "XOR" => Operation.Xor,
            _ => throw new Exception("Invalid Operation")
        };

        var registerB = match.Groups[3].Value;
        var targetRegister = match.Groups[4].Value;

        operations.Add((registerA, operation, registerB, targetRegister));
    }

    var operationsQueue =
        new Queue<(string registerA, Operation operation, string registerB, string targetRegister)>(operations);

    while (operationsQueue.TryDequeue(out var operation))
    {
        if (!registerValues.TryGetValue(operation.registerA, out var registerAValue) ||
            !registerValues.TryGetValue(operation.registerB, out var registerBValue))
        {
            operationsQueue.Enqueue(operation);
            continue;
        }

        var value = operation.operation switch
        {
            Operation.And => registerAValue && registerBValue,
            Operation.Or => registerAValue || registerBValue,
            Operation.Xor => (registerAValue || registerBValue) && !(registerAValue && registerBValue),
            _ => throw new Exception()
        };

        if (!registerValues.TryAdd(operation.targetRegister, value))
        {
            registerValues[operation.targetRegister] = value;
        }
    }

    Console.WriteLine($"Result: {FindRegisterOutput('z', registerValues)}");

    var stringBuilder = new StringBuilder("flowchart TD\n");

    var initialGates = operations.Where(operation =>
        (operation.registerA[0] == 'x' || operation.registerA[0] == 'y') &&
        (operation.registerB[0] == 'x' || operation.registerB[0] == 'y')).ToList();
    var otherGates = operations.Where(operation =>
        !((operation.registerA[0] == 'x' || operation.registerA[0] == 'y') &&
          (operation.registerB[0] == 'x' || operation.registerB[0] == 'y'))).ToList();

    operations = initialGates.OrderBy(operation => int.Parse(operation.registerA[1..]))
        .ThenBy(operation => operation.operation).Concat(otherGates.OrderBy(operation => operation.targetRegister))
        .ToList();
    for (var i = 0; i < operations.Count; i++)
    {
        var (registerA, operation, registerB, targetRegister) = operations[i];
        stringBuilder.Append(
            $"\t{registerA} & {registerB} --> gate{i}[{operation switch
            {
                Operation.And => "AND",
                Operation.Or => "OR",
                Operation.Xor => "XOR",
                _ => throw new Exception()
            }}] --> {targetRegister}\n");
    }

    Console.WriteLine("\nMermaid Diagram Code");
    Console.WriteLine(stringBuilder);

    var xOutput = FindRegisterOutput('x', registerValues);
    var yOutput = FindRegisterOutput('y', registerValues);
    var zOutput = FindRegisterOutput('z', registerValues);
    var expectedZ = xOutput + yOutput;

    var xOutputAsBinary = Convert.ToString(xOutput, 2);
    var yOutputAsBinary = Convert.ToString(yOutput, 2);
    var zOutputAsBinary = Convert.ToString(zOutput, 2);
    var expectedOutputAsBinary = Convert.ToString(xOutput + yOutput, 2);
    
    Console.WriteLine($"X:        0{xOutputAsBinary} ({xOutput})");
    Console.WriteLine($"Y:        0{yOutputAsBinary} ({yOutput})");
    Console.WriteLine($"z:        {zOutputAsBinary} ({zOutput})");
    Console.WriteLine($"Expected: {expectedOutputAsBinary} ({xOutput + yOutput})");

    for (var i = expectedOutputAsBinary.Length - 1; i >= 0; i--)
    {
        if (expectedOutputAsBinary[i] != Convert.ToString(zOutput, 2)[i])
        {
            Console.WriteLine("Difference as: " + (expectedOutputAsBinary.Length - i - 1));
        }
    }
}
return;

long FindRegisterOutput(char registerLabel, Dictionary<string, bool> registerValues)
{
    var output = 0L;
    var index = 0;
    while (registerValues.TryGetValue($"{registerLabel}{index:00}", out var registerValue))
    {
        output |= (registerValue ? 1L : 0L) << index;
        ++index;
    }

    return output;
}