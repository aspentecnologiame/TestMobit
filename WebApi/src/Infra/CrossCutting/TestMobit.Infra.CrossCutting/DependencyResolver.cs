using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestMobit.Infra.Data.DatabaseRepository.RepositoryResolver;
using TestMobit.Service.ServiceResolver;

namespace TestMobit.Infra.CrossCutting
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterCrossCuttingDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServicesDependencies();
            services.RegisterSqlRepositoriesDependencies();
            //services.RegisterRabbitMQRepositoriesDependencies();
            //services.RegisterWorkerDependencies();

            return services;
        }
    }
}
