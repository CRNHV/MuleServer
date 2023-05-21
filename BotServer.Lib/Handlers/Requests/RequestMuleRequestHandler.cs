using System.Net.WebSockets;
using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Models;
using BotServer.Lib.Data.Requests.Client;
using BotServer.Lib.Data.Requests.Server;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Session.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BotServer.Lib.Handlers.Requests;

internal sealed class RequestMuleRequestHandler : BaseRequestHandler, IRequestHandler
{
    private readonly ISessionFinder _sessionFinder;
    private readonly ILogger<RequestMuleRequestHandler> _logger;

    public RequestMuleRequestHandler(ISessionFinder sessionFinder, ILogger<RequestMuleRequestHandler> logger) : base(logger)
    {
        _sessionFinder = sessionFinder;
        _logger = logger;
    }

    public RequestAction RequestAction => RequestAction.REQUEST_MULE;

    public async Task HandleRequestAsync(JObjectBotRequest request)
    {
        var muleRequest = request.Data.ToObject<MuleRequest>();

        BotSession? muleSession = _sessionFinder.FindMuleSessionForFarm(muleRequest.FarmType);
        if (muleSession == null)
        {
            _logger.LogInformation("Mule for bot: {rsn} {farm} not found", muleRequest.Rsn, muleRequest.FarmType);
            await SendCancelMule(request.WebSocket);
            return;
        }

        if (muleSession.WebSocket.State == WebSocketState.Closed)
        {
            _logger.LogInformation("Websocket for request: {@request} was closed", request);
            await SendCancelMule(request.WebSocket);
            return;
        }

        // Send confirmation to the bot and tell the mule to start 
        ServerRequest<MuleRequest> muleStartMuleRequest = new(RequestAction.REQUEST_MULE, muleRequest);
        ServerRequest<MuleRequest> botStartMuleRequest = new(RequestAction.REQUEST_MULE, muleRequest);

        await SendDataAsync(muleSession.WebSocket, muleStartMuleRequest);
        await SendDataAsync(request.WebSocket, botStartMuleRequest);

    }

    private async Task SendCancelMule(WebSocket websocket)
    {
        ServerRequest<object> cancelMule = new ServerRequest<object>(RequestAction.CANCEL_MULE);
        var cancelMuleRequestStr = JsonConvert.SerializeObject(cancelMule);
        await SendDataAsync(websocket, cancelMule);
        return;
    }
}
