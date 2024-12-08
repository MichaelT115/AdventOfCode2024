using Day5_PrintQueue;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var inputParts = input.Split("\r\n\r\n");

var orderingRules = ParseOrderingRules(inputParts[0].AsSpan());
var printQueues = ParseQueues(inputParts[1]);

var printQueuer = new PrintQueuer(orderingRules);


Console.WriteLine($"Result: {printQueuer.ProcessQueues(printQueues)}");
Console.WriteLine($"Result (w/ Corrections): {printQueuer.ProcessQueuesWithCorrections(printQueues)}");
return;

int[][] ParseQueues(ReadOnlySpan<char> queuesText)
{
    var queues = new List<int[]>();
    foreach (var queueText in queuesText.EnumerateLines())
    {
        var queue = new List<int>();
        foreach (var pageNumber in queueText.Split(","))
        {
            var (start, length) = pageNumber.GetOffsetAndLength(queueText.Length);
            queue.Add(int.Parse(queueText.Slice(start, length)));
        }
        queues.Add(queue.ToArray());
    }

    return queues.ToArray();
}

(int comesBefore, int comesAfter)[] ParseOrderingRules(ReadOnlySpan<char> pageOrderingRulesText)
{
    var pageOrderingRules = new List<(int comesBefore, int comesAfter)>();
    foreach (var ruleText in pageOrderingRulesText.EnumerateLines())
    {
        var separatorIndex = ruleText.IndexOf('|');
        pageOrderingRules.Add((int.Parse(ruleText[..separatorIndex]), int.Parse(ruleText[(separatorIndex + 1)..])));
    }

    return pageOrderingRules.ToArray();
}