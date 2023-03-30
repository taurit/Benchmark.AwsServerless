using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using System.Text.Json;
using System.Runtime.InteropServices;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Benchmark.AwsServerless;

record ResponseModel(string Architecture, string DotNetVersion, bool IsColdStart);

public class Functions
{
    static bool IsFirstCallAfterColdStart = true;

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/")]
    public IHttpResult Default() => PrepareResponse();

    // X86_64:

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/128")]
    public IHttpResult x86_64_128() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/256")]
    public IHttpResult x86_64_256() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/512")]
    public IHttpResult x86_64_512() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/1024")]
    public IHttpResult x86_64_1024() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/2048")]
    public IHttpResult x86_64_2048() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/3008")]
    public IHttpResult x86_64_3008() => PrepareResponse();

    // ARM64:

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/128")]
    public IHttpResult Arm64_128() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/256")]
    public IHttpResult Arm64_256() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/512")]
    public IHttpResult Arm64_512() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/1024")]
    public IHttpResult Arm64_1024() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/2048")]
    public IHttpResult Arm64_2048() => PrepareResponse();

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/3008")]
    public IHttpResult Arm64_3008() => PrepareResponse();

    private static IHttpResult PrepareResponse()
    {
        var architecture = RuntimeInformation.ProcessArchitecture;
        var dotnetVersion = Environment.Version.ToString();

        var responseModel = new ResponseModel(architecture.ToString(), dotnetVersion, IsFirstCallAfterColdStart);
        IsFirstCallAfterColdStart = false;

        string jsonString = JsonSerializer.Serialize(responseModel);
        return HttpResults.Ok(jsonString);
    }

}