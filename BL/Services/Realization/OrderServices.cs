using AutoMapper;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public int Create(Order order) => _repository.Create(_mapper.Map<Order, OrderEntity>(order));

        public void Delete(int id) => _repository.Delete(id);

        public List<Order> Read() => _mapper.Map<List<OrderEntity>, List<Order>>(_repository.Read());

        public Order ReadById(int id) => _mapper.Map<OrderEntity, Order>(_repository.ReadById(id));

        public List<Order> ReadComplitedOrders(DateTime? from, DateTime? to)
        {
            var orders = _mapper.Map<IEnumerable<OrderEntity>, IEnumerable<Order>>(_repository.ReadComplitedOrders());

            if (from != null)
            {
                orders = orders.Where(x => x.StartDate >= from);
            }

            if (to != null)
            {
                orders = orders.Where(x => x.StartDate <= to);
            }

            return orders.ToList();
        }

        public List<Order> ReadOutstandingOrders(DateTime? from, DateTime? to)
        {
            var orders = _mapper.Map<IEnumerable<OrderEntity>, IEnumerable<Order>>(_repository.ReadOutstandingOrders());

            if (from != null)
            {
                orders = orders.Where(x => x.StartDate >= from);
            }

            if (to != null)
            {
                orders = orders.Where(x => x.StartDate <= to);
            }

            return orders.ToList();
        }

        public void Update(Order order)
        {
            _repository.Update(_mapper.Map<Order, OrderEntity>(order));
        }
    }
}
