namespace Day7_BridgeRepair;

public static class Calibrator
{
    public static bool CanEquationBeTrue(ReadOnlySpan<char> equation, out ulong testValue)
    {
        var colonIndex = equation.IndexOf(':');
        testValue = ulong.Parse(equation[..colonIndex]);

        var numbersText = equation[(colonIndex + 2)..];
        var numberTextRangesEnumerator = numbersText.Split(' ');

        numberTextRangesEnumerator.MoveNext();
        var (firstNumberOffset, firstNumberLength) =
            numberTextRangesEnumerator.Current.GetOffsetAndLength(numbersText.Length);
        var firstNumber = ulong.Parse(numbersText.Slice(firstNumberOffset, firstNumberLength));

        var currentValues = new List<ulong> { firstNumber };
        var nextValues = new List<ulong>();

        foreach (var numberTextRange in numberTextRangesEnumerator)
        {
            var (offset, length) = numberTextRange.GetOffsetAndLength(numbersText.Length);
            var number = ulong.Parse(numbersText.Slice(offset, length));

            nextValues.Clear();
            foreach (var currentValue in currentValues)
            {
                var additionResult = currentValue + number;
                if (additionResult <= testValue)
                {
                    nextValues.Add(additionResult);
                }

                var multiplicationResult = currentValue * number;
                if (multiplicationResult <= testValue)
                {
                    nextValues.Add(multiplicationResult);
                }
            }

            (currentValues, nextValues) = (nextValues, currentValues);
        }

        foreach (var value in currentValues)
        {
            if (value == testValue)
            {
                return true;
            }
        }

        return false;
    }

    public static bool CanEquationBeTrueWithConcatOperation(ReadOnlySpan<char> equation, out ulong testValue)
    {
        var colonIndex = equation.IndexOf(':');
        testValue = ulong.Parse(equation[..colonIndex]);

        var numbersText = equation[(colonIndex + 2)..];
        var numberTextRangesEnumerator = numbersText.Split(' ');

        numberTextRangesEnumerator.MoveNext();
        var (firstNumberOffset, firstNumberLength) =
            numberTextRangesEnumerator.Current.GetOffsetAndLength(numbersText.Length);
        var firstNumber = ulong.Parse(numbersText.Slice(firstNumberOffset, firstNumberLength));

        var currentValues = new List<ulong> { firstNumber };
        var nextValues = new List<ulong>();

        foreach (var numberTextRange in numberTextRangesEnumerator)
        {
            var (offset, length) = numberTextRange.GetOffsetAndLength(numbersText.Length);
            var number = ulong.Parse(numbersText.Slice(offset, length));

            nextValues.Clear();
            foreach (var currentValue in currentValues)
            {
                var additionResult = currentValue + number;
                if (additionResult <= testValue)
                {
                    nextValues.Add(additionResult);
                }

                var multiplicationResult = currentValue * number;
                if (multiplicationResult <= testValue)
                {
                    nextValues.Add(multiplicationResult);
                }

                var concatenationResult = Concatenate(currentValue, number);
                if (concatenationResult <= testValue)
                {
                    nextValues.Add(concatenationResult);
                }
            }

            (currentValues, nextValues) = (nextValues, currentValues);
        }

        foreach (var value in currentValues)
        {
            if (value == testValue)
            {
                return true;
            }
        }

        return false;
    }

    private static ulong Concatenate(ulong numberA, ulong numberB)
    {
        var result = numberA;
        var numberBTemp = numberB;
        while (numberBTemp> 0)
        {
            result *= 10;
            numberBTemp /= 10;
        }
        return result + numberB;
    }
}