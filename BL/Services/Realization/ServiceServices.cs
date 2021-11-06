using AutoMapper;
using BL.DtoModels;
using BL.Services.Validaton;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using BL.Services.Abstract;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services
{
    public class ServiceServices : IServiceServices
    {
        private IServiceEntityRepository _repository;
        
        private Mapper _mapper;

        private IServiceValidator _serviceValidator;

        public ServiceServices(IServiceEntityRepository repository, Mapper mapper, IServiceValidator serviceValidator)  
        {
            _mapper = mapper;

            _repository = repository;

            _serviceValidator = serviceValidator;
        }

        public int Create(Service service)
        {
            _serviceValidator.CheckForValidity(service);
            
            var id = _repository.Create(_mapper.Map<Service, ServiceEntity>(service));

            return id;
        }

        public void Delete(int id) => _repository.Delete(id);
  
        public List<Service> Read() => _mapper.Map<List<ServiceEntity>, List<Service>>(_repository.Read());

        public Service ReadById(int id) => _mapper.Map<ServiceEntity, Service>(_repository.ReadById(id));

        public void Update(Service service)
        {
            _serviceValidator.CheckForValidity(service);

            _repository.Update(_mapper.Map<Service, ServiceEntity>(service));
        }
    }  
}
