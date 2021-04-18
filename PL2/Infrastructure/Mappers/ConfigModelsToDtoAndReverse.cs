using System;
using System.Collections.Generic;
using System.Text;

using BL.dtoModels;
using AutoMapper;
using PL.Models.ModelsForView;
using PL.Models;

namespace PL.Infrastructure.Mappers
{
    public class ConfigModelsToDtoAndReverse : Profile
    {
        public ConfigModelsToDtoAndReverse()
        {
            CreateMap<BL.dtoModels.Client, PL.Models.Client>().ReverseMap();
            CreateMap<BL.dtoModels.Componet, PL.Models.Componet>().ReverseMap();
            CreateMap<BL.dtoModels.InformationAboutComponents, PL.Models.InformationAboutComponents>().ReverseMap();
            CreateMap<BL.dtoModels.Order, PL.Models.Order>().ReverseMap();
            CreateMap<BL.dtoModels.OrderInfo, PL.Models.OrderInfo>().ReverseMap();
            CreateMap<BL.dtoModels.Role, PL.Models.Role>().ReverseMap();
            CreateMap<BL.dtoModels.Service, PL.Models.Service>().ReverseMap();
            CreateMap<BL.dtoModels.User, PL.Models.User>().ReverseMap();
            CreateMap<BL.dtoModels.Worker, PL.Models.Worker>().ReverseMap();
            CreateMap<BL.dtoModels.UserRole, PL.Models.UserRole>().ReverseMap();
            CreateMap<BL.dtoModels.Combined.FullUser, PL.Models.ModelsForView.FullUser>().ReverseMap();
        }
    }
}
