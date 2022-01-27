public class MessagingCommand : AsyncCommand<Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        // Console.WriteLine($"Starting messaging session with hub {settings.HubUri}");

        await using var connection = new HubConnectionBuilder()
            .WithUrl(settings.ApiUrl)
            .Build();

        await connection.StartAsync();

        connection.On<String>("message", message =>
        {
            System.Console.WriteLine(message);
        });

        while (true)
        {
            var input = Console.ReadLine();
            await connection.InvokeAsync("Send", input);
        }
    }
}
