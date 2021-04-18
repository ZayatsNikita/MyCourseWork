using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;

namespace PL.Infrastructure
{
    public class ConfigModeleAutoFac : Autofac.Module
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713";
        protected override void Load(ContainerBuilder builder)
        {

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
        }
    }
}
