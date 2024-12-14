using Day2_RedNoseReports;

using var streamReader = new StreamReader(args[0]);

var input = streamReader.ReadToEnd();

Console.WriteLine($"Safe Count: {ReportAnalyzer.CalculateSafeReportsCount(input)}");
Console.WriteLine($"Safe Count (w/ Removal): {ReportAnalyzer.CalculateSafeReportsCount(input, true)}");