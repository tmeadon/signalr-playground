
global using Microsoft.AspNetCore.SignalR.Client;
global using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{   
    config.AddCommand<MessagingCommand>("messaging")
        .WithAlias("m")
        .WithDescription("Joins a messaging session with the SignalR server");
});
await app.RunAsync(args);


