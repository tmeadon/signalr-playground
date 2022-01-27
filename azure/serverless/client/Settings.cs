using System.ComponentModel;

public class Settings : CommandSettings
{
    [CommandOption("--uri")]
    [Description("The URI of the SignalR hub.")]
    public string ApiUrl { get; set; } = "";
}
