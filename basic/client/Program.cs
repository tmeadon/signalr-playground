
global using Microsoft.AspNetCore.SignalR.Client;
global using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<StreamDateTimeCommand>("stream")
        .WithAlias("s")
        .WithDescription("Streams the current time from the SignalR server")
        .WithExample(new []{ "stream", "--hub http://localhost:5277/hub" });
    
    config.AddCommand<MessagingCommand>("messaging")
        .WithAlias("m")
        .WithDescription("Joins a messaging session with the SignalR server")
        .WithExample(new []{ "messaging", "--hub http://localhost:5277/hub" });
});
await app.RunAsync(args);


