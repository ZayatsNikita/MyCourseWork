using System;
using System.Collections.Generic;
using PL.Infrastructure.Services.Abstract;
using AutoMapper;
using PL.Models;

namespace PL.Infrastructure.Services
{
    public class ChartManager : IChartManager
    {
        private BL.Services.Abstract.IChartManager _manager;
        private Mapper _mapper;
        public ChartManager(BL.Services.Abstract.IChartManager manager, Mapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public Dictionary<Worker, decimal> GetInformationAboutProfitByManagers(DateTime? from, DateTime? to)
        {
          Dictionary<Worker, decimal> list = _mapper.Map<Dictionary<BL.DtoModels.Worker, decimal>, Dictionary<Worker, decimal>>(_manager.GetInformationAboutProfitByManagers(from, to));

            return list;
        }

        public Dictionary<Worker, decimal> GetInformationAboutProfitByMasters(DateTime? from, DateTime? to)
        {
            Dictionary<Worker, decimal> list = _mapper.Map<Dictionary<BL.DtoModels.Worker, decimal>, Dictionary<Worker, decimal>>(
            _manager.GetInformationAboutProfitByMasters(from,to));
            return list;
        }

        public List<OrderInfo> GetInformationAboutTheServicesOrdered(DateTime? from, DateTime? to)
        {
            var result = _mapper.Map<List<BL.DtoModels.OrderInfo>, List<OrderInfo>>(_manager.GetInformationAboutTheServicesOrdered(from, to));
            return result;
        }
    }
}
