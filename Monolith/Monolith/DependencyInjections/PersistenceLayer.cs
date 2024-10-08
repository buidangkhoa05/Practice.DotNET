﻿using Domain.Common.Persistence;
using Infrastructure.Data.Common;
using Persistence.DbContexts;
using Persistence.Interceptors;

namespace Monolith.DependencyInjections
{
    public static class PersistenceLayer
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<AuditDataInterceptor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
