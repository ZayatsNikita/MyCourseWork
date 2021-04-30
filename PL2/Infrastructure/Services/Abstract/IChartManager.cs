using PL.Models;
using System;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IChartManager
    {
        public Dictionary<Worker, decimal> GetInformationAboutProfitByManagers(DateTime? from, DateTime? to);
        public Dictionary<Worker, decimal> GetInformationAboutProfitByMasters(DateTime? from, DateTime? to);
        public List<OrderInfo> GetInformationAboutTheServicesOrdered(DateTime? from, DateTime? to);
    }
}