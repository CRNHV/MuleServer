using System.Collections.Concurrent;
using BotServer.Lib.Data.Models;
using BotServer.Lib.Session.Interfaces;

namespace BotServer.Lib.Session;

internal sealed class BotSessionHolder : IBotSessionHolder
{
    private readonly ConcurrentDictionary<Guid, BotSession> _sessions;

    public BotSessionHolder()
    {
        _sessions = new ConcurrentDictionary<Guid, BotSession>();
    }

    public Guid AddSession(BotSession session)
    {
        var existingSession = _sessions.Where(x => x.Value.Bot.Rsn == session.Bot.Rsn).FirstOrDefault();
        if (existingSession.Value != null)
        {
            return existingSession.Key;
        }

        Guid sessionId = Guid.NewGuid();
        if (_sessions.TryAdd(sessionId, session))
        {
            return sessionId;
        }
        return Guid.Empty;
    }

    public void RemoveSession(Guid connectionId)
    {
        _sessions.TryRemove(connectionId, out BotSession? session);
    }

    public KeyValuePair<Guid, BotSession>? GetSession(Func<KeyValuePair<Guid, BotSession>, bool> query)
    {
        return _sessions.Where(query).FirstOrDefault();
    }

    public List<BotSession> GetSessions()
    {
        return _sessions.Select(x => x.Value).ToList();
    }
}
