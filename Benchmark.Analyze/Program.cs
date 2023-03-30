using System.Globalization;
using System.Text.Json;
using Benchmark.Client;
using CsvHelper;

namespace Benchmark.Analyze;

internal class Program
{
    private const string inputFileName = @"d:\\request-log.txt";
    private const string outputFileName = @"d:\\request-log.csv";

    private static void Main(string[] args)
    {
        var allRequests = File.ReadAllLines(inputFileName)
            .Select(x => JsonSerializer.Deserialize<HttpCallDetails>(x))
            .Where(x => x is not null)
            .Where(x => x.timeSinceLastRequest is not null)
            .OfType<HttpCallDetails>()
            .ToList();

        var samples = new List<DataPoint>(allRequests.Count);

        // convert to CSV so it's easy do draw charts in Excel
        foreach (var request in allRequests)
        {
            string? architecture = null;
            if (request.Url.Contains("Arm64")) architecture = "arm64";
            if (request.Url.Contains("x86_64")) architecture = "x64";
            if (architecture is null) continue; // warm-up request

            var memory = request.Url.Split("/").Last();

            var minutesSinceLastCall = request.timeSinceLastRequest!.Value.TotalMinutes;

            var lambdaSelfReportedColdStart = request.Response!.Contains("\"IsColdStart\":true");

            var dataPoint = new DataPoint(architecture, memory, minutesSinceLastCall,
                request.TotalTime.TotalMilliseconds, lambdaSelfReportedColdStart, request.Error is not null);
            samples.Add(dataPoint);
        }

        ExportResults(outputFileName, samples);
        Console.WriteLine("Done!");
    }

    private static void ExportResults(string fileName, List<DataPoint> samples)
    {
        using var writer = new StreamWriter(fileName);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(samples);
    }
}

internal record DataPoint(string Architecture, string MemorySizeMb, double MinutesSinceLastCall,
    double EndToEndRequestDurationMs, bool IsColdStart, bool RequestFailed);