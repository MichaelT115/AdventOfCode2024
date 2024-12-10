using Day9_DiskFragmenter;

using var streamReader = new StreamReader(args[0]);

var memorySpans = FileCompacter.CompactFiles(streamReader.ReadToEnd());

var checkSum = FileCompacter.CalculateCheckSum(memorySpans);

Console.WriteLine($"Result: {checkSum}");