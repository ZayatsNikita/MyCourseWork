using PL.Models;
using System.Collections.Generic;
using System;
namespace PL.Infrastructure.Services.Abstract
{
    public interface IOrderServices
    {
        public void Create(Order order);
        public List<Order> Read(
            int minId = -1,
            int maxId = -1,

            int minMasterId = -1,
            int maxMasterId = -1,

            int minManagerId = -1,
            int maxManagerId = -1,

            DateTime? minStartDate = null,
            DateTime? maxStartDate = null,

            DateTime? minCompletionDate = null,
            DateTime? maxCompletionDate = null,

            int minClientId = -1,
            int maxClientId = -1
            );
        public void Delete(Order order);
        public void Update(Order order,
            int ClientId = -1,
            int MasterId = -1,
            int ManagerId = -1,
            DateTime? StartDate = null,
            DateTime? CompletionDate = null);
    }
}
