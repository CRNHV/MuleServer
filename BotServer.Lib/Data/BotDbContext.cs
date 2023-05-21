using BotServer.Lib.Data.Entities;
using BotServer.Lib.Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace BotServer.Lib.Data;

internal class BotDbContext : DbContext
{
    public DbSet<Bot> Bots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bot>().HasData(new Bot("Doodsoorzaak", BotType.MULE, FarmType.VYRE));
    }
}
