using Netflex.Domain.Entities.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Netflex.Persistence.Interceptors;

public class DateTrackingEntityInterceptor
        : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext == null) return;
        foreach (var entry in dbContext.ChangeTracker.Entries<IDateTracking>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Added
                || entry.State == EntityState.Modified
                || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}