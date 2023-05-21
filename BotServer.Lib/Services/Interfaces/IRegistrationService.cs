using System.Net.WebSockets;
using BotServer.Lib.Data.Enum;

namespace BotServer.Lib.Services.Interfaces;

public interface IRegistrationService
{
    Guid RegisterConnection(FarmType farmType, BotType botType, string loginName, WebSocket webSocket);
    void UnRegisterConnection(Guid connectionId);
}
