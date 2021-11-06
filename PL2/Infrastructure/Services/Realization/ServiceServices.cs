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
            _repository.Delete(service.Id);
        }

        public List<Service> Read()
        {
            List<Service> result = _mapper.Map<List<BL.DtoModels.Service>, List<Service>>(_repository.Read());
            return result;
        }

        public Service ReadById(int id)
        {
            return _mapper.Map<BL.DtoModels.Service, Service>(_repository.ReadById(id));
        }

        public void Update(Service service)
        {
            _repository.Update(_mapper.Map<Service, BL.DtoModels.Service>(service));
        }
    }
}
