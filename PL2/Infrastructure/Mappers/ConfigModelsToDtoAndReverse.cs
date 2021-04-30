using AutoMapper;
using System.Collections.Generic;
namespace PL.Infrastructure.Mappers
{
    public class ConfigModelsToDtoAndReverse : Profile
    {
        public ConfigModelsToDtoAndReverse()
        {
            CreateMap<BL.DtoModels.Client, PL.Models.Client>().ReverseMap();
            CreateMap<BL.DtoModels.Componet, PL.Models.Componet>().ReverseMap();
            CreateMap<BL.DtoModels.Order, PL.Models.Order>().ReverseMap();
            CreateMap<BL.DtoModels.OrderInfo, PL.Models.OrderInfo>().ReverseMap();
            CreateMap<BL.DtoModels.Role, PL.Models.Role>().ReverseMap();
            CreateMap<BL.DtoModels.Service, PL.Models.Service>().ReverseMap();
            CreateMap<BL.DtoModels.Combined.BuildStandart, PL.Models.BuildStandart>().ReverseMap();
            CreateMap<BL.DtoModels.User, PL.Models.User>().ReverseMap();
            CreateMap<BL.DtoModels.Worker, PL.Models.Worker>().ReverseMap();
            CreateMap<KeyValuePair<BL.DtoModels.Worker, decimal>, KeyValuePair<PL.Models.Worker, decimal>>().ReverseMap();
            CreateMap<BL.DtoModels.UserRole, PL.Models.UserRole>().ReverseMap();
            CreateMap<BL.DtoModels.Combined.FullUser, PL.Models.ModelsForView.FullUser>().ReverseMap();
        }
    }
}
