using BotServer.Lib.Data.Models;

namespace BotServer.Lib.Session.Interfaces;

public interface IBotSessionHolder
{
    Guid AddSession(BotSession session);
    void RemoveSession(Guid connectionId);
    KeyValuePair<Guid, BotSession>? GetSession(Func<KeyValuePair<Guid, BotSession>, bool> query);
    public List<BotSession> GetSessions();
}
