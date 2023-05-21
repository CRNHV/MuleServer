using BotServer.Lib.Extensions;

namespace BotServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddLogging();

        builder.Services.AddLib();

        var app = builder.Build();

        var webSocketOptions = new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2)
        };

        app.UseWebSockets(webSocketOptions);

        app.MapControllers();

        app.Run();
    }
}