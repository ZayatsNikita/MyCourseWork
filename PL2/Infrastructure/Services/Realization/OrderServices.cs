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
            _repository.Delete(order.Id);
        }

        public List<Order> Read()
        {
            return _mapper.Map<List<BL.DtoModels.Order>, List<Order>>(_repository.Read());
        }

        public Order ReadById(int id)
        {
            return _mapper.Map<BL.DtoModels.Order, Order>(_repository.ReadById(id));
        }

        public void Update(Order order)
        {
            _repository.Update(_mapper.Map<Order, BL.DtoModels.Order>(order));
        }
    }
}
