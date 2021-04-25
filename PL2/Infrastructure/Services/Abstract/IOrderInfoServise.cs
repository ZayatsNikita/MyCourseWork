using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IOrderInfoServise
    {
        public void Create(OrderInfo orderInfo);
        public List<OrderInfo> Read(
            int minId = -1,
            int maxId = -1,
            int minCountOfServicesRendered = -1,
            int maxCountOfServicesRendered = -1,
            int minServiceId = -1,
            int maxServiceId = -1,
            int minOrderNumber = -1,
            int maxOrderNumber = -1
            );
        public void Delete(OrderInfo orderInfo);
        public void Update(OrderInfo orderInfo,
            int OrderNumber,
            int CountOfServicesRendered,
            int ServiceId
            );
    }
}
