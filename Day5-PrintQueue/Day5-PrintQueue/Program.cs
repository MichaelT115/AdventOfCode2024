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
        foreach (var pageNumber in queueText.Split(','))
        {
            queue.Add(ParsePageNumber(pageNumber, queueText));
        }

        queues.Add(queue.ToArray());
    }

    return queues.ToArray();
}

int ParsePageNumber(Range range, ReadOnlySpan<char> queuesText)
{
    var (start, length) = range.GetOffsetAndLength(queuesText.Length);
    return int.Parse(queuesText.Slice(start, length));
}

(int, int )[] ParseOrderingRules(ReadOnlySpan<char> pageOrderingRulesText)
{
    var pageOrderingRules = new List<(int comesBefore, int comesAfter)>();
    foreach (var ruleText in pageOrderingRulesText.EnumerateLines())
    {
        pageOrderingRules.Add(ParsePageOrderingRule(ruleText));
    }

    return pageOrderingRules.ToArray();
}

(int, int) ParsePageOrderingRule(ReadOnlySpan<char> pageOrderingRuleText)
{
    var separatorIndex = pageOrderingRuleText.IndexOf('|');
    return (int.Parse(pageOrderingRuleText[..separatorIndex]), int.Parse(pageOrderingRuleText[(separatorIndex + 1)..]));
}