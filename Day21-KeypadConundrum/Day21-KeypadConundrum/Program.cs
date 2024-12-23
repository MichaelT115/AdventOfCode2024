using System.Diagnostics;
using Day21_KeypadConundrum;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd().AsSpan();

// Part 1
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    var commandSequenceFinder = Keypads.BuildDirectionalKeypad();
    for (var i = 0; i < 1; i++)
    {
        commandSequenceFinder = Keypads.BuildDirectionalKeypad(commandSequenceFinder);
    }

    commandSequenceFinder = Keypads.BuildNumericKeypad(commandSequenceFinder);

    var complexitySum = 0L;
    foreach (var line in input.EnumerateLines())
    {
        var complexity = commandSequenceFinder.GetComplexity(line.ToString());

        // Console.WriteLine($"{line}: {complexity}");

        complexitySum += complexity;
    }

    Console.WriteLine($"Result: {complexitySum} (Time = {stopwatch.ElapsedMilliseconds}ms)\n");
}

// Part 2
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    var commandSequenceFinder = Keypads.BuildDirectionalKeypad();
    for (var i = 0; i < 24; ++i)
    {
        commandSequenceFinder = Keypads.BuildDirectionalKeypad(commandSequenceFinder);
    }

    commandSequenceFinder = Keypads.BuildNumericKeypad(commandSequenceFinder);

    var complexitySum = 0L;
    foreach (var line in input.EnumerateLines())
    {
        var complexity = commandSequenceFinder.GetComplexity(line.ToString());

        // Console.WriteLine($"{line}: {complexity}");

        complexitySum += complexity;
    }

    Console.WriteLine($"Result (Part 2): {complexitySum} (Time = {stopwatch.ElapsedMilliseconds}ms)\n");
}