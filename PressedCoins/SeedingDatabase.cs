using PressedCoins.Domain.Database.Models;

namespace PressedCoins.Database;

public class SeedingDatabase
{
    private readonly PressedCoinsContext _context;
    public SeedingDatabase(PressedCoinsContext context)
    {
        _context = context;
    }
    
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.EnsureDeletedAsync(cancellationToken);
        await _context.Database.EnsureCreatedAsync(cancellationToken); 
        await AddCoinsAsync();
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task AddCoinsAsync()
    {
        await _context.AddRangeAsync(
            new Coin
            {
                Subject = "Motive 1",
                DateCreated = DateTime.Now,
            },
            new Coin
            {
                Subject = "Motive 2",
                DateCreated = DateTime.Now,
            }
        );
    }
}