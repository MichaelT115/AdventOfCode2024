using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

// Part 1
{
    var connectionsByComputer = new Dictionary<string, HashSet<string>>();
    foreach (var line in input.EnumerateLines())
    {
        var computerA = line[..2].ToString();
        var computerB = line[3..].ToString();

        if (!connectionsByComputer.TryAdd(computerA, [computerB]))
        {
            connectionsByComputer[computerA].Add(computerB);
        }

        if (!connectionsByComputer.TryAdd(computerB, [computerA]))
        {
            connectionsByComputer[computerB].Add(computerA);
        }
    }

    var foundConnections = new HashSet<string>();
    foreach (var (computer, connectingComputersSet) in connectionsByComputer.Where(computerAndConnections =>
                 computerAndConnections.Key.StartsWith('t')))
    {
        foreach (var connectingComputer in connectingComputersSet)
        {
            var overlaps = connectionsByComputer[connectingComputer].Intersect(connectingComputersSet);
            var key = overlaps.Select(computerInSet => BuildSetKey(computer, connectingComputer, computerInSet));
            foundConnections.UnionWith(key);
        }
    }

    Console.WriteLine($"Result (Part 1): {foundConnections.Count}");
}

// Part 2
{
    var computerToConnectingComputers = new Dictionary<string, HashSet<string>>();
    foreach (var line in input.EnumerateLines())
    {
        var computerA = line[..2].ToString();
        var computerB = line[3..].ToString();

        if (!computerToConnectingComputers.TryAdd(computerA, [computerA, computerB]))
        {
            computerToConnectingComputers[computerA].Add(computerB);
        }

        if (!computerToConnectingComputers.TryAdd(computerB, [computerA, computerB]))
        {
            computerToConnectingComputers[computerB].Add(computerA);
        }
    }

    var biggestGroup = GetHighestSet(computerToConnectingComputers);
    var biggestGroupKey = BuildSetKey(biggestGroup.ToArray());
    Console.WriteLine($"Result (Part 2): {biggestGroupKey}");
}

return;

string BuildSetKey(params string[] computers) => string.Join(',', computers.Order());

HashSet<string> GetHighestSet(Dictionary<string, HashSet<string>> connectionsByComputer)
{
    var biggestGroupSize = int.MinValue;
    HashSet<string> biggestGroup = [];
    foreach (var computer in connectionsByComputer.Keys)
    {
        var group = GetHighestSetInternal(computer, connectionsByComputer, [], []);

        if (biggestGroupSize < group.Count)
        {
            biggestGroupSize = group.Count;
            biggestGroup = group;
        }
    }

    return biggestGroup;
}

HashSet<string> GetHighestSetInternal(string computer, Dictionary<string, HashSet<string>> connectionsByComputer,
    HashSet<string> set, HashSet<string> visited)
{
    if (!visited.Add(computer))
    {
        return set;
    }

    var connectedComputersSet = connectionsByComputer[computer];

    if (!connectedComputersSet.IsSupersetOf(set))
    {
        return set;
    }

    var sets = connectedComputersSet
        .Select(connectedComputer =>
            GetHighestSetInternal(connectedComputer, connectionsByComputer, [computer, ..set], visited))
        .OrderBy(hashSet => hashSet.Count);

    return sets.Last();
}
