using System.Runtime.InteropServices;
using System.Text.Json;

namespace Benchmark.AwsServerless.Shared;

public class ResponseBuilder
{
    public static string BuildResponse(bool isFirstCallAfterColdStart)
    {
        var architecture = RuntimeInformation.ProcessArchitecture;
        var dotnetVersion = Environment.Version.ToString();
        var responseModel = new ResponseModel(architecture.ToString(), dotnetVersion, isFirstCallAfterColdStart);
        var jsonString = JsonSerializer.Serialize(responseModel);
        return jsonString;
    }
}