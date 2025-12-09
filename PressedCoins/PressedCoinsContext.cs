using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PressedCoins.Domain.Database.Models;
using PressedCoins.Domain.StronglyTypes;

namespace PressedCoins.Database;

public class PressedCoinsContext : DbContext
{
    public static string DbName = "PressedCoins";
    public PressedCoinsContext(DbContextOptions<PressedCoinsContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assemblyWithConfigurations = typeof(CoinId).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
    }
    
   public DbSet<Coin> Coins { get; set; }
}