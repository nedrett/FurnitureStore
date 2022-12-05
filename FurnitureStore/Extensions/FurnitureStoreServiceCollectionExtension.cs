using FurnitureStore.Infrastructure.Data.Common;

namespace FurnitureStore.Extensions
{
    using Core.Contracts;
    using Core.Contracts.User;
    using Core.Services;
    using Core.Services.User;

    public static class FurnitureStoreServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IChairService, ChairService>();
            services.AddScoped<ISofaService, SofaService>();
            services.AddScoped<ITvTableService, TvTableService>();
            services.AddScoped<IArmChairService, ArmChairService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
