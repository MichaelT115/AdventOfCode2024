using Day7_BridgeRepair;

using var streamReader = new StreamReader(args[0]);

var sum = 0ul;
var sumWithConcatenations = 0ul;

while (streamReader.Peek() >= 0)
{
    var equation = streamReader.ReadLine().AsSpan();
    if (Calibrator.CanEquationBeTrue(equation, out var testValue))
    {
        sum += testValue;
    }

    if (Calibrator.CanEquationBeTrueWithConcatOperation(equation, out testValue))
    {
        sumWithConcatenations += testValue;
    }
}

Console.WriteLine($"Result: {sum}");
Console.WriteLine($"Result w/Concatenations: {sumWithConcatenations}");
