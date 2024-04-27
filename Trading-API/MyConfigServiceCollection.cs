using Trading_API.Infrastructure;
using Trading_API.Interfaces;

namespace Trading_API
{
    public static class MyConfigServiceCollection
    {
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            //Database Connection
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            //----------------------------------------------
            return services;
        }
    }
}
