using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.DtoModels;

namespace BL.Services.Abstract
{
    public interface IOrderInfoServices
    {
        public void Create(OrderInfo orderInfo);
        public List<OrderInfo> Read(
            int minId = Constants.DefIntVal,
            int maxId = Constants.DefIntVal,
            int minCountOfServicesRendered = Constants.DefIntVal,
            int maxCountOfServicesRendered = Constants.DefIntVal,
            int minServiceId = Constants.DefIntVal,
            int maxServiceId = Constants.DefIntVal,
            int minOrderNumber = Constants.DefIntVal,
            int maxOrderNumber = Constants.DefIntVal
            );
        public void Delete(OrderInfo orderInfo);
        public void Update(OrderInfo orderInfo,
            int OrderNumber,
            int CountOfServicesRendered,
            int ServiceId
            );
    }
}