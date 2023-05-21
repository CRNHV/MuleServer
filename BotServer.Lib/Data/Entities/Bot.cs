using System.ComponentModel.DataAnnotations;
using BotServer.Lib.Data.Enum;

namespace BotServer.Lib.Data.Entities;

public class Bot
{
    [Key]
    public string Rsn { get; set; }
    public BotType BotType { get; set; }
    public FarmType FarmType { get; set; }

    public Bot(string username, BotType botType, FarmType farmType)
    {
        Rsn = username;
        BotType = botType;
        FarmType = farmType;
    }
}
