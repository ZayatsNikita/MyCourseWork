using AutoMapper;
using PL.Models;
using System.Collections.Generic;
namespace PL.Infrastructure.Services
{
    public class ComponentServices : Abstract.IComponentServices
    {
        private BL.Services.Abstract.IComponetServices _componentService;
        private Mapper _mapper;
        public ComponentServices(BL.Services.Abstract.IComponetServices componentService, Mapper mapper) 
        {
            _componentService = componentService;
            _mapper = mapper;
        }

        public void Create(Componet componet)
        {
            _componentService.Create(_mapper.Map<Componet, BL.DtoModels.Componet>(componet));
        }


        public void Delete(Componet componet)
        {
            _componentService.Delete(_mapper.Map<Componet, BL.DtoModels.Componet>(componet));
        }

        public List<Componet> Read(int minId, int maxId, string title,string productionStandards, decimal maxPrice, decimal minPrice)
        {
            List<Componet> result = _mapper.Map<List<BL.DtoModels.Componet>, List<Componet>> (_componentService.Read(minId, maxId, title, productionStandards, maxPrice, minPrice));
            return result;
        }

        public void Update(Componet componet)
        {
            _componentService.Update(_mapper.Map<Componet, BL.DtoModels.Componet>(componet), componet.Title,componet.ProductionStandards, componet.Price);
        }
    }
}
