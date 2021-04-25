using AutoMapper;
using BL.DtoModels.Combined;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Linq;
using System.Collections.Generic;
using BL.Services.Abstract;
namespace BL.Services
{
    public class BuildStandartServices : Abstract.IBuildStandartServices
    {
        private IСomponetServiceEntityRepo _repository;
        IComponetServices _componetServices;
        IServiceServices _serviceServices;
        private Mapper _mapper;
        public BuildStandartServices(IComponetServices componetServices, IServiceServices serviceServices, IСomponetServiceEntityRepo repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _componetServices = componetServices;
            _serviceServices = serviceServices;
        }

        public void Create(BuildStandart buildStandart)
        {
            _repository.Create(_mapper.Map<ServiceComponent, СomponetServiceEntity>(new ServiceComponent()
            {   
                Id = buildStandart.Id,
                ComponetId = buildStandart.Componet.Id,
                ServiceId = buildStandart.Service.Id   
            }));
        }

        public void Delete(BuildStandart buildStandart)
        {
            _repository.Delete(_mapper.Map<ServiceComponent, СomponetServiceEntity>(new ServiceComponent()
            {
                Id = buildStandart.Id
            }));
        }

        public List<BuildStandart> Read(int minId = -1, int maxId = -1, int minServiceId = -1, int maxServiceId = -1, int minComponetId = -1, int maxComponetId = -1)
        {
            List<ServiceComponent> serviceComponents = _mapper.Map<List<СomponetServiceEntity>, List<ServiceComponent>>(_repository.Read(minId, maxId, minServiceId, maxServiceId, minComponetId, maxComponetId));
            List<BuildStandart> result = new List<BuildStandart>(serviceComponents.Count);

            int length = serviceComponents.Count;

            for (int i = 0; i < length; i++)
            {
                result.Add(new BuildStandart());
                result[i].Id = serviceComponents[i].Id;
                result[i].Componet = _componetServices.Read(minId: serviceComponents[i].ComponetId, maxId: serviceComponents[i].ComponetId).FirstOrDefault();
                result[i].Service = _serviceServices.Read(MinId: serviceComponents[i].ServiceId, MaxId: serviceComponents[i].ServiceId).FirstOrDefault();
            }

            return result;
        }

        public void Update(BuildStandart buildStandart, int serviceId = -1, int componetId = -1)
        {
            _repository.Update(_mapper.Map<ServiceComponent, СomponetServiceEntity>(new ServiceComponent {Id = buildStandart.Id }), serviceId, componetId);
        }
    }
}
