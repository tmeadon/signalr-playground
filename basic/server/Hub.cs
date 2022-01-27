using System.Runtime.CompilerServices;

public class MainHub : Hub
{
    public void Send(string message)
    {
        // Clients.All.SendAsync("Receive", message);
        Clients.AllExcept(Context.ConnectionId).SendAsync("Receive", message);
    }

    public async IAsyncEnumerable<DateTime> Streaming([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            yield return DateTime.UtcNow;
            await Task.Delay(1000, cancellationToken);
        }
    }
}