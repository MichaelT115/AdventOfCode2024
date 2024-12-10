using MemorySpan = (int fileId, int length);

namespace Day9_DiskFragmenter;

public static class FileCompacter
{
    public static MemorySpan[] CompactFiles(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return [];
        }

        if (input.Length == 1)
        {
            return [(0, input[0] - '0')];
        }

        var memorySpansList = new List<(int fileId, int lenght)>(input.Length / 2);

        var left = 0;
        var right = input.Length % 2 != 0 ? input.Length - 1 : input.Length - 2;

        var rightSpanLengthLeft = input[right] - '0';

        while (left <= right)
        {
            if (left == right - 1)
            {
                memorySpansList.Add(new MemorySpan
                {
                    fileId = right / 2,
                    length = rightSpanLengthLeft
                });
                break;

            }

            if (left % 2 == 0)
            {
                memorySpansList.Add(new MemorySpan
                {
                    fileId = left / 2,
                    length = input[left] - '0'
                });
            }
            else
            {
                var emptySpaceLeft = input[left] - '0';
                while (left < right && emptySpaceLeft > 0)
                {
                    var spaceTaken = Math.Min(rightSpanLengthLeft, emptySpaceLeft);
                    if (spaceTaken == 0)
                    {
                        break;
                    }
                    
                    emptySpaceLeft -= spaceTaken;
                    rightSpanLengthLeft -= spaceTaken;

                    memorySpansList.Add(new MemorySpan
                    {
                        fileId = right / 2,
                        length = spaceTaken
                    });

                    if (rightSpanLengthLeft == 0)
                    {
                        right -= 2;
                        rightSpanLengthLeft = input[right] - '0';
                    }
                }
            }
            
            ++left;
        }

        return memorySpansList.ToArray();
    }
    
    public static ulong CalculateCheckSum(ReadOnlySpan<MemorySpan> memorySpans)
    {
        var checkSum = 0ul;
        var index = 0ul;
        foreach (var (fileId, length) in memorySpans)
        {
            for (var i = 0; i < length; ++i)
            {
                checkSum += (ulong)fileId * index++;
            }
        }

        return checkSum;
    }
}