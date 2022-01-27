global using System.Collections.Generic;
global using System.Net;
global using System.Threading.Tasks;
global using Microsoft.Azure.Functions.Worker;
global using Microsoft.Azure.Functions.Worker.Http;
global using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .Build();

        host.Run();
    }
}