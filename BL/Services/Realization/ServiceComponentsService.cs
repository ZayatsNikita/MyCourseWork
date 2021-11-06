using AutoMapper;
using BL.DtoModels;
using BL.DtoModels.Combined;
using BL.Services.Validaton;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services
{
    public class ServiceComponentsService : Abstract.IServiceComponentsService
    {
        private IСomponetServiceEntityRepo _serviceComponentsRepository;

        private IComponetEntityRepository _componentRepository;

        private IServiceEntityRepository _serviceRepository;

        private IServiceComponentsValidator _serviceComponentsValidator;

        private Mapper _mapper;

        public ServiceComponentsService(IComponetEntityRepository componentRepository, IServiceEntityRepository serviceRepository, IСomponetServiceEntityRepo repository, Mapper mapper, IServiceComponentsValidator serviceComponentsValidator)
        {
            _mapper = mapper;

            _serviceComponentsValidator = serviceComponentsValidator;

            _serviceComponentsRepository = repository;
            
            _componentRepository = componentRepository;

            _serviceRepository = serviceRepository;
        }

        public int Create(FullServiceComponents serviceComponents)
        {
            _serviceComponentsValidator.CheckForValidy(serviceComponents);

            _serviceComponentsRepository.Create(_mapper.Map<ServiceComponent, ServiceComponentsEntity>(new ServiceComponent()
            {
                Id = serviceComponents.Id,
                ComponetId = serviceComponents.Componet.Id,
                ServiceId = serviceComponents.Service.Id
            }));

            return serviceComponents.Id;
        }

        public void Delete(int id)
        {
            _serviceComponentsRepository.Delete(id);
        }

        public List<FullServiceComponents> Read()
        {
            List<ServiceComponent> serviceComponents = _mapper.Map<List<ServiceComponentsEntity>, List<ServiceComponent>>(_serviceComponentsRepository.Read());

            List<FullServiceComponents> result = new List<FullServiceComponents>(serviceComponents.Count);

            int length = serviceComponents.Count;

            for (int i = 0; i < length; i++)
            {
                result.Add(new FullServiceComponents());
                result[i].Id = serviceComponents[i].Id;
                result[i].Componet = _mapper.Map<Component>(_componentRepository.ReadById(serviceComponents[i].ComponetId));
                result[i].Service = _mapper.Map<Service>(_serviceRepository.ReadById(serviceComponents[i].ServiceId));
            }

            return result;
        }

        public FullServiceComponents ReadById(int id)
        {
            var serviceComponents = _serviceComponentsRepository.ReadById(id);

            var fullServiceComponents = new FullServiceComponents
            {
                Id = serviceComponents.Id,
                Componet = _mapper.Map<Component>(_componentRepository.ReadById(serviceComponents.ComponetId)),
                Service = _mapper.Map<Service>(_serviceRepository.ReadById(serviceComponents.ServiceId)),
            };

            return fullServiceComponents;
        }

        public void Update(FullServiceComponents serviceComponents)
        {
            _serviceComponentsValidator.CheckForValidy(serviceComponents);

            _serviceComponentsRepository.Update(new ServiceComponentsEntity
            {
                Id = serviceComponents.Id,
                ComponetId = serviceComponents.Componet.Id,
                ServiceId = serviceComponents.Service.Id,
            });
        }
    }
}
