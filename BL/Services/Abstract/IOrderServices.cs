using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.DtoModels;

namespace BL.Services.Abstract
{
    public interface IOrderServices
    {
        public int Create(Order order);
        public List<Order> Read(
            int minId = -1,
            int maxId = -1,

            int minMasterId = -1,
            int maxMasterId = -1,

            int minManagerId = -1,
            int maxManagerId = -1,

            DateTime? minStartDate= null,
            DateTime? maxStartDate = null,

            DateTime? minCompletionDate = null,
            DateTime? maxCompletionDate = null,

            int minClientId = -1,
            int maxClientId = -1
            );
        public List<Order> ReadOutstandingOrders();
        public void Delete(Order order);
        public void Update(Order order, 
            int ClientId,
            int MasterId,
            int ManagerId,
            DateTime? StartDate,
            DateTime? CompletionDate);
    }
}