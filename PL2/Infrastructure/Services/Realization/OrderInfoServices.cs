using AutoMapper;
using PL.Models;
using System.Collections.Generic;
namespace PL.Infrastructure.Services
{
    public class OrderInfoServices : Abstract.IOrderInfoServise
    {
        private BL.Services.Abstract.IOrderInfoServices _repository;
        
        private Mapper _mapper;

        public OrderInfoServices(BL.Services.Abstract.IOrderInfoServices repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(OrderInfo orderInfo)
        {
            _repository.Create(_mapper.Map<OrderInfo, BL.DtoModels.OrderInfo>(orderInfo));
        }

        public void Delete(OrderInfo orderInfo)
        {
            _repository.Delete(orderInfo.Id);
        }

        public List<OrderInfo> Read()
        {
            return _mapper.Map<List<BL.DtoModels.OrderInfo>, List<OrderInfo>>(_repository.Read());
        }

        public OrderInfo ReadById(int id)
        {
            return _mapper.Map<BL.DtoModels.OrderInfo, OrderInfo>(_repository.ReadById(id));
        }

        public void Update(OrderInfo orderInfo)
        {
            _repository.Update(_mapper.Map<OrderInfo, BL.DtoModels.OrderInfo>(orderInfo));
        }
    }
}
