using BotServer.Lib.Data.Enum;
using BotServer.Lib.Data.Models;

namespace BotServer.Lib.Session.Interfaces;

internal interface ISessionFinder
{
    BotSession FindBotByName(string muleRsn);
    BotSession FindMuleSessionForFarm(FarmType farmType);
}
