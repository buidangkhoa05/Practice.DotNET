using Domain.Common;
using Domain.Entities;
using Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Persistence.DataConfigurations;

namespace Infrastructure.Data.DbContexts
{
    public class ApplicationDbContext(
        AuditDataInterceptor auditDataInterceptor
    ) : DbContext
    {
        public virtual DbSet<TodoList> TodoList { get; set; }
        public virtual DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .AddInterceptors(auditDataInterceptor)
                .UseNpgsql(AppConfig.ConnectionStrings.DefaultConnection)
                .UseSnakeCaseNamingConvention();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
