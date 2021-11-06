using BL.DtoModels;
using System;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IChartManager
    {
        Dictionary<Worker, decimal> GetInformationAboutProfitByManagers(DateTime? from, DateTime? to);
        
        Dictionary<Worker, decimal> GetInformationAboutProfitByMasters(DateTime? from, DateTime? to);

        List<OrderInfo> GetInformationAboutTheServicesOrdered(DateTime? from, DateTime? to);
    }
}