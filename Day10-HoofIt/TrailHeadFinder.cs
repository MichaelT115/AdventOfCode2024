public static class TrailHeadFinder
{
    public static int CountTrailHeadScores(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return 0;
        }

        var firstLineBreakIndex = input.IndexOf('\n');
        var width = firstLineBreakIndex >= 0 ? firstLineBreakIndex + 1 : input.Length;
        var height = input.Length % width == 0 ? input.Length / width : input.Length / width + 1;

        var potentialTrailHeadPositions = new List<int>();
        for (var i = 0; i < input.Length; ++i)
        {
            if (input[i] == '0')
            {
                potentialTrailHeadPositions.Add(i);
            }
        }

        var sum = 0;
        foreach (var potentialTrailHeadPosition in potentialTrailHeadPositions)
        {
            var checkForPositions = new Queue<int>(1);
            checkForPositions.Enqueue(potentialTrailHeadPosition);

            var visitedPositions = new HashSet<int>();

            while (checkForPositions.TryDequeue(out var position))
            {
                if (input[position] == '9')
                {
                    ++sum;
                    continue;
                }

                var nextLevel = (char)(input[position] + 1);
                var leftPosition = position - 1;
                if (position % width != 0 && input[leftPosition] == nextLevel && visitedPositions.Add(leftPosition))
                {
                    checkForPositions.Enqueue(leftPosition);
                }

                var rightPosition = position + 1;
                if (position % width < width - 1 && rightPosition < input.Length && input[rightPosition] == nextLevel &&
                    visitedPositions.Add(rightPosition))
                {
                    checkForPositions.Enqueue(rightPosition);
                }

                var upPosition = position - width;
                if (width <= position && input[upPosition] == nextLevel && visitedPositions.Add(upPosition))
                {
                    checkForPositions.Enqueue(upPosition);
                }

                var downPosition = position + width;
                if (position / width < height - 1 && input[downPosition] == nextLevel &&
                    visitedPositions.Add(downPosition))
                {
                    checkForPositions.Enqueue(downPosition);
                }
            }
        }

        return sum;
    }

    public static int CountTrailHeadQuality(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return 0;
        }

        var firstLineBreakIndex = input.IndexOf('\n');
        var width = firstLineBreakIndex >= 0 ? firstLineBreakIndex + 1 : input.Length;
        var height = input.Length % width == 0 ? input.Length / width : input.Length / width + 1;

        var checkForPositions = new Queue<int>();
        for (var i = 0; i < input.Length; ++i)
        {
            if (input[i] == '0')
            {
                checkForPositions.Enqueue(i);
            }
        }

        var sum = 0;

        while (checkForPositions.TryDequeue(out var position))
        {
            if (input[position] == '9')
            {
                ++sum;
                continue;
            }

            var nextLevel = (char)(input[position] + 1);
            var leftPosition = position - 1;
            if (position % width != 0 && input[leftPosition] == nextLevel)
            {
                checkForPositions.Enqueue(leftPosition);
            }

            var rightPosition = position + 1;
            if (position % width < width - 1 && rightPosition < input.Length && input[rightPosition] == nextLevel)
            {
                checkForPositions.Enqueue(rightPosition);
            }

            var upPosition = position - width;
            if (width <= position && input[upPosition] == nextLevel)
            {
                checkForPositions.Enqueue(upPosition);
            }

            var downPosition = position + width;
            if (position / width < height - 1 && input[downPosition] == nextLevel)
            {
                checkForPositions.Enqueue(downPosition);
            }
        }

        return sum;
    }
}