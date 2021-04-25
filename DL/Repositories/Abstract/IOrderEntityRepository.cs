using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IOrderEntityRepository
    {
        public void Create(OrderEntity client);
        public List<OrderEntity> Read(
            int minId,
            int maxId,

            int minMasterId,
            int maxMasterId,

            int minManagerId,
            int maxManagerId,

            DateTime? minStartDate,
            DateTime? maxStartDate,

            DateTime? minCompletionDate,
            DateTime? maxCompletionDate,

            int minClientId,
            int maxClientId
            );
        public List<OrderEntity> ReadOutstandingOrders();
        public void Delete(OrderEntity order);
        public void Update(OrderEntity order, 
            int ClientId,
            int MasterId,
            int ManagerId,
            DateTime? StartDate,
            DateTime? CompletionDate);
    }
}