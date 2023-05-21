using BotServer.Lib.Data.Enum;
using Newtonsoft.Json.Linq;

namespace BotServer.Lib.Data.Requests.Server;

internal class ServerRequest<T> where T : class
{
    public RequestAction Action { get; }

    public T? Data { get; }

    public ServerRequest(RequestAction action, T? data = null)
    {
        Data = data;
        Action = action;
    }
}

internal class JObjectServerRequest : ServerRequest<JObject>
{
    public JObjectServerRequest(RequestAction action, JObject? data = null) : base(action, data)
    {
    }
}