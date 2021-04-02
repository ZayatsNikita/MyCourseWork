using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IOrderInfoServices
    {
        public void Create(OrderInfo orderInfo);
        public List<OrderInfo> Read(
            int minId,
            int maxId,
            int minCountOfServicesRendered,
            int maxCountOfServicesRendered,
            int minServiceId,
            int maxServiceId,
            int minOrderNumber,
            int maxOrderNumber
            );
        public void Delete(OrderInfo orderInfo);
        public void Update(OrderInfo orderInfo,
            int OrderNumber,
            int CountOfServicesRendered,
            int ServiceId
            );
    }
}