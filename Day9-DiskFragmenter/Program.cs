using Day9_DiskFragmenter;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

var memorySpans = FileCompacter.CompactFiles(input);
var memorySpans2 = FileCompacter.CompactFiles2(input);

Console.WriteLine($"Result: {FileCompacter.CalculateCheckSum(memorySpans)}");
Console.WriteLine($"Result: {FileCompacter.CalculateCheckSum(memorySpans2)}");