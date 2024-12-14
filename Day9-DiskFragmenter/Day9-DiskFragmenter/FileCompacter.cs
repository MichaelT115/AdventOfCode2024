using MemorySpan = (int fileId, int length);

namespace Day9_DiskFragmenter;

public static class FileCompacter
{
    // For Check Sum, empty space is functionally zero.
    private const int EmptySpaceFileId = 0;

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

    public static MemorySpan[] CompactFiles2(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return [];
        }

        if (input.Length == 1)
        {
            return [(0, input[0] - '0')];
        }

        var memorySpansList = ParseMemorySpans(input);

        var index = memorySpansList.Count - 1;
        while (index > 1)
        {
            var memorySpan = memorySpansList[index];
            if (memorySpan.fileId == EmptySpaceFileId)
            {
                if (memorySpan.length == 0)
                {
                    memorySpansList.RemoveAt(index);
                }
                else if (index < memorySpansList.Count - 1 && memorySpansList[index + 1].fileId == EmptySpaceFileId)
                {
                    memorySpansList[index] = memorySpan with
                    {
                        length = memorySpan.length + memorySpansList[index + 1].length
                    };
                    memorySpansList.RemoveAt(index + 1);
                }
                --index;
                continue;
            }

            var swapped = false;
            for (var j = 1; j < index; ++j)
            {
                var targetMemorySpan = memorySpansList[j];
                if (targetMemorySpan.fileId != EmptySpaceFileId || targetMemorySpan.length < memorySpan.length)
                {
                    continue;
                }

                memorySpansList[index] = memorySpan with { fileId = EmptySpaceFileId };

                var spaceLeft = targetMemorySpan.length - memorySpan.length;
                memorySpansList[j] = memorySpan;
                if (spaceLeft > 0)
                {
                    memorySpansList.Insert(j + 1, new MemorySpan { fileId = EmptySpaceFileId, length = spaceLeft });
                    ++index;
                }

                swapped = true;
                break;
            }

            if (!swapped)
            {
                --index;
            }
        }

        return memorySpansList.ToArray();
    }

    private static List<MemorySpan> ParseMemorySpans(ReadOnlySpan<char> input)
    {
        var memorySpansList = new List<MemorySpan>(input.Length);

        for (var index = 0; index < input.Length; ++index)
        {
            memorySpansList.Add(new MemorySpan
            {
                fileId = index % 2 == 0 ? index / 2 : EmptySpaceFileId,
                length = input[index] - '0'
            });
        }

        return memorySpansList;
    }

    public static ulong CalculateCheckSum(ReadOnlySpan<MemorySpan> memorySpans)
    {
        var checkSum = 0ul;
        var index = 0ul;
        foreach (var (fileId, length) in memorySpans)
        {
            if (fileId == EmptySpaceFileId) continue;
            for (var i = 0; i < length; ++i)
            {
                checkSum += (ulong)fileId * index++;
            }
        }

        return checkSum;
    }
}