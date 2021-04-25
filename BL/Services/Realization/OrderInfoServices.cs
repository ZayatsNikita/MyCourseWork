using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using BL.DtoModels;
using BL.Mappers;
using AutoMapper;
using DL.Entities;

namespace BL.Services
{
    public class OrderInfoServices : Abstract.IOrderInfoServices
    {
        private IOrderInfoEntityRepository _repository;
        private Mapper _mapper;
        public OrderInfoServices(IOrderInfoEntityRepository repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(OrderInfo orderInfo)
        {
            _repository.Create(_mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo));
        }

        public void Delete(OrderInfo orderInfo)
        {
            _repository.Delete(_mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo));
        }

        public List<OrderInfo> Read(int minId, int maxId, int minCountOfServicesRendered, int maxCountOfServicesRendered, int minServiceId, int maxServiceId, int minOrderNumber, int maxOrderNumber)
        {
            List<OrderInfo> result = _mapper.Map<List<OrderInfoEntity>, List<OrderInfo>>(_repository.Read(minId, maxId, minCountOfServicesRendered, maxCountOfServicesRendered, minServiceId, maxServiceId, minOrderNumber, maxOrderNumber));
            return result;
        }

        public void Update(OrderInfo orderInfo, int OrderNumber, int CountOfServicesRendered, int ServiceId)
        {
            _repository.Update(_mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo),OrderNumber, CountOfServicesRendered, ServiceId);
        }
    }

   


}
