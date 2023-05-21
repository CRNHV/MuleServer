using System.Net.WebSockets;
using BotServer.Lib.Data.Entities;

namespace BotServer.Lib.Data.Models;

public sealed class BotSession
{
    public WebSocket WebSocket { get; set; }
    public Bot Bot { get; set; }
}
