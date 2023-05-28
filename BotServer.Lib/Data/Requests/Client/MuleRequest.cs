using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Models;

namespace BotServer.Lib.Data.Requests.Client;

public class MuleRequest
{
    public FarmType FarmType { get; set; }
    public string Rsn { get; set; }
    public List<RequestedItem> Items { get; set; } = new List<RequestedItem>();
    public MulePoint WorldPoint { get; set; }
    public int World { get; set; }

    public MuleRequest Copy()
    {
        return new MuleRequest()
        {
            FarmType = FarmType,
            Rsn = Rsn,
            Items = Items,
            WorldPoint = WorldPoint,
            World = World,
        };
    }
}
