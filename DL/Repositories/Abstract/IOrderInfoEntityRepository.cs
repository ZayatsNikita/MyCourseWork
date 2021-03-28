using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IOrderInfoEntityRepository
    {
        public void Create(OrderInfoEntity orderInfo);
        public List<OrderInfoEntity> Read(
            int minId,
            int maxId,
            int minCountOfServicesRendered,
            int maxCountOfServicesRendered,
            int minServiceId,
            int maxServiceId,
            int minOrderNumber,
            int maxOrderNumber
            );
        public void Delete(OrderInfoEntity orderInfo);
        public void Update(OrderInfoEntity orderInfo,
            int OrderNumber,
            int CountOfServicesRendered,
            int ServiceId
            );
    }
}