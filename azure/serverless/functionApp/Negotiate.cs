public class Negotiate
{
    private readonly ILogger _logger;

    public Negotiate(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Negotiate>();
    }

    [Function("Negotiate")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [SignalRConnectionInfoInput(ConnectionStringSetting = "AzureSignalRConnectionString", HubName = "test")] SignalRConnectionInfo connectionInfo)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync<SignalRConnectionInfo>(connectionInfo);
        return response;
    }
}

public class SignalRConnectionInfo
{
    public string url { get; set; }
    public string accessToken { get; set; }
}
