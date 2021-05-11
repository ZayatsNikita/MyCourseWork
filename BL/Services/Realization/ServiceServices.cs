using AutoMapper;
using BL.DtoModels;
using BL.Services.Validaton;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
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

        public List<Service> Read(int MinId = Constants.DefIntVal, int MaxId = Constants.DefIntVal, string Title=null, string Description=null, decimal maxPrice = Constants.DefIntVal, decimal minPrice = Constants.DefIntVal)
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
