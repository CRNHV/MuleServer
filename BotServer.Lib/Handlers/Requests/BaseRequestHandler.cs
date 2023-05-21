using System.Net.WebSockets;
using System.Text;
using BotServer.Lib.Data.Requests.Server;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BotServer.Lib.Handlers.Requests;

internal class BaseRequestHandler
{
    private readonly ILogger _logger;

    public BaseRequestHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task SendDataAsync<T>(WebSocket webSocket, ServerRequest<T> request) where T : class
    {
        var muleRequestJsonStr = JsonConvert.SerializeObject(request);
        await SendDataAsync(webSocket, muleRequestJsonStr);
    }

    private async Task SendDataAsync(WebSocket webSocket, string data)
    {
        _logger.LogInformation("Send data: {data}", data);

        if (webSocket.State == WebSocketState.Open)
        {
            await webSocket.SendAsync(
            new ArraySegment<byte>(Encoding.ASCII.GetBytes(data)),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);
        }
    }
}
