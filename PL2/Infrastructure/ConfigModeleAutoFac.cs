using Autofac;

namespace PL.Infrastructure
{
    public class ConfigModeleAutoFac : Autofac.Module
    {
        private static string connectionString = "Data Source=DESKTOP-9DGMGOJ;Initial Catalog=KursovayaDb;Integrated Security=True";
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DL.Repositories.ClientEntiryRepo>()
               .As<DL.Repositories.Abstract.IClientEntiryRepo>()
               .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.ComponetEntityRepository>()
                .As<DL.Repositories.Abstract.IComponetEntityRepository>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.OrderEntityRepository>()
                .As<DL.Repositories.Abstract.IOrderEntityRepository>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.OrderInfoEntityRepository>()
                .As<DL.Repositories.Abstract.IOrderInfoEntityRepository>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.RoleEntityRepository>()
                .As<DL.Repositories.Abstract.IRoleEntityRepository>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.WorkerEntityRepo>()
                .As<DL.Repositories.Abstract.IWorkerEntityRepo>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.UserRoleRepository>()
                .As<DL.Repositories.Abstract.IUserRoleRepository>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DL.Repositories.UserEntityRepo>()
               .As<DL.Repositories.Abstract.IUserEntityRepo>()
               .WithParameter("connectionString", connectionString);
            
            builder.RegisterType<DL.Repositories.ServiceEntityRepository>()
                .As<DL.Repositories.Abstract.IServiceEntityRepository>()
                .WithParameter("connectionString", connectionString);
            
            builder.RegisterType<DL.Repositories.СomponetServiceEntityRepo>()
                .As<DL.Repositories.Abstract.IСomponetServiceEntityRepo>()
                .WithParameter("connectionString", connectionString);
        }
    }
}
