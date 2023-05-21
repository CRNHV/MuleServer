using System.Net.WebSockets;
using BotServer.Lib.Data.Entities;
using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Models;
using BotServer.Lib.Services.Interfaces;
using BotServer.Lib.Session.Interfaces;
using Microsoft.Extensions.Logging;

namespace BotServer.Lib.Services;

internal sealed class RegistrationService : IRegistrationService
{
    private readonly IBotSessionHolder _botSessionHolder;
    private readonly ILogger<RegistrationService> _logger;

    public RegistrationService(IBotSessionHolder botSessionHolder, ILogger<RegistrationService> logger)
    {
        _botSessionHolder = botSessionHolder;
        _logger = logger;
    }

    public Guid RegisterConnection(FarmType farmType, BotType botType, string loginName, WebSocket webSocket)
    {
        _logger.LogInformation("Registration: {farmType} {botType} {loginName}", farmType, botType, loginName);

        Bot newBot = new(loginName, botType, farmType);
        BotSession session = new()
        {
            Bot = newBot,
            WebSocket = webSocket
        };

        return _botSessionHolder.AddSession(session);
    }

    public void UnRegisterConnection(Guid connectionId)
    {
        _botSessionHolder.RemoveSession(connectionId);
    }
}
