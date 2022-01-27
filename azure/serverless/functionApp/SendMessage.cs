public class SendMessage
{
    private readonly ILogger _logger;

    public SendMessage(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Negotiate>();
    }

    [Function("SendMessage")]
    [SignalROutput(ConnectionStringSetting = "AzureSignalRConnectionString", HubName = "test")]
    public async Task<SignalRMessage> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req
    )
    {
        var message = await req.ReadAsStringAsync();
        return new SignalRMessage()
        {
            Target = "message",
            Arguments = new[] { message }
        };
    }
}

public class SignalRMessage
{
    public string Target { get; set; }
    public object[] Arguments { get; set; }
}