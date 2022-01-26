public class StreamDateTimeCommand : AsyncCommand<Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        Console.WriteLine($"Starting streaming session with hub {settings.HubUri}");

        await using var connection = new HubConnectionBuilder()
            .WithUrl(settings.HubUri)
            .Build();

        await connection.StartAsync();

        await foreach (var date in connection.StreamAsync<DateTime>("Streaming"))
        {
            Console.WriteLine(date);
        }

        return 0;
    }
}
