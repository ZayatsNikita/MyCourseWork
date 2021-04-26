﻿using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using BL.DtoModels;
using BL.Mappers;
using AutoMapper;
using DL.Entities;
using System.Linq;
namespace BL.Services
{
    public class OrderInfoServices : Abstract.IOrderInfoServices
    {
        private IOrderInfoEntityRepository _repository;
        private Mapper _mapper;
        private Abstract.IBuildStandartServices _buildStandartServices;
        public OrderInfoServices(IOrderInfoEntityRepository repository, Abstract.IBuildStandartServices buildStandartServices, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
            _buildStandartServices = buildStandartServices;
        }

        public void Create(OrderInfo orderInfo)
        {
            OrderInfoEntity s = _mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo);
            _repository.Create(_mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo));
        }

        public void Delete(OrderInfo orderInfo)
        {
            _repository.Delete(_mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo));
        }

        public List<OrderInfo> Read(int minId = -1, int maxId = -1, int minCountOfServicesRendered = -1, int maxCountOfServicesRendered = -1, int minServiceId = -1, int maxServiceId = -1, int minOrderNumber = -1, int maxOrderNumber = -1)
        {
            
            List<OrderInfoEntity> entities = _repository.Read(minId, maxId, minCountOfServicesRendered, maxCountOfServicesRendered, minServiceId, maxServiceId, minOrderNumber, maxOrderNumber);
            var orderInfoEnumerable = entities.Select(x => new OrderInfo()
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                CountOfServicesRendered = x.CountOfServicesRendered,
                BuildStandart = _buildStandartServices.Read(minId: x.ServiceId, maxId: x.ServiceId).FirstOrDefault()
            });

            List<OrderInfo> result = orderInfoEnumerable.ToList();

            return result;
        }

        public void Update(OrderInfo orderInfo, int OrderNumber, int CountOfServicesRendered, int ServiceId)
        {
            _repository.Update(_mapper.Map<OrderInfo, OrderInfoEntity>(orderInfo),OrderNumber, CountOfServicesRendered, ServiceId);
        }
    }

   


}