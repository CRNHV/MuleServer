using BotServer.Lib.Data.Requests.Client;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Handlers.Requests;
using BotServer.Lib.Services.Interfaces;
using BotServer.Lib.Session.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BotServer.Lib.Handlers;

internal sealed class MessageHandler : IMessageHandler
{

    private readonly ILogger<MessageHandler> _logger;
    private IReadOnlyCollection<IRequestHandler> _requestHandlers;

    public MessageHandler(IRegistrationService registrationService, ISessionFinder sessionFinder, ILogger<MessageHandler> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;

        _requestHandlers = new List<IRequestHandler>()
        {
            new RegisterRequestHandler(registrationService, serviceProvider.GetRequiredService<ILogger<RegisterRequestHandler>>()),
            new UnregisterRequestHandler(registrationService, serviceProvider.GetRequiredService<ILogger<UnregisterRequestHandler>>()),
            new MuleDoneRequestHandler(sessionFinder, serviceProvider.GetRequiredService<ILogger<MuleDoneRequestHandler>>()),
            new RequestMuleRequestHandler(sessionFinder, serviceProvider.GetRequiredService<ILogger<RequestMuleRequestHandler>>())

        };
    }

    public async Task HandleMessageAsync(JObjectBotRequest request)
    {
        _logger.LogInformation("Request: {action}", request.Action);

        try
        {
            foreach (IRequestHandler requestHandler in _requestHandlers)
            {
                if (requestHandler.RequestAction == request.Action)
                {
                    await requestHandler.HandleRequestAsync(request);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
