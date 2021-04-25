using BL.DtoModels.Combined;
using BL.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
namespace BL.Services
{
    public class FullOrderInfoServices : Abstract.IFullOrderInfoServices
    {
        private OrderInfoServices _orderInfoService;
        private Mapper _mapper;
        public FullOrderInfoServices(OrderInfoServices orderInfoService, Mapper mapper)
        {
            _orderInfoService = orderInfoService;
            _mapper = mapper;
        }
        public void Create(FullOrderInfo orderInfo)
        {
            
        }

        public void Delete(FullOrderInfo fullOrderInfo)
        {
            _orderInfoService.Delete(new OrderInfo() { Id = fullOrderInfo.Id}); 
        }

        public List<FullOrderInfo> Read(int minId = -1, int maxId = -1, int minCountOfServicesRendered = -1, int maxCountOfServicesRendered = -1, int minBuildStandartId = -1, int maxBuildStandartId = -1, int minOrderNumber = -1, int maxOrderNumber = -1)
        {
            throw new NotImplementedException();
        }

        public void Update(FullOrderInfo orderInfo, int OrderNumber, int CountOfServicesRendered, int BuildStandartId)
        {
            throw new NotImplementedException();
        }
    }
}
