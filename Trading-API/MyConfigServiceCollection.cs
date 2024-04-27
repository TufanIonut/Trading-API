using Trading_API.Infrastructure;
using Trading_API.Interfaces;
using Trading_API.Repository;

namespace Trading_API
{
    public static class MyConfigServiceCollection
    {
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            //Database Connection
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            //----------------------------------------------
            //User Repository
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
