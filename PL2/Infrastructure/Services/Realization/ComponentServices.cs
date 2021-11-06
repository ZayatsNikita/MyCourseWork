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
            _componentService.Create(_mapper.Map<Componet, BL.DtoModels.Component>(componet));
        }


        public void Delete(Componet componet)
        {
            _componentService.Delete(componet.Id);
        }

        public List<Componet> Read()
        {
            List<Componet> result = _mapper.Map<List<BL.DtoModels.Component>, List<Componet>> (_componentService.Read());
            return result;
        }

        public Componet ReadById(int id)
        {
            return _mapper.Map<BL.DtoModels.Component, Componet>(_componentService.ReadById(id));
        }

        public void Update(Componet componet)
        {
            _componentService.Update(_mapper.Map<Componet, BL.DtoModels.Component>(componet));
        }
    }
}
