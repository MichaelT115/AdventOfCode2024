using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var (locks, keys) = ParseInput(input);

var fittingLockKeyPairs = keys.Sum(key => locks.Count(@lock => DoesKeyFitLock(key, @lock)));

Console.WriteLine($"Result: {fittingLockKeyPairs}");
return;

static (List<int[]> locks, List<int[]> keys) ParseInput(ReadOnlySpan<char> readOnlySpan)
{
    var locksList = new List<int[]>();
    var keysList = new List<int[]>();

    int[] schematic = [];
    var isNewSchematic = true;
    var isLock = false;
    foreach (var line in readOnlySpan.EnumerateLines())
    {
        if (line.IsEmpty)
        {
            isNewSchematic = true;
            if (isLock)
            {
                locksList.Add(schematic);
            }
            else
            {
                keysList.Add(schematic);
            }
            continue;
        }
    
        if (isNewSchematic)
        {
            isNewSchematic = false;
            isLock = line is "#####";
            schematic = [0, 0, 0, 0, 0];
        }

        for (var i = 0; i < 5; ++i)
        {
            if (line[i] == '#')
            {
                ++schematic[i];
            }
        }
    }

    if (isLock)
    {
        locksList.Add(schematic);
    }
    else
    {
        keysList.Add(schematic);
    }

    return (locksList, keysList);
}

static bool DoesKeyFitLock(int[] key, int[] @lock)
{
    for (var i = 0; i < 5; ++i)
    {
        if (key[i] + @lock[i] > 7)
        {
            return false;
        }
    }

    return true;
}