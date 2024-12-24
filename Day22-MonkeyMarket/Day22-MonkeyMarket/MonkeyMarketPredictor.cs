namespace Day22_MonkeyMarket;

public static class MonkeyMarketPredictor
{
    public static long CalculateSecretNumber(long secretNumber, int count = 1)
    {
        for (var i = 0; i < count; ++i)
        {
            secretNumber = GetNextSecretNumber(secretNumber);
        }
        return secretNumber;
    }

    private static long GetNextSecretNumber(long secretNumber)
    {
        const long modulo = 16777216;

        var a = secretNumber * 64;
        secretNumber ^= a;
        secretNumber %= modulo;

        var b = secretNumber / 32;
        secretNumber ^= b;
        secretNumber %= modulo;

        var c = secretNumber * 2048;
        secretNumber ^= c;
        secretNumber %= modulo;

        return secretNumber;
    }
}