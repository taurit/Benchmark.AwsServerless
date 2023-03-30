namespace Benchmark.Client;

// As a bonus, I want to observe how intervals between function invocations impact whether cold start occurs
internal class RequestHistory
{
    private readonly Dictionary<string, DateTimeOffset> _history = new Dictionary<string, DateTimeOffset>();

    public void LogRequestSentToUrl(string url) {
        var now = DateTimeOffset.Now;
        _history[url] = now;
    }
    
    public TimeSpan? CheckLastRequestSentToUrl(string url) {
        if (!_history.ContainsKey(url)) return null;

        var now = DateTimeOffset.Now;
        var lastRequest = _history[url];
        var interval = now - lastRequest;
        return interval;
    }
}