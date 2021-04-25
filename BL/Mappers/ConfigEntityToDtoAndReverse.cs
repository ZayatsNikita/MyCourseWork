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
            CreateMap<Componet, ComponetEntity>().ReverseMap();
            CreateMap<ServiceComponent, СomponetServiceEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<OrderInfo, OrderInfoEntity>().ReverseMap();
            CreateMap<Role, RoleEntity>().ReverseMap();
            CreateMap<Service, ServiceEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<Worker, WorkerEntity>().ReverseMap();
            CreateMap<UserRole, UserRoleEntity>().ReverseMap();
            CreateMap<FullOrderInfo, OrderInfo>()
                .ForMember("CountOfServicesRendered", x => x.MapFrom(y => y.CountOfServiceOrdered))
                .ForMember("ServiceId", x => x.MapFrom(y => y.BuildStandart.Id))
                .ForMember("Id", x => x.MapFrom(z => z.Id))
                .ForMember("OrderNumber", x => x.MapFrom(z => 0));
        }
    }
}
