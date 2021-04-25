using System;
using DL.Repositories.Abstract;
using DL.Repositories;
using DL.Entities;
using BL.Services.Abstract;
using BL.Services;
using BL.DtoModels;
using System.Collections.Generic;
using BL.DtoModels.Combined;
using AutoMapper;
using BL.Mappers;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogInTests_InCorrectData_ArgumentException();
            LogInTests_CorrectData_newFullClient();

        }
        public static void LogInTests_CorrectData_newFullClient()
        {

            var config = new MapperConfiguration(x=>x.AddProfile(new ConfigEntityToDtoAndReverse()));


            IUserEntityRepo userEntityRepo = new UserEntityRepo();
            IWorkerEntityRepo workerEntityRepo = new WorkerEntityRepo();
            IRoleEntityRepository roleEntityRepository = new RoleEntityRepository();
            IUserRoleRepository userRoleRepository = new UserRoleRepository();


            Mapper mapper = new Mapper(config);
            IUserServices userServices = new UserServices(userEntityRepo ,mapper);
            IWorkerServices workerServices = new WorkerServices(workerEntityRepo, mapper);
            IRoleServices roleServices = new RoleServices(roleEntityRepository, mapper);
            IUserRoleServices userRoleServices = new UserRoleServises(userRoleRepository, mapper);


            IFullUserServices fullUserServices = new FullUserServisces(userServices, workerServices, roleServices, userRoleServices);

            string login = "login";
            string password = "password";


            FullUser user = null;

            try
            {
                user = fullUserServices.Read(login, password);
            }
            catch (ArgumentException)
            {

            }

            bool actual = user!= null;

        }
        public static void LogInTests_InCorrectData_ArgumentException()
        {

            var config = new MapperConfiguration(x => x.AddProfile(new ConfigEntityToDtoAndReverse()));


            IUserEntityRepo userEntityRepo = new UserEntityRepo();
            IWorkerEntityRepo workerEntityRepo = new WorkerEntityRepo();
            IRoleEntityRepository roleEntityRepository = new RoleEntityRepository();
            IUserRoleRepository userRoleRepository = new UserRoleRepository();


            Mapper mapper = new Mapper(config);
            IUserServices userServices = new UserServices(userEntityRepo, mapper);
            IWorkerServices workerServices = new WorkerServices(workerEntityRepo, mapper);
            IRoleServices roleServices = new RoleServices(roleEntityRepository, mapper);
            IUserRoleServices userRoleServices = new UserRoleServises(userRoleRepository, mapper);


            IFullUserServices fullUserServices = new FullUserServisces(userServices, workerServices, roleServices, userRoleServices);

            string login = "login";
            string password = "passwords";


            FullUser user = null;

            try
            {
                user = fullUserServices.Read(login, password);
            }
            catch (ArgumentException)
            {
              
            }

            bool actual = user == null;

        }
    }
}
