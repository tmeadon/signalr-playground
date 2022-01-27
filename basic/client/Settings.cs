using System.ComponentModel;

public class Settings : CommandSettings
{
    [CommandOption("--hub")]
    [Description("The URI of the SignalR hub.")]
    [DefaultValue("http://localhost:5277/hub")]
    public string HubUri { get; set; } = "";
}
