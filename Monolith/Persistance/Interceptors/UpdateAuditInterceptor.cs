using Domain.Common.Entity.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Persistence.Interceptors
{
    public class UpdateAuditInterceptor : SaveChangesInterceptor, ISaveChangesInterceptor
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
                    else if (entity.State == EntityState.Modified)
                    {
                        auditable.UpdatedDate = utcNow;
                        auditable.UpdatedBy = 0;
                    }
                }
            }

        }
    }
}
