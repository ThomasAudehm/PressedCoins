using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PressedCoins.Domain.Database.Models;

namespace PressedCoins.Domain.Database.Interceptors;

public sealed class DateTrackingInterceptor : ISaveChangesInterceptor 
{
    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
        {
            return result;
        }
        eventData.Context.DateTracking();
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
           return new ValueTask<InterceptionResult<int>>(result);
        }
        
        eventData.Context.DateTracking();
        return new ValueTask<InterceptionResult<int>>(result);
    } 
}

public static class DateTrackingInterceptorExtensions
{
    public static void DateTracking(this DbContext context)
    {
        var entities = context.ChangeTracker.Entries()
            .Where(x => x.GetType().GetInterfaces().Contains(typeof(IDateTracking)))
            .ToList();

        if (entities.Count == 0)
        {
            return;
        }

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(nameof(IDateTracking.DateCreated)).CurrentValue = DateTime.Now;
                entity.Property(nameof(IDateTracking.DateModified)).CurrentValue = DateTime.Now;
            }

            if (entity.State == EntityState.Modified)
            {
                entity.Property(nameof(IDateTracking.DateModified)).CurrentValue = DateTime.Now;
            }
        }
    }
}
