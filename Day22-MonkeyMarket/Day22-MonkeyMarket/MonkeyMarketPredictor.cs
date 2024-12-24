namespace Day22_MonkeyMarket;

public static class MonkeyMarketPredictor
{
    public static long CalculateSecretNumber(long secretNumber, int count)
    {
        for (var i = 0; i < count; ++i)
        {
            secretNumber = CalculateSecretNumber(secretNumber);
        }

        return secretNumber;
    }

    private static long CalculateSecretNumber(long secretNumber)
    {
        const long modulo = 0b_1_00000000_00000000_00000000;

        var a = secretNumber * 0b_1000000;
        secretNumber ^= a;
        secretNumber %= modulo;

        var b = secretNumber / 0b_100000;
        secretNumber ^= b;
        secretNumber %= modulo;

        var c = secretNumber * 0b_1000_00000000;
        secretNumber ^= c;
        secretNumber %= modulo;

        return secretNumber;
    }

    public static void FillOutProfitFromCommands(long secretNumber,
        int count,
        Dictionary<(int first, int second, int third, int fourth), long> dictionary)
    {
        var doneSequences = new HashSet<(int first, int second, int third, int fourth)>();

        var previousPrice = (int)secretNumber % 10;

        secretNumber = CalculateSecretNumber(secretNumber);
        var firstPrice = (int)(secretNumber % 10);
        var firstPriceDifference = firstPrice - previousPrice;
        previousPrice = firstPriceDifference;

        secretNumber = CalculateSecretNumber(secretNumber);
        var secondPrice = (int)(secretNumber % 10);
        var secondPriceDifference = secondPrice - previousPrice;
        previousPrice = secondPrice;

        secretNumber = CalculateSecretNumber(secretNumber);
        var thirdPrice = (int)(secretNumber % 10);
        var thirdPriceDifference = thirdPrice - previousPrice;
        previousPrice = thirdPrice;

        secretNumber = CalculateSecretNumber(secretNumber);
        var fourthPrice = (int)(secretNumber % 10);
        var fourthPriceDifference = fourthPrice - previousPrice;
        previousPrice = fourthPrice;

        (int first, int second, int third, int fourth) key = (firstPriceDifference, secondPriceDifference,
            thirdPriceDifference, fourthPriceDifference);

        if (!dictionary.TryAdd(key, previousPrice))
        {
            dictionary[key] += previousPrice;
        }

        doneSequences.Add(key);

        for (var i = 5; i <= count; ++i)
        {
            secretNumber = CalculateSecretNumber(secretNumber);
            var newPrice = (int)(secretNumber % 10);

            key.first = key.second;
            key.second = key.third;
            key.third = key.fourth;
            key.fourth = newPrice - previousPrice;

            previousPrice = newPrice;

            if (doneSequences.Contains(key))
            {
                continue;
            }

            if (!dictionary.TryAdd(key, newPrice))
            {
                dictionary[key] += newPrice;
            }

            doneSequences.Add(key);
        }
    }
}