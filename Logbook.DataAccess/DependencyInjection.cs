using EventsWeb.DataAccess.Entities;
using EventsWeb.DataAccess.Repositories;
using EventsWeb.DataAccess.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventsWeb.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddUnitOfWork(configuration);
            services.AddRepositories(configuration);
            services.AddIdentity();

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
                options.UseLazyLoadingProxies();
            });
        }

        private static void AddUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<DatabaseContext>();
        }
    }
}
