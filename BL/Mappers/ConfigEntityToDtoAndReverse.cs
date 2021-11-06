using AutoMapper;
using BL.DtoModels;
using BL.DtoModels.Combined;
using DL.Entities;

namespace BL.Mappers
{
    public class ConfigEntityToDtoAndReverse : Profile
    {
        public ConfigEntityToDtoAndReverse()
        {
            CreateMap<Client, ClientEntity>().ReverseMap();
            CreateMap<Component, ComponetEntity>().ReverseMap();
            CreateMap<ServiceComponent, ServiceComponentsEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<OrderInfo, OrderInfoEntity>().ReverseMap();
            CreateMap<Role, RoleEntity>().ReverseMap();
            CreateMap<Service, ServiceEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<Worker, WorkerEntity>().ReverseMap();
            CreateMap<UserRole, UserRoleEntity>().ReverseMap();
            CreateMap<OrderInfo, OrderInfoEntity>()
                .ForMember("ServiceId", x => x.MapFrom(y => y.BuildStandart.Id));
        }
    }
}
