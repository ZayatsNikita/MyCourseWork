using PL.Models;
using System.Collections.Generic;
using System;
namespace PL.Infrastructure.Services.Abstract
{
    public interface IOrderServices
    {
        public void Create(Order order);
        public List<Order> Read();
        public void Delete(Order order);
        public void Update(Order order);
        public Order ReadById(int id);
    }
}
