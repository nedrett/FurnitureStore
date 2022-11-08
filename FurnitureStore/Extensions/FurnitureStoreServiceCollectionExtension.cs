using FurnitureStore.Infrastructure.Data.Common;

namespace FurnitureStore.Extensions
{
    using Core.Contracts;
    using Core.Services;

    public static class FurnitureStoreServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ITableService, TableService>();

            return services;
        }
    }
}
