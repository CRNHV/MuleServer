using BotServer.Lib.Data.Requests.Client;

namespace BotServer.Lib.Handlers.Interfaces
{
    internal interface IMessageHandler
    {
        Task HandleMessageAsync(JObjectBotRequest request);
    }
}
