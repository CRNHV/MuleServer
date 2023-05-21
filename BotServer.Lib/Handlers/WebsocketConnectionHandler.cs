using System.Net.WebSockets;
using System.Text;
using BotServer.Lib.Data.Requests.Client;
using BotServer.Lib.Handlers.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BotServer.Lib.Handlers;

internal class WebsocketConnectionHandler : IWebsocketConnectionHandler
{
    private readonly IMessageHandler _messageHandler;
    private readonly ILogger<WebsocketConnectionHandler> _logger;

    public WebsocketConnectionHandler(ILogger<WebsocketConnectionHandler> logger, IMessageHandler messageHandler)
    {
        _logger = logger;
        _messageHandler = messageHandler;
    }

    public async Task ProcessSocketAsync(WebSocket webSocket, TaskCompletionSource<object> taskCompletionSource)
    {
        _logger.LogInformation("Start processing new WebSocket connection ");

        int bufferLength = 1024 * 4;

        var buffer = new byte[bufferLength];
        var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        try
        {
            while (!receiveResult.CloseStatus.HasValue)
            {
                string message = Encoding.ASCII.GetString(buffer);
                if (string.IsNullOrEmpty(message))
                {
                    continue;
                }

                JObjectBotRequest? request = JsonConvert.DeserializeObject<JObjectBotRequest>(message);
                if (request == null)
                {
                    taskCompletionSource.SetResult(new object());
                    await webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        string.Empty,
                        CancellationToken.None);

                    return;
                }

                request.WebSocket = webSocket;
                await _messageHandler.HandleMessageAsync(request);

                for (int i = 0; i < bufferLength; i++)
                {
                    buffer[i] = 0;
                }

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.ToString());
        }
        finally
        {
            _logger.LogInformation("WebSocket connection closed");

            taskCompletionSource.TrySetResult(new object());
            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
