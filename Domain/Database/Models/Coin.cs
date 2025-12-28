using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressedCoins.Domain.StronglyTypes;

namespace PressedCoins.Domain.Database.Models;

public class Coin : IDateTracking
{
    public CoinId Id { get; init; }
    public required Motif Motif { get; init; } 
    public DateTime DateCreated { get; init; }
    
    public DateTime DateModified { get; init; }
} 

public class CoinConfiguration : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.HasKey(x=> x.Id);
        builder.Property(x => x.Id).HasVogenConversion().HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.Property(x => x.Motif).HasMaxLength(50);
        builder.ToTable(TableNames.Coins);
    }
}