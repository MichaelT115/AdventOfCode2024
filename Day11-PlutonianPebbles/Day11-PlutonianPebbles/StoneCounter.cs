namespace Day11_PlutonianPebbles;

public static class StoneCounter
{
    private static readonly Dictionary<ulong, ulong> ReplacementMemo = new() { { 0, 1 } };
    private static readonly Dictionary<ulong, ulong> NewValueMemo = new();

    public static ulong CountStones(ulong[] stones, int blinks) =>
        CountStones(stones, blinks, new Dictionary<(int, ulong), ulong>());

    private static ulong CountStones(ulong[] stones, int blinks,
        Dictionary<(int blinks, ulong stoneValue), ulong> memo)
    {
        if (blinks == 0)
        {
            return (ulong)stones.Length;
        }

        var total = 0ul;
        foreach (var stone in stones)
        {
            if (memo.TryGetValue((blinks, stone), out var memoCount))
            {
                total += memoCount;
            }
            else
            {
                var count = CountStones(ProcessStone(stone), blinks - 1, memo);
                memo.Add((blinks, stone), count);
                total += count;
            }
        }

        return total;
    }

    public static ulong[] ProcessStone(ulong stone)
    {
        ulong newStone;
        if (ReplacementMemo.TryGetValue(stone, out var replacementStone))
        {
            return NewValueMemo.TryGetValue(stone, out newStone)
                ? [replacementStone, newStone]
                : [replacementStone];
        }

        var digitCount = (ulong)Math.Floor(Math.Log10(stone) + 1);
        if (digitCount % 2 != 0)
        {
            replacementStone = stone * 2024;
            ReplacementMemo.Add(stone, replacementStone);
            return [replacementStone];
        }

        var divider = 10ul;
        var pivot = digitCount / 2ul;
        for (var j = 1ul; j < pivot; ++j)
        {
            divider *= 10;
        }

        replacementStone = stone / divider;
        newStone = stone % divider;

        ReplacementMemo.Add(stone, replacementStone);
        NewValueMemo.Add(stone, newStone);

        return [replacementStone, newStone];
    }
}