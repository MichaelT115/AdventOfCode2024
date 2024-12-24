using Day24_CrossedWires;

using var streamReader = new StreamReader(args[0]);

var lines = streamReader.ReadToEnd().Split('\n');

var registerValues = new Dictionary<string, bool>();
var operations = new Queue<(string registerA, Operation operation, string registerB, string targetRegister)>();

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

    operations.Enqueue((registerA, operation, registerB, targetRegister));
}

while (operations.TryDequeue(out var operation))
{
    if (!registerValues.TryGetValue(operation.registerA, out var registerAValue) || !registerValues.TryGetValue(operation.registerB, out var registerBValue))
    {
        operations.Enqueue(operation);
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

var output = 0L;
var zIndex = 0;
while (registerValues.TryGetValue($"z{zIndex:00}", out var registerValue))
{
    output |= (registerValue ? 1L : 0L) << zIndex;
    ++zIndex;
}

Console.WriteLine($"Result: {output}");