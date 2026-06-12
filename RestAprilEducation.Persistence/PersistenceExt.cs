using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Domain;

namespace RestAprilEducation.Persistence
{
    public static class PersistenceExt
    {
        public static void AddPersistenceExt(this IServiceCollection services, IConfiguration configuration)
        {
            // DELEGELER
            // built-in => Action, Predicate, Function

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"), sqlServerOptionsAction =>
                {

                    sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.GetName().Name);


                });
            });

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

            var applicationAssembly = typeof(Application.ApplicationAssembly).Assembly;
            var persistenceAssembly = typeof(PersistenceAssembly).Assembly;

            var repositoryInterfaces = applicationAssembly.GetTypes()
                .Where(t => t.IsInterface && t.Name.EndsWith("Repository"));

            foreach (var serviceType in repositoryInterfaces)
            {
                var implementationType = persistenceAssembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && serviceType.IsAssignableFrom(t));

                if (implementationType is not null)
                {
                    services.AddScoped(serviceType, implementationType);
                }

            }

            services.AddScoped<IUnitOfWork, UnitOfWork>(); 

            // services.AddScoped<IProductRepository, ProductRepositoryWithInMemory>();
        }


    }
}
