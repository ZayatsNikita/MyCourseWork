using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using DL.Repositories.Abstract;
using DL.Repositories.Realization.MongoDbRepostories;
using System.Threading.Tasks;

namespace PL.Infrastructure.ServiceCollectionExtensions
{
    public static class DlServicesInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientEntiryRepo, MongoDbClientRepository>();
            
            services.AddScoped<IComponetEntityRepository, MongoDbComponentRepository>();
            
            services.AddScoped<IOrderEntityRepository, MongoDbOrdersRepository>();
            
            services.AddScoped<IOrderInfoEntityRepository, MongoDbOrderInfoRepository>();

            services.AddScoped<IRoleEntityRepository, MongoDbRoleRepository>();

            services.AddScoped<IWorkerEntityRepo, MongoDbWorkerRepository>();

            services.AddScoped<IUserRoleRepository, MongoDbUserRolePairsRepository>();

            services.AddScoped<IUserEntityRepo, MongoDbUserRepository>();

            services.AddScoped<IServiceEntityRepository, MongoDbServiceRepository>();
            
            services.AddScoped<IСomponetServiceEntityRepo, MongoDbComponentServicePairRepository>();
        }
    }
}
