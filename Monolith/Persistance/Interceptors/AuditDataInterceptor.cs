using Domain.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Persistence.Interceptors
{
    public class AuditDataInterceptor : SaveChangesInterceptor, ISaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditEntities(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entities = context.ChangeTracker.Entries().ToList();

            foreach (var entity in entities)
            {
                if (entity.Entity is IAuditable auditable)
                {
                    if (entity.State == EntityState.Added)
                    {
                        auditable.CreatedDate = utcNow;
                        auditable.UpdatedDate = utcNow;
                        auditable.CreatedBy = 0;
                        auditable.UpdatedBy = 0;
                    }
                    else if (entity.State == EntityState.Modified  || entity.HasChangedOwnedEntities()) 
                    {
                        auditable.UpdatedDate = utcNow;
                        auditable.UpdatedBy = 0;
                    }
                }
            }

        }
    }

    public static class InterceptorExtensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry)
        {
            return entry.References.Any(r =>
                r.TargetEntry != null
                && r.TargetEntry.Metadata.IsOwned()
                && (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
        }
    }
}
