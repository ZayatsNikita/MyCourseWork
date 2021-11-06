using DL.Entities;
using System.Collections.Generic;

namespace DL.Repositories.Abstract
{
    public interface IOrderEntityRepository : IRepository<OrderEntity>
    {
        List<OrderEntity> ReadComplitedOrders();

        List<OrderEntity> ReadOutstandingOrders();
    }
}