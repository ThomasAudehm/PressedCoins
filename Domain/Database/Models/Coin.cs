using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressedCoins.Domain.StronglyTypes;

namespace PressedCoins.Domain.Database.Models;

public class Coin
{
    public CoinId Id { get; init; }
    public required string Subject { get; init; } 
    public DateTime DateCreated { get; init; }
    
} 

public class CoinConfiguration : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.HasKey(x=> x.Id);
        builder.Property(x => x.Id).HasConversion(new CoinId.EfCoreConverter()).HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.Property(x => x.Subject).HasMaxLength(50);
        builder.ToTable(TableNames.Coins);
    }
}