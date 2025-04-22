using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestMobit.Domain.Interfaces.Repositories.Database;
using TestMobit.Domain.Interfaces.Repositories.Database.Base;
using TestMobit.Infra.Data.DatabaseRepository.Base;

namespace TestMobit.Infra.Data.DatabaseRepository.RepositoryResolver
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterSqlRepositoriesDependencies(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("TestMobit"));
            services.AddScoped(typeof(IDatabaseRepository<>), typeof(DataBaseRepository<>));

            typeof(DependencyResolver).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IRepository).IsAssignableFrom(t))
                .ToList()
                .ForEach(t =>
                    t.GetInterfaces()
                        .Where(i => typeof(IRepository).IsAssignableFrom(i))
                        .ToList()
                        .ForEach(i => services.AddScoped(i, t))
                );

            return services;
        }
    }
}
