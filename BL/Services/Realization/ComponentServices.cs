using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using BL.DtoModels;
using BL.Mappers;
using AutoMapper;
using DL.Entities;
using BL.Services.Validaton;
namespace BL.Services
{
    public class ComponentServices : Abstract.IComponetServices
    {
        private IComponetEntityRepository _repository;
        private Mapper _mapper;
        public ComponentServices(IComponetEntityRepository repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;        
        }

        public void Create(Componet componet)
        {
            if (ComponentValidationService.IsValid(componet))
            {
                _repository.Create(_mapper.Map<Componet, ComponetEntity>(componet));
            }
        }

        public void Delete(Componet componet)
        {
            _repository.Delete(_mapper.Map<Componet, ComponetEntity>(componet));
        }

        public List<Componet> Read(int minId, int maxId, string title, decimal maxPrice, decimal minPrice)
        {
            List<Componet> result= _mapper.Map<List<ComponetEntity>, List<Componet>>(_repository.Read(minId,  maxId, title,  maxPrice, minPrice));
            return result;
        }
        public void Update(Componet componet, string title, decimal Price)
        {
            if (ComponentValidationService.IsValid(componet))
            {
                _repository.Update(_mapper.Map<Componet, ComponetEntity>(componet), title, Price);
            }
        }
    }
}
