using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Requests.Client;
using BotServer.Lib.Data.Requests.Server;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BotServer.Lib.Handlers.Requests;

internal sealed class RegisterRequestHandler : BaseRequestHandler, IRequestHandler
{
    private readonly IRegistrationService _registrationService;
    private readonly ILogger<RegisterRequestHandler> _logger;

    public RegisterRequestHandler(IRegistrationService registrationService, ILogger<RegisterRequestHandler> logger) : base(logger)
    {
        _registrationService = registrationService;
        _logger = logger;
    }

    public RequestAction RequestAction => RequestAction.REGISTER;

    public async Task HandleRequestAsync(JObjectBotRequest request)
    {
        _logger.LogInformation("Handle new registration: {@request}", request);

        RegistrationRequest registrationRequest = request.Data.ToObject<RegistrationRequest>();
        if (registrationRequest == null)
        {
            return;
        }

        Guid connectionId = _registrationService.RegisterConnection(
            registrationRequest.FarmType,
            registrationRequest.BotType,
            registrationRequest.Rsn,
            request.WebSocket
            );

        var connectionIdRequest = new SetConnectionId(connectionId);
        ServerRequest<SetConnectionId> serverRequest = new(RequestAction.SET_CONNECTIONID, connectionIdRequest);
        await SendDataAsync(request.WebSocket, serverRequest);
    }
}
