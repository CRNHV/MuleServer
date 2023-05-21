using BotServer.Lib.Handlers;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Services;
using BotServer.Lib.Services.Interfaces;
using BotServer.Lib.Session;
using BotServer.Lib.Session.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BotServer.Lib.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLib(this IServiceCollection services)
    {
        // services.AddDbContext<BotDbContext>(opt => opt.UseInMemoryDatabase("Boat"));

        services.AddScoped<IMessageHandler, MessageHandler>();
        services.AddScoped<IWebsocketConnectionHandler, WebsocketConnectionHandler>();

        services.AddSingleton<IBotSessionHolder, BotSessionHolder>();
        services.AddScoped<ISessionFinder, SessionFinder>();

        services.AddScoped<IRegistrationService, RegistrationService>();
    }
}
