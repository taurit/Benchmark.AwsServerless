namespace Benchmark.Client;

internal static class Configuration
{
    public static readonly List<TimeSpan> IntervalsBetweenInvocationSeries = new()
    {
        // cold start unlikely for such low intervals, but it's good to have data to see a clear difference
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(30),
        TimeSpan.FromSeconds(60),
        TimeSpan.FromSeconds(90),
        TimeSpan.FromMinutes(2),
        TimeSpan.FromMinutes(3),
        TimeSpan.FromMinutes(4),

        // cold start expected after 5-7 minutes of inactivity
        TimeSpan.FromMinutes(5),
        TimeSpan.FromMinutes(6),
        TimeSpan.FromMinutes(7),
        TimeSpan.FromMinutes(10),
        TimeSpan.FromMinutes(15),
        TimeSpan.FromMinutes(20),
        TimeSpan.FromMinutes(30),
        TimeSpan.FromMinutes(40),
        TimeSpan.FromMinutes(50),
        TimeSpan.FromMinutes(60),
        TimeSpan.FromMinutes(90)
    };

    public static readonly List<TestedConfiguration> TestedConfigurations = new()
    {
        new(Architecture.x86_64, 128),
        new(Architecture.x86_64, 256),
        new(Architecture.x86_64, 512),
        new(Architecture.x86_64, 1024),
        new(Architecture.x86_64, 2048),
        new(Architecture.x86_64, 3008),

        new(Architecture.Arm64, 128),
        new(Architecture.Arm64, 256),
        new(Architecture.Arm64, 512),
        new(Architecture.Arm64, 1024),
        new(Architecture.Arm64, 2048),
        new(Architecture.Arm64, 3008)
    };
}