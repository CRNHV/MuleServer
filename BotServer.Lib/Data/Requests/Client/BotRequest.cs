using System.ComponentModel.DataAnnotations.Schema;
using System.Net.WebSockets;
using BotServer.Lib.Data.Enum;
using Newtonsoft.Json.Linq;

namespace BotServer.Lib.Data.Requests.Client;

public class BotRequest<T> where T : class
{
    public Guid? ConnectionId { get; set; }

    [NotMapped]
    public WebSocket WebSocket { get; set; }

    public RequestAction Action { get; set; }

    public T? Data { get; set; }
}

public sealed class JObjectBotRequest : BotRequest<JObject>
{

}