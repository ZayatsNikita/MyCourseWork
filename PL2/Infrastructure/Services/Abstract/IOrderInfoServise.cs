using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IOrderInfoServise
    {
        public void Create(OrderInfo orderInfo);
        public List<OrderInfo> Read();
        public void Delete(OrderInfo orderInfo);
        public void Update(OrderInfo orderInfo);
        public OrderInfo ReadById(int id);
    }
}
