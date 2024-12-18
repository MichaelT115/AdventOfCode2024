namespace Day12_GardenGroups;

public static class RegionFinder
{
    public struct Region
    {
        public int Area;
        public int Perimeter;
        public int Corners;
    }

    public static List<Region> GetRegions(ReadOnlySpan<char> input)
    {
        var indexOfLineBreak = input.IndexOf('\n');
        var width = indexOfLineBreak != -1 ? indexOfLineBreak + 1 : input.Length;
        var height = 0;
        foreach (var _ in input.EnumerateLines())
        {
            ++height;
        }

        var regions = new List<Region>();

        var visited = new HashSet<int>();

        for (var gridIndex = 0; gridIndex < input.Length; ++gridIndex)
        {
            if (input[gridIndex] is '\n' or '\r' || visited.Contains(gridIndex))
            {
                continue;
            }

            var positionsInRegion = new HashSet<int>();

            var toVisit = new Queue<int>();
            toVisit.Enqueue(gridIndex);
            while (toVisit.TryDequeue(out var position))
            {
                if (!visited.Add(position))
                {
                    continue;
                }

                positionsInRegion.Add(position);

                var regionType = input[position];

                if (position != 0 && input[position - 1] == regionType)
                {
                    toVisit.Enqueue(position - 1);
                }

                if (position != input.Length - 1 && input[position + 1] == regionType)
                {
                    toVisit.Enqueue(position + 1);
                }

                if (position >= width && input[position - width] == regionType)
                {
                    toVisit.Enqueue(position - width);
                }

                if ((height - 1) * width > position && input[position + width] == regionType)
                {
                    toVisit.Enqueue(position + width);
                }
            }

            var perimeter = 0;
            var corners = 0;
            foreach (var positionNeighbors in positionsInRegion.Select(position => new[]
                     {
                         positionsInRegion.Contains(position - width),
                         height > 1 && width > 1 && positionsInRegion.Contains(position - width + 1),
                         positionsInRegion.Contains(position + 1),
                         positionsInRegion.Contains(position + width + 1),
                         positionsInRegion.Contains(position + width),
                         height > 1 && positionsInRegion.Contains(position + width - 1),
                         positionsInRegion.Contains(position - 1),
                         positionsInRegion.Contains(position - width - 1)
                     }))
            {
                for (var i = 0; i < 8; i += 2)
                {
                    if (!positionNeighbors[i])
                    {
                        ++perimeter;
                    }
                }

                for (var i = 1; i < 8; i += 2)
                {
                    corners += (counterClockwiseFromCorner: positionNeighbors[i - 1], corner: positionNeighbors[i],
                            clockwiseFromCorner: positionNeighbors[(i + 1) % 8]) switch
                        {
                            (false, false, false) => 1,
                            (true, false, true) => 1,
                            (false, true, false) => 1,
                            _ => 0
                        };
                }
            }

            regions.Add(new Region
            {
                Area = positionsInRegion.Count,
                Perimeter = perimeter,
                Corners = corners
            });
        }

        return regions;
    }
}