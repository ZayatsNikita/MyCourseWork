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
            _repository.Delete(_mapper.Map<OrderInfo, BL.DtoModels.OrderInfo>(orderInfo));
        }

        public List<OrderInfo> Read(int minId, int maxId, int minCountOfServicesRendered, int maxCountOfServicesRendered, int minServiceId, int maxServiceId, int minOrderNumber, int maxOrderNumber)
        {

            List<OrderInfo> result = _mapper.Map<List<BL.DtoModels.OrderInfo>, List<OrderInfo>>(_repository.Read(minId, maxId, minCountOfServicesRendered, maxCountOfServicesRendered, minServiceId, maxServiceId, minOrderNumber, maxOrderNumber));
            
            return result;
        }

        public void Update(OrderInfo orderInfo, int OrderNumber, int CountOfServicesRendered, int ServiceId)
        {
            _repository.Update(_mapper.Map<OrderInfo, BL.DtoModels.OrderInfo>(orderInfo), OrderNumber, CountOfServicesRendered, ServiceId);
        }
    }
}
