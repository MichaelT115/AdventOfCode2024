namespace Day12_GardenGroups;

public static class RegionCoster
{
    public static ulong FindTotalCost(ReadOnlySpan<char> input)
    {
        if (input.Length == 0)
        {
            return 0;
        }

        var indexOfLineBreak = input.IndexOf('\n');
        var width = indexOfLineBreak != -1 ? indexOfLineBreak + 1 : input.Length;
        var height = 0;
        foreach (var _ in input.EnumerateLines())
        {
            ++height;
        }

        var regions = new List<(int area, int fences)>();

        var visited = new HashSet<int>();

        for (var gridIndex = 0; gridIndex < input.Length; ++gridIndex)
        {
            if (input[gridIndex] is '\n' or '\r' || visited.Contains(gridIndex))
            {
                continue;
            }

            var region = default((int area, int fences));

            var toVisit = new Queue<int>();
            toVisit.Enqueue(gridIndex);
            while (toVisit.TryDequeue(out var gridIndexInRegion))
            {
                if (!visited.Add(gridIndexInRegion))
                {
                    continue;
                }

                region.area++;

                var regionType = input[gridIndexInRegion];

                if (gridIndexInRegion == 0)
                {
                    ++region.fences;
                }
                else
                {
                    if (input[gridIndexInRegion - 1] != regionType)
                    {
                        ++region.fences;
                    }
                    else
                    {
                        toVisit.Enqueue(gridIndexInRegion - 1);
                    }
                }

                if (gridIndexInRegion == input.Length - 1)
                {
                    ++region.fences;
                }
                else
                {
                    if (input[gridIndexInRegion + 1] != regionType)
                    {
                        ++region.fences;
                    }
                    else
                    {
                        toVisit.Enqueue(gridIndexInRegion + 1);
                    }
                }

                if (gridIndexInRegion < width)
                {
                    ++region.fences;
                }
                else
                {
                    if (input[gridIndexInRegion - width] != regionType)
                    {
                        ++region.fences;
                    }
                    else
                    {
                        toVisit.Enqueue(gridIndexInRegion - width);
                    }
                }

                if ((height - 1) * width <= gridIndexInRegion)
                {
                    ++region.fences;
                }
                else
                {
                    if (input[gridIndexInRegion + width] != regionType)
                    {
                        ++region.fences;
                    }
                    else
                    {
                        toVisit.Enqueue(gridIndexInRegion + width);
                    }
                }
            }

            regions.Add(region);
        }

        return regions.Aggregate<(int area, int fences), ulong>(0,
            (current, region) => current + (ulong)(region.area * region.fences));
    }
}