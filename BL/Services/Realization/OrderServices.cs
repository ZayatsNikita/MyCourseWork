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
    public class OrderServices : Abstract.IOrderServices
    {
        private IOrderEntityRepository _repository;
        private Mapper _mapper;
        public OrderServices(IOrderEntityRepository repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(Order order)
        {
            _repository.Create(_mapper.Map<Order, OrderEntity>(order));
        }

        public void Delete(Order order)
        {
            _repository.Delete(_mapper.Map<Order, OrderEntity>(order));
        }

        public List<Order> Read(int minId, int maxId, int minMasterId, int maxMasterId, int minManagerId, int maxManagerId, DateTime? minStartDate, DateTime? maxStartDate, DateTime? minCompletionDate, DateTime? maxCompletionDate, int minClientId, int maxClientId)
        {
            List<Order> result = _mapper.Map<List<OrderEntity>, List<Order>>(_repository.Read(minId,  maxId, minMasterId,  maxMasterId, minManagerId,  maxManagerId, minStartDate, maxStartDate, minCompletionDate,  maxCompletionDate, minClientId, maxClientId));
            return result;
        }

        public List<Order> ReadOutstandingOrders()
        {
            List<Order> result = _mapper.Map<List<OrderEntity>, List<Order>>(_repository.ReadOutstandingOrders());
            return result;
        }

        public void Update(Order order, int ClientId, int MasterId, int ManagerId, DateTime? StartDate, DateTime? CompletionDate)
        {
            _repository.Update(_mapper.Map<Order, OrderEntity>(order), ClientId, MasterId, ManagerId, StartDate, CompletionDate);
        }
    }

   


}
