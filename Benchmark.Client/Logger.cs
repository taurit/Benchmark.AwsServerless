using System.Text.Json;

namespace Benchmark.Client;

internal class Logger
{
    private readonly string _appendLogFileName;

    public Logger(string appendLogFileName)
    {
        _appendLogFileName = appendLogFileName;
    }

    public async Task LogResult(HttpCallDetails result)
    {
        var resultSerialized = JsonSerializer.Serialize(result);
        await File.AppendAllTextAsync(_appendLogFileName, $"{resultSerialized}{Environment.NewLine}");
    }
}