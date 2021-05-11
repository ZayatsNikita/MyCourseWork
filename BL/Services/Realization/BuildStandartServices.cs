using AutoMapper;
using BL.DtoModels;
using BL.DtoModels.Combined;
using BL.Services.Abstract;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
using BL.Services.Validaton;

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
            if (BuildStandartServiceValidation.IsValid(buildStandart, Read()))
            {
                _repository.Create(_mapper.Map<ServiceComponent, СomponetServiceEntity>(new ServiceComponent()
                {
                    Id = buildStandart.Id,
                    ComponetId = buildStandart.Componet.Id,
                    ServiceId = buildStandart.Service.Id
                }));
            }
        }

        public void Delete(BuildStandart buildStandart)
        {
            _repository.Delete(_mapper.Map<ServiceComponent, СomponetServiceEntity>(new ServiceComponent()
            {
                Id = buildStandart.Id
            }));
        }

        public List<BuildStandart> Read(int minId = Constants.DefIntVal, int maxId = Constants.DefIntVal, int minServiceId = Constants.DefIntVal, int maxServiceId = Constants.DefIntVal, int minComponetId = Constants.DefIntVal, int maxComponetId = Constants.DefIntVal)
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

        public void Update(BuildStandart buildStandart, int serviceId = Constants.DefIntVal, int componetId = Constants.DefIntVal)
        {
            _repository.Update(_mapper.Map<ServiceComponent, СomponetServiceEntity>(new ServiceComponent {Id = buildStandart.Id }), serviceId, componetId);
        }
    }
}
