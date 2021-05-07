using AutoMapper;
using PL.Models;
using System;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class OrderServices : Abstract.IOrderServices
    {
        private BL.Services.Abstract.IOrderServices _repository;
        private Mapper _mapper;
        public OrderServices(BL.Services.Abstract.IOrderServices repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(Order order)
        {
            int id = _repository.Create(_mapper.Map<Order, BL.DtoModels.Order>(order));
            order.Id = id;
        }

        public void Delete(Order order)
        {
            _repository.Delete(_mapper.Map<Order, BL.DtoModels.Order>(order));
        }

        public List<Order> Read(int minId, int maxId, int minMasterId, int maxMasterId, int minManagerId, int maxManagerId, DateTime? minStartDate, DateTime? maxStartDate, DateTime? minCompletionDate, DateTime? maxCompletionDate, int minClientId, int maxClientId)
        {
            List<Order> result = _mapper.Map<List<BL.DtoModels.Order>, List<Order>>(_repository.Read(minId, maxId, minMasterId, maxMasterId, minManagerId, maxManagerId, minStartDate, maxStartDate, minCompletionDate, maxCompletionDate, minClientId, maxClientId));
            return result;
        }

        public void Update(Order order, int ClientId, int MasterId, int ManagerId, DateTime? StartDate, DateTime? CompletionDate)
        {
            _repository.Update(_mapper.Map<Order, BL.DtoModels.Order>(order), ClientId, MasterId, ManagerId, StartDate, CompletionDate);
        }
    }
}
