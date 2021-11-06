using AutoMapper;
using BL.DtoModels;
using BL.Services.Abstract;
using BL.Services.Validaton;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services
{
    public class ComponentServices : IComponetServices
    {
        private IComponetEntityRepository _repository;

        private IComponentValidator _componentValidator;

        private Mapper _mapper;
        
        public ComponentServices(IComponetEntityRepository repository, Mapper mapper, IComponentValidator componentValidator)  
        {
            _mapper = mapper;

            _repository = repository;

            _componentValidator = componentValidator;
        }

        public int Create(Component componet)
        {
            _componentValidator.CheckForValidity(componet);
            
            var id = _repository.Create(_mapper.Map<Component, ComponetEntity>(componet));
            
            return id;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Component> Read()
        {
            List<Component> result= _mapper.Map<List<ComponetEntity>, List<Component>>(_repository.Read());

            return result;
        }

        public Component ReadById(int id) => _mapper.Map<ComponetEntity, Component>(_repository.ReadById(id));

        public void Update(Component componet)
        {
            _componentValidator.CheckForValidity(componet);

            _repository.Update(_mapper.Map<Component, ComponetEntity>(componet));
        }
    }
}
