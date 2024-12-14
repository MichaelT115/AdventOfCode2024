namespace Day5_PrintQueue;

public sealed class PrintQueuer
{
    private readonly Dictionary<int, HashSet<int>> _pagesThatComeAfter;
    private readonly Dictionary<int, HashSet<int>> _pagesThatComeBefore;

    public PrintQueuer((int comesBefore, int comesAfter)[] orders)
    {
        _pagesThatComeAfter = new Dictionary<int, HashSet<int>>(orders.Length);
        foreach (var order in orders)
        {
            if (!_pagesThatComeAfter.TryGetValue(order.comesBefore, out var pagesThatComeAfterSet))
            {
                pagesThatComeAfterSet = [];
                _pagesThatComeAfter.Add(order.comesBefore, pagesThatComeAfterSet);
            }

            pagesThatComeAfterSet.Add(order.comesAfter);
        }

        _pagesThatComeBefore = new Dictionary<int, HashSet<int>>(orders.Length);
        foreach (var order in orders)
        {
            if (!_pagesThatComeBefore.TryGetValue(order.comesAfter, out var pagesThatComeBeforeSet))
            {
                pagesThatComeBeforeSet = [];
                _pagesThatComeBefore.Add(order.comesAfter, pagesThatComeBeforeSet);
            }

            pagesThatComeBeforeSet.Add(order.comesBefore);
        }
    }

    public int ProcessQueues(int[][] pageQueues)
        => pageQueues
            .Where(pageQueue => IsValidQueue(pageQueue))
            .Sum(pageQueue => pageQueue[pageQueue.Length / 2]);
    
    public int ProcessQueuesWithCorrections(int[][] pageQueues) 
        => pageQueues
            .Where(pageQueue => TryFixQueue(pageQueue))
            .Sum(pageQueue => pageQueue[pageQueue.Length / 2]);

    public bool IsValidQueue(ReadOnlySpan<int> pageQueue)
    {
        var previousPages = new HashSet<int>(pageQueue.Length);

        foreach (var page in pageQueue)
        {
            if (_pagesThatComeAfter.TryGetValue(page, out var pageThatComesAfterSet))
            {
                if (pageThatComesAfterSet.Any(pageThatComesAfter => previousPages.Contains(pageThatComesAfter)))
                {
                    return false;
                }
            }

            previousPages.Add(page);
        }

        return true;
    }

    public bool TryFixQueue(Span<int> pageQueue)
    {
        var neededFix = false;
        var index = 0;
        while (index < pageQueue.Length)
        {
            if (TryFindCorrection(pageQueue, index, out var swapIndex))
            {
                (pageQueue[index], pageQueue[swapIndex]) = (pageQueue[swapIndex], pageQueue[index]);
                neededFix = true;
                continue;
            }

            ++index;
        }

        return neededFix;
    }

    private bool TryFindCorrection(ReadOnlySpan<int> pageQueue, int currentIndex, out int swapIndex)
    {
        swapIndex = currentIndex + 1;
        if (!_pagesThatComeBefore.TryGetValue(pageQueue[currentIndex], out var pageThatComesBeforeSet)) return false;

        while (swapIndex < pageQueue.Length)
        {
            if (pageThatComesBeforeSet.Contains(pageQueue[swapIndex]))
            {
                return true;
            }

            swapIndex++;
        }

        return false;
    }
}