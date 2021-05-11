using PL.Models;
using System.Collections.Generic;
using System;
namespace PL.Infrastructure.Services.Abstract
{
    public interface IOrderServices
    {
        public void Create(Order order);
        public List<Order> Read(
            int minId = Constans.DefIntVal,
            int maxId = Constans.DefIntVal,

            int minMasterId = Constans.DefIntVal,
            int maxMasterId = Constans.DefIntVal,

            int minManagerId = Constans.DefIntVal,
            int maxManagerId = Constans.DefIntVal,

            DateTime? minStartDate = null,
            DateTime? maxStartDate = null,

            DateTime? minCompletionDate = null,
            DateTime? maxCompletionDate = null,

            int minClientId = Constans.DefIntVal,
            int maxClientId = Constans.DefIntVal
            );
        public void Delete(Order order);
        public void Update(Order order,
            int ClientId = Constans.DefIntVal,
            int MasterId = Constans.DefIntVal,
            int ManagerId = Constans.DefIntVal,
            DateTime? StartDate = null,
            DateTime? CompletionDate = null);
    }
}
