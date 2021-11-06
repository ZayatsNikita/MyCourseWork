using AutoMapper;
using BL.DtoModels;
using BL.DtoModels.Combined;
using BL.Services.Abstract;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class OrderInfoServices : IOrderInfoServices
    {
        private IOrderInfoEntityRepository _orderInfoRepository;
        
        private Mapper _mapper;
        
        private IСomponetServiceEntityRepo _serviceComponentsRepository;

        private IServiceEntityRepository _serviceEntityRepository;

        private IComponetEntityRepository _componetEntityRepository;

        public OrderInfoServices(IOrderInfoEntityRepository orderInfoRepository, IСomponetServiceEntityRepo serviceComponentsRepository,
            IComponetEntityRepository componetEntityRepository, IServiceEntityRepository serviceEntityRepository, Mapper mapper)  
        {
            _mapper = mapper;
            
            _orderInfoRepository = orderInfoRepository;

            _componetEntityRepository = componetEntityRepository;

            _serviceEntityRepository = serviceEntityRepository;

            _serviceComponentsRepository = serviceComponentsRepository;
        }

        public int Create(OrderInfo orderInfo)
        {
            var orderInfoEntity = new OrderInfoEntity
            {
                OrderNumber = orderInfo.OrderNumber,
                CountOfServicesRendered = orderInfo.CountOfServicesRendered,
                ServiceId = orderInfo.BuildStandart.Id,
                Id = orderInfo.Id,
            };

            var id = _orderInfoRepository.Create(orderInfoEntity);

            return id;
        }

        public void Delete(int id) => _orderInfoRepository.Delete(id);

        public List<OrderInfo> Read()
        {
            List<OrderInfoEntity> entities = _orderInfoRepository.Read();

            var orderInfoEnumerable = entities.Select(x => new OrderInfo()
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                CountOfServicesRendered = x.CountOfServicesRendered,
                BuildStandart = GetServiceComponents(x.ServiceId),
            });

            List<OrderInfo> result = orderInfoEnumerable.ToList();

            return result;
        }

        public OrderInfo ReadById(int id)
        {
            var orderInfoEntity = _orderInfoRepository.ReadById(id);

            return new OrderInfo
            {
                Id = orderInfoEntity.Id,
                OrderNumber = orderInfoEntity.OrderNumber,
                CountOfServicesRendered = orderInfoEntity.CountOfServicesRendered,
                BuildStandart = GetServiceComponents(orderInfoEntity.ServiceId),
            };
        }

        public void Update(OrderInfo orderInfo)
        {
            var orderInfoEntity = new OrderInfoEntity
            {
                OrderNumber = orderInfo.OrderNumber,
                CountOfServicesRendered = orderInfo.CountOfServicesRendered,
                ServiceId = orderInfo.BuildStandart.Id,
                Id = orderInfo.Id,
            };

            _orderInfoRepository.Update(orderInfoEntity);
        }

        private FullServiceComponents GetServiceComponents(int serviceCompoentId)
        {
            var serviceComponents = _serviceComponentsRepository.ReadById(serviceCompoentId);

            var component = _componetEntityRepository.ReadById(serviceComponents.ComponetId);

            var service = _serviceEntityRepository.ReadById(serviceComponents.ServiceId);

            return new FullServiceComponents
            {
                Id = serviceComponents.Id,
                Service = _mapper.Map<Service>(service),
                Componet = _mapper.Map<Component>(component),
            };
        }
    }
}
