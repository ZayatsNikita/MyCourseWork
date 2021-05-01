using AutoMapper;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using BL.Services.Validaton;
namespace BL.Services
{
    public class ServiceServices : Abstract.IServiceServices
    {
        private IServiceEntityRepository _repository;
        private Mapper _mapper;
        public ServiceServices(IServiceEntityRepository repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(Service service)
        {
            if (ServiceValidationService.IsValid(service))
            {
                _repository.Create(_mapper.Map<Service, ServiceEntity>(service));
            }
        }

        public void Delete(Service service)
        {
            _repository.Delete(_mapper.Map<Service, ServiceEntity>(service));
        }

        public List<Service> Read(int MinId =-1, int MaxId = -1, string Title=null, string Description=null, decimal maxPrice = -1, decimal minPrice = -1)
        {
            List<Service> result = _mapper.Map<List<ServiceEntity>, List<Service>>(_repository.Read(MinId, MaxId, Title, Description, maxPrice, minPrice));
            return result;
        }

        public void Update(Service service, string title, string description, decimal price)
        {
            if (ServiceValidationService.IsValid(service))
            {
                _repository.Update(_mapper.Map<Service, ServiceEntity>(service), title, description, price);
            }
        }
    }  
}
