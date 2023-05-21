using System.Text;
using BotServer.Lib.Handlers.Interfaces;
using BotServer.Lib.Session.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotServer.Controllers;

public class WebSocketController : ControllerBase
{
    private readonly IWebsocketConnectionHandler _connectionHandler;
    private readonly IBotSessionHolder _botSessionHolder;
    private readonly ILogger<WebSocketController> _logger;

    public WebSocketController(IWebsocketConnectionHandler connectionHandler, ILogger<WebSocketController> logger, IBotSessionHolder botSessionHolder)
    {
        _connectionHandler = connectionHandler;
        _logger = logger;
        _botSessionHolder = botSessionHolder;
    }

    [Route("/ws")]
    public async Task AcceptWebsocket()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            _logger.LogInformation("Accepted new WebSocket request from IP: {IP}", HttpContext.Connection.RemoteIpAddress);

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var socketFinishedTcs = new TaskCompletionSource<object>();
            await _connectionHandler.ProcessSocketAsync(webSocket, socketFinishedTcs);
            await socketFinishedTcs.Task;
        }
        else
        {
            this.Response.StatusCode = 400;
            await Response.BodyWriter.WriteAsync(Encoding.ASCII.GetBytes("Error"));
            await Response.StartAsync();
        }
    }
}