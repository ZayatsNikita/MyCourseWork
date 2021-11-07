using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using BL.Services.Abstract;
using BL.Services.Validaton;
using BL.Services.Abstract.ValidationInterfaces;

namespace PL.Infrastructure.ServiceCollectionExtensions
{
    public static class BllServicesInjection
    {
        public static void AddBllManagers(this IServiceCollection services)
        {
            services.AddSingleton<IChartManager, ChartManager>();
            services.AddSingleton<IServiceComponentsService, ServiceComponentsService>();
            services.AddSingleton<IRoleServices, RoleServices>();
            services.AddSingleton<IWorkerServices, WorkerServices>();
            services.AddSingleton<IUserRoleServices, UserRoleServises>();
            services.AddSingleton<IUserServices, UserServisces>();
            services.AddSingleton<IServiceServices, ServiceServices>();
            services.AddSingleton<IComponetServices, ComponentServices>();
            services.AddSingleton<IClientServices, ClientServices>();
            services.AddSingleton<IOrderServices, OrderServices>();
            services.AddSingleton<IOrderInfoServices, OrderInfoServices>();

            services.AddBllValidators();
        }

        public static void AddBllValidators(this IServiceCollection services)
        {
            services.AddScoped<IClientValidator, ClientValidationService>();
            services.AddScoped<IComponentValidator, ComponentValidationService>();
            services.AddScoped<IFullUserValidator, FullUserValidationService>();
            services.AddScoped<IServiceComponentsValidator, ServiceComponentsValidator>();
            services.AddScoped<IServiceValidator, ServiceValidationService>();
            services.AddScoped<IUserValidator, UserValidationService>();
            services.AddScoped<IWorkerValidator, WorkerValidationService>();
        }
    }
}
