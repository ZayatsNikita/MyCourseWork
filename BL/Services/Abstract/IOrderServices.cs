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
            int minId = Constants.DefIntVal,
            int maxId = Constants.DefIntVal,

            int minMasterId = Constants.DefIntVal,
            int maxMasterId = Constants.DefIntVal,

            int minManagerId = Constants.DefIntVal,
            int maxManagerId = Constants.DefIntVal,

            DateTime? minStartDate= null,
            DateTime? maxStartDate = null,

            DateTime? minCompletionDate = null,
            DateTime? maxCompletionDate = null,

            int minClientId = Constants.DefIntVal,
            int maxClientId = Constants.DefIntVal
            );
        public List<Order> ReadOutstandingOrders(DateTime? from, DateTime? to);
        public List<Order> ReadComplitedOrders(DateTime? from, DateTime? to);
        public void Delete(Order order);
        public void Update(Order order, 
            int ClientId,
            int MasterId,
            int ManagerId,
            DateTime? StartDate,
            DateTime? CompletionDate);
    }
}