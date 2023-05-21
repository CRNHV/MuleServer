using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Requests.Client;
using BotServer.Lib.Data.Requests.Server;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Session.Interfaces;
using Microsoft.Extensions.Logging;

namespace BotServer.Lib.Handlers.Requests;

internal sealed class MuleDoneRequestHandler : BaseRequestHandler, IRequestHandler
{
    private readonly ISessionFinder _sessionFinder;
    private readonly ILogger<MuleDoneRequestHandler> logger;

    public MuleDoneRequestHandler(ISessionFinder sessionFinder, ILogger<MuleDoneRequestHandler> logger) : base(logger)
    {
        _sessionFinder = sessionFinder;
        this.logger = logger;
    }

    public RequestAction RequestAction => RequestAction.MULE_DONE;

    public async Task HandleRequestAsync(JObjectBotRequest request)
    {
        MuleDoneRequest muleDoneRequest = request.Data.ToObject<MuleDoneRequest>();
        if (muleDoneRequest == null)
        {
            return;
        }

        var muleSession = _sessionFinder.FindBotByName(muleDoneRequest.MuleRsn);
        ServerRequest<MuleDoneRequest> serverRequest = new ServerRequest<MuleDoneRequest>(RequestAction.MULE_DONE);
        await SendDataAsync(request.WebSocket, serverRequest);
    }
}
