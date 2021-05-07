using AutoMapper;
using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class ServiceServices : Abstract.IServiceServices
    {
        private BL.Services.Abstract.IServiceServices _repository;
        private Mapper _mapper;
        public ServiceServices(BL.Services.Abstract.IServiceServices repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(Service service)
        {
            _repository.Create(_mapper.Map<Service, BL.DtoModels.Service>(service));
        }

        public void Delete(Service service)
        {
            _repository.Delete(_mapper.Map<Service, BL.DtoModels.Service>(service));
        }

        public List<Service> Read(int MinId = -1, int MaxId = -1, string Title = null, string Description = null, decimal maxPrice = -1, decimal minPrice = -1)
        {
            List<Service> result = _mapper.Map<List<BL.DtoModels.Service>, List<Service>>(_repository.Read(MinId, MaxId, Title, Description, maxPrice, minPrice));
            return result;
        }

        public void Update(Service service)
        {
            _repository.Update(_mapper.Map<Service, BL.DtoModels.Service>(service), service.Title, service.Description, service.Price);
        }
    }
}
