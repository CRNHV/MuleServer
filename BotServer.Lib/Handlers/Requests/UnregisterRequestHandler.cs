using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Requests.Client;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BotServer.Lib.Handlers.Requests;

internal sealed class UnregisterRequestHandler : BaseRequestHandler, IRequestHandler
{
    private readonly IRegistrationService _registrationService;
    private readonly ILogger<UnregisterRequestHandler> logger;

    public UnregisterRequestHandler(IRegistrationService registrationService, ILogger<UnregisterRequestHandler> logger) : base(logger)
    {
        _registrationService = registrationService;
        this.logger = logger;
    }

    public RequestAction RequestAction => RequestAction.UNREGISTER;

    public async Task HandleRequestAsync(JObjectBotRequest request)
    {
        _registrationService.UnRegisterConnection(request.ConnectionId.GetValueOrDefault());
    }
}
