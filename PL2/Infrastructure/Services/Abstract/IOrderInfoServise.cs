using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IOrderInfoServise
    {
        public void Create(OrderInfo orderInfo);
        public List<OrderInfo> Read(
            int minId = Constans.DefIntVal,
            int maxId = Constans.DefIntVal,
            int minCountOfServicesRendered = Constans.DefIntVal,
            int maxCountOfServicesRendered = Constans.DefIntVal,
            int minServiceId = Constans.DefIntVal,
            int maxServiceId = Constans.DefIntVal,
            int minOrderNumber = Constans.DefIntVal,
            int maxOrderNumber = Constans.DefIntVal
            );
        public void Delete(OrderInfo orderInfo);
        public void Update(OrderInfo orderInfo,
            int OrderNumber,
            int CountOfServicesRendered,
            int ServiceId
            );
    }
}
