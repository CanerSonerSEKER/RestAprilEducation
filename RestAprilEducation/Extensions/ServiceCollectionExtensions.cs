using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using RestAprilEducation.Persistence;
using RestAprilEducation.Application;

namespace RestAprilEducation.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceRepositories(this IServiceCollection services)
        {
            var persistenceAssembly = typeof(PersistenceAssembly).Assembly;
            var applicationAssembly = typeof(ApplicationAssembly).Assembly;

            var repositoryTypes = persistenceAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

            foreach (var implType in repositoryTypes)
            {
                var serviceInterface = implType.GetInterfaces().FirstOrDefault(i => i.Assembly == applicationAssembly);
                if (serviceInterface != null)
                {
                    services.AddScoped(serviceInterface, implType);
                }
            }

            return services;
        }
    }
}
