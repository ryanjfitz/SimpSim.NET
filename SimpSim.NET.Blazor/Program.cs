using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace SimpSim.NET.Blazor;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");

        builder.Services.AddSingleton<SimpleSimulator>();

        await builder.Build().RunAsync();
    }
}