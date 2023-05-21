using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Models;

namespace BotServer.Lib.Session.Interfaces;

internal class SessionFinder : ISessionFinder
{
    private readonly IBotSessionHolder _sessionHolder;

    public SessionFinder(IBotSessionHolder sessionHolder)
    {
        _sessionHolder = sessionHolder;
    }

    public BotSession FindBotByName(string muleRsn)
    {
        var keyValuePair = _sessionHolder.GetSession(kvp => kvp.Value.Bot.Rsn == muleRsn);
        if (!keyValuePair.HasValue)
        {
            // TODO:
            return null;
        }

        return keyValuePair.Value.Value;
    }

    public BotSession FindMuleSessionForFarm(FarmType farmType)
    {
        var keyValuePair = _sessionHolder.GetSession(kvp => kvp.Value.Bot.FarmType == farmType && kvp.Value.Bot.BotType == BotType.MULE);
        if (!keyValuePair.HasValue)
        {
            // TODO:
            return null;
        }

        return keyValuePair.Value.Value;
    }
}
