global using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<MainHub>("/hub");

app.MapPost("/send", async ([FromBody] string message, IHubContext<MainHub> hub) =>
{
    Console.WriteLine(message);
    await hub.Clients.All.SendAsync("Receive", message);
});

app.MapGet("/", () => "Hello World!");

app.Run();
