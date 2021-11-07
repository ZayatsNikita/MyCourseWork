//using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PL.Infrastructure.ServiceCollectionExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PL.Infrastructure.Services;
using PL.Infrastructure.Services.Abstract;
using System.Collections.Generic;

namespace PL2
{
    public class Startup
    {
        [System.Obsolete]
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; private set; }

        //public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Регистрация сервисов для уровня представления
            services.AddSingleton<IChartManager, ChartManager>();
            services.AddSingleton<IWorkerServices, WorkerServices>();
            services.AddSingleton<IRoleServices, RoleServices>();
            services.AddSingleton<IFullUserServices, FullUserServisces>();
            services.AddSingleton<IServiceServices, ServiceServices>();
            services.AddSingleton<IComponentServices, ComponentServices>();
            services.AddSingleton<IBuildStandartService, BuildStandartService>();
            services.AddSingleton<IClientServices, ClientServices>();
            services.AddSingleton<IOrderServices, OrderServices>();
            services.AddSingleton<IOrderInfoServise, OrderInfoServices>();
            #endregion

            #region Регистрация сервисов для уровня бизнес логики
            services.AddBllManagers();
            services.AddRepositories();
            #endregion

            services.AddMemoryCache();
            services.AddSession();


            services.AddMvc(options => options.EnableEndpointRouting = false);
            
            
            #region регистрация сервисов для уровня доступа к данным

            var mapperConfigure = new MapperConfiguration(x =>
                x.AddProfiles(new List<AutoMapper.Profile>(){
                   new BL.Mappers.ConfigEntityToDtoAndReverse(),
                   new PL.Infrastructure.Mappers.ConfigModelsToDtoAndReverse()
                }));


            var mapper = new AutoMapper.Mapper(mapperConfigure);
            
            services.AddSingleton(mapper);
            #endregion

            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new PL.Infrastructure.ConfigModeleAutoFac());
        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(route => {
                route.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Acount", action = "Index" }
                    );

            });
        }
    }
}
