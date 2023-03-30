using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Benchmark.AwsServerless.Shared;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace Benchmark.AwsServerless.X86_64;

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
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/128")]
    public IHttpResult x86_64_128()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/256")]
    public IHttpResult x86_64_256()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/512")]
    public IHttpResult x86_64_512()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/1024")]
    public IHttpResult x86_64_1024()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/2048")]
    public IHttpResult x86_64_2048()
    {
        return PrepareResponse();
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/x86_64/3008")]
    public IHttpResult x86_64_3008()
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