using AutoMapper;
using BL.dtoModels;
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
            _repository.Create(_mapper.Map<Service, ServiceEntity>(service));
        }

        public void Delete(Service service)
        {
            _repository.Delete(_mapper.Map<Service, ServiceEntity>(service));
        }

        public List<Service> Read(int MinId, int MaxId, int minInfoAboutComponentId, int maxInfoAboutComponentId, string Title, string Description, decimal maxPrice, decimal minPrice)
        {
            List<Service> result = _mapper.Map<List<ServiceEntity>, List<Service>>(_repository.Read(MinId, MaxId, minInfoAboutComponentId, maxInfoAboutComponentId, Title, Description, maxPrice, minPrice));
            return result;
        }

        public void Update(Service service, int infoAboutComponentId, string title, string description, decimal price)
        {
            _repository.Update(_mapper.Map<Service, ServiceEntity>(service), infoAboutComponentId , title, description , price);

        }
    }  
}
