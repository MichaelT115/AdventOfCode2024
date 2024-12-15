namespace Day13_ClawContraption;

public static class TokenCalculator
{
    public record struct Vector2(long X, long Y);

    public static bool FindLowestNeededTokens(Vector2 buttonA, Vector2 buttonB, Vector2 prize, long buttonLimit,
        out long lowestNeededTokens, bool withCorrection = false)
    {
        if (withCorrection)
        {
            prize.X += 10000000000000L;
            prize.Y += 10000000000000L;
        }

        var buttonAy = prize.X * buttonA.Y - buttonA.X * prize.Y;
        var buttonBx = buttonB.X * buttonA.Y - buttonA.X * buttonB.Y;
        if (buttonAy % buttonBx != 0)
        {
            lowestNeededTokens = int.MaxValue;
            return false;
        }

        var bPresses = buttonAy / buttonBx;
        if (bPresses > buttonLimit)
        {
            lowestNeededTokens = int.MaxValue;
            return false;
        }

        var buttonBy = prize.Y - buttonB.Y * bPresses;
        if (buttonA.Y != 0 && buttonBy % buttonA.Y != 0)
        {
            lowestNeededTokens = int.MaxValue;
            return false;
        }

        var aPresses = buttonBy / buttonA.Y;
        if (aPresses > buttonLimit)
        {
            lowestNeededTokens = int.MaxValue;
            return false;
        }

        lowestNeededTokens = aPresses * 3 + bPresses * 1;
        return true;
    }
}