using BotServer.Lib.Data.Enum;

namespace BotServer.Lib.Data.Requests.Client;

public sealed class RegistrationRequest
{
    public string Rsn { get; set; }
    public FarmType FarmType { get; set; }
    public BotType BotType { get; set; }
}
