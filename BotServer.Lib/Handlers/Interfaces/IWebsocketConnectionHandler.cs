using System.Net.WebSockets;

namespace BotServer.Lib.Handlers.Interfaces;

public interface IWebsocketConnectionHandler
{
    Task ProcessSocketAsync(WebSocket webSocket, TaskCompletionSource<object> taskCompletionSource);
}
