using Day3_MullItOver;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Result: {MemoryScanner.Scan(input)}");
Console.WriteLine($"Result (w/ Conditionals): {MemoryScanner.ScanWithConditionals(input)}");