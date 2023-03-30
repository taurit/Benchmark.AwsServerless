using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Benchmark.AwsServerless.Shared;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace Benchmark.AwsServerless.ARM64;

/// <summary>
///     A collection of sample Lambda functions that provide a REST api for doing simple math calculations.
/// </summary>
public class Functions
{
    private static bool _isFirstCallAfterColdStart = true;

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/")]
    public IHttpResult Default()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/128")]
    public IHttpResult Arm64_128()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/256")]
    public IHttpResult Arm64_256()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/512")]
    public IHttpResult Arm64_512()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/1024")]
    public IHttpResult Arm64_1024()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/2048")]
    public IHttpResult Arm64_2048()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/Arm64/3008")]
    public IHttpResult Arm64_3008()
    {
        return PrepareResponse();
    }

    private static IHttpResult PrepareResponse()
    {
        var responseJsonString = ResponseBuilder.BuildResponse(_isFirstCallAfterColdStart);
        _isFirstCallAfterColdStart = false;
        return HttpResults.Ok(responseJsonString);
    }
}