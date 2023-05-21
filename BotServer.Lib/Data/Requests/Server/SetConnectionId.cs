namespace BotServer.Lib.Data.Requests.Server;

internal class SetConnectionId
{
    public Guid ConnectionId { get; set; }

    public SetConnectionId(Guid connectionId)
    {
        this.ConnectionId = connectionId;
    }
}
