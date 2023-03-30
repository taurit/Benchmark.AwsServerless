namespace Benchmark.Client;

internal record HttpCallDetails(DateTimeOffset RequestStartTime, TimeSpan TotalTime, string Url, string? Response,
    string? Error);