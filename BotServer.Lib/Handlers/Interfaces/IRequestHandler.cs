using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Requests.Client;

namespace BotServer.Lib.Handlers.Interfaces;

internal interface IRequestHandler
{
    public RequestAction RequestAction { get; }
    public Task HandleRequestAsync(JObjectBotRequest request);
}
