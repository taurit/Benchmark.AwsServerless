using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Benchmark.Client;

internal class Program
{
    private const string BaseUrl = "https://3g6rdmxoo8.execute-api.eu-north-1.amazonaws.com/";

    // dependencies
    private static readonly HttpClient HttpClient = new();
    private static readonly RequestHistory RequestHistoryLog = new();
    private static readonly Logger Logger = new Logger("d:\\request-log.txt");

    private static async Task Main(string[] args)
    {
        foreach (var interval in Configuration.IntervalsBetweenInvocationSeries)
        {
            // attempt to warm up and get DNS cached and perhaps networking devices 
            Console.WriteLine("Starting warm-up routine...");
            for (var i = 0; i < 3; i++)
            {
                var result = await SendRequest(BaseUrl);
                await Logger.LogResult(result);
                await Task.Delay(TimeSpan.FromSeconds(3));
            }

            Console.WriteLine("Completed warm-up routine.");

            Console.WriteLine("Testing configurations...");
            foreach (var configuration in Configuration.TestedConfigurations)
            {
                var url = $"{BaseUrl}{configuration.Architecture}/{configuration.MemorySize}";
                var result = await SendRequest(url);
                await Logger.LogResult(result);
                await Task.Delay(TimeSpan.FromSeconds(3));
            }

            Console.WriteLine("Completed testing configurations.");
            Console.WriteLine($"Next round in: {interval.TotalMinutes} minutes");
            await Task.Delay(interval);
        }
    }

    private static async Task<HttpCallDetails> SendRequest(string url)
    {
        Console.WriteLine($"Sending request to {url}");
        var timeSinceLastRequest = RequestHistoryLog.CheckLastRequestSentToUrl(url);
        RequestHistoryLog.LogRequestSentToUrl(url);

        var startTime = DateTimeOffset.UtcNow;
        var s = Stopwatch.StartNew();
        try
        {
            var response = await HttpClient.GetStringAsync(url);
            s.Stop();

            Console.WriteLine($"Request succeeded, response: {response}");
            return new HttpCallDetails(startTime, s.Elapsed, url, response, null, timeSinceLastRequest);
        }
        catch (Exception e)
        {
            s.Stop();

            Console.WriteLine($"Request failed, error: {e.Message}");
            return new HttpCallDetails(startTime, s.Elapsed, url, null, e.Message, timeSinceLastRequest);
        }
    }
}