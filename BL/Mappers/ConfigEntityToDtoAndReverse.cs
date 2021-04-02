using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;
using BL.dtoModels;
using DL.Entities;
namespace BL.Mappers
{
    public class ConfigEntityToDtoAndReverse : Profile
    {
        public ConfigEntityToDtoAndReverse()
        {
            CreateMap<Client, ClientEntity>().ReverseMap();
            CreateMap<Componet, ComponetEntity>().ReverseMap();
            CreateMap<InformationAboutComponents, InformationAboutComponentsEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<OrderInfo, OrderInfoEntity>().ReverseMap();
            CreateMap<Role, RoleEntity>().ReverseMap();
            CreateMap<Service, ServiceEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<Worker, WorkerEntity>().ReverseMap();
        }
    }
}
